using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;
using System;

public class UBigNumber
{
    public static readonly string[] symbols = { " ", "K", "M", "B", "T", "Qa", "Qi", "Sx", "Sp" };
    private List<int> intCheckers = new List<int>();
    private List<int> floatCheckers = new List<int>();

    public UBigNumber() { }

    public UBigNumber(string num)
    {
        bool afterDot = false;
        for (int i = 0; i < num.Length; i++)
        {
            if (num[i] == '.' || num[i] == ',')
            {
                afterDot = true;
                continue;
            }
            if (!afterDot && num[i] != '-') this.intCheckers.Add(Convert.ToInt32(num[i].ToString()));
            if (afterDot) this.floatCheckers.Add(Convert.ToInt32(num[i].ToString()));
        }
    }

    #region Math Operators
    public static UBigNumber operator +(UBigNumber bigNumber1, UBigNumber bigNumber2)
    {
        bigNumber1.intCheckers.Reverse();
        bigNumber2.intCheckers.Reverse();
        UBigNumber res = new UBigNumber();

        for (int i = 0; i < Mathf.Max(bigNumber1.intCheckers.Count, bigNumber2.intCheckers.Count); i++)
        {
            int value1 = i < bigNumber1.intCheckers.Count ? bigNumber1.intCheckers[i] : 0;
            int value2 = i < bigNumber2.intCheckers.Count ? bigNumber2.intCheckers[i] : 0;
            res.intCheckers.Add(value1 + value2);
        }
        for (int i = 0; i < Mathf.Max(bigNumber1.floatCheckers.Count, bigNumber2.floatCheckers.Count); i++)
        {
            int value1 = i < bigNumber1.floatCheckers.Count ? bigNumber1.floatCheckers[i] : 0;
            int value2 = i < bigNumber2.floatCheckers.Count ? bigNumber2.floatCheckers[i] : 0;
            res.floatCheckers.Add(value1 + value2);
        }
        bigNumber1.intCheckers.Reverse();
        bigNumber2.intCheckers.Reverse();
        res.intCheckers.Reverse();
        res.UpdateBigNumber();
        return res;
    }

    public static UBigNumber operator ++(UBigNumber bigNumber1)
    {
        UBigNumber one = new UBigNumber("1");
        return bigNumber1 + one;
    }

    public static UBigNumber operator --(UBigNumber bigNumber1)
    {
        UBigNumber one = new UBigNumber("1");
        return bigNumber1 - one;
    }

    public static UBigNumber operator -(UBigNumber bigNumber1, UBigNumber bigNumber2)
    {
        bigNumber2.intCheckers.Reverse();
        UBigNumber num1 = new UBigNumber(bigNumber1.ToString());
        UBigNumber num2 = new UBigNumber(bigNumber2.ToString());
        bigNumber2.intCheckers.Reverse();
        while (num1.floatCheckers.Count != num2.floatCheckers.Count)
        {
            if (num1.floatCheckers.Count > num2.floatCheckers.Count)
            {
                num2.floatCheckers.Add(0);
            }
            else
            {
                num1.floatCheckers.Add(0);
            }
        }
        num1.intCheckers.Reverse();
        for (int i = num1.floatCheckers.Count - 1; i >= 0; i--)
        {
            if (num1.floatCheckers[i] < num2.floatCheckers[i])
            {
                if (i != 0)
                {
                    int j = i - 1;
                    while (num1.floatCheckers[j] == 0)
                    {
                        num1.floatCheckers[j] = 9;
                        j--;
                        if (j < 0)
                        {
                            int k = 0;
                            while (num1.intCheckers[k] == 0)
                            {
                                num1.intCheckers[k] = 9;
                                k++;
                            }
                            num1.intCheckers[k]--;
                            break;
                        }
                    }
                    if (j >= 0)
                    {
                        num1.floatCheckers[j]--;
                    }
                    num1.floatCheckers[i] = num1.floatCheckers[i] + 10 - num2.floatCheckers[i];
                }
                else
                {
                    int k = 0;
                    while (num1.intCheckers[k] == 0)
                    {
                        num1.intCheckers[k] = 9;
                        k++;
                    }
                    num1.intCheckers[k]--;
                    num1.floatCheckers[i] = num1.floatCheckers[i] + 10 - num2.floatCheckers[i];
                }
            }
            else
            {
                num1.floatCheckers[i] -= num2.floatCheckers[i];
            }
        }
        for (int i = 0; i < Math.Min(num1.intCheckers.Count, num2.intCheckers.Count); i++)
        {
            if (num1.intCheckers[i] < num2.intCheckers[i])
            {
                if (i + 1 != num1.intCheckers.Count)
                {
                    int j = i + 1;
                    while (num1.intCheckers[j] == 0)
                    {
                        num1.intCheckers[j] = 9;
                        j++;
                    }
                    num1.intCheckers[j]--;
                    num1.intCheckers[i] = num1.intCheckers[i] + 10 - num2.intCheckers[i];
                }
                else
                {
                    num1.intCheckers[i] -= num2.intCheckers[i];
                }
            }
            else
            {
                num1.intCheckers[i] -= num2.intCheckers[i];
            }
        }
        num1.intCheckers.Reverse();
        num1.UpdateBigNumber();
        return num1;
    }

    public static UBigNumber operator *(UBigNumber bigNumber1, UBigNumber bigNumber2)
    {
        if (bigNumber1 == ConvertToBigNumber(0) || bigNumber2==ConvertToBigNumber(0))
        {
            return new UBigNumber("0");
        }
        UBigNumber num1 = new UBigNumber(bigNumber1.GetIntPart().ToString() + bigNumber1.GetFloatPart().ToString());
        UBigNumber num2 = new UBigNumber(bigNumber2.GetIntPart().ToString() + bigNumber2.GetFloatPart().ToString());
        UBigNumber res = new UBigNumber();
        int count1, count2;
        int numberAfterDot = bigNumber1.floatCheckers.Count + bigNumber2.floatCheckers.Count;
        bool first = false;
        if (num1.intCheckers.Count > num2.intCheckers.Count)
        {
            count1 = num2.intCheckers.Count;
            count2 = num1.intCheckers.Count;
            first = true;
        }
        else
        {
            count1 = num1.intCheckers.Count;
            count2 = num2.intCheckers.Count;
        }
        res.intCheckers.Add(0);
        for (int i = 0; i < count1; i++)
        {
            UBigNumber temp = new UBigNumber();
            for (int k = 0; k < i; k++)
            {
                temp.intCheckers.Add(0);
            }
            for (int j = 0; j < count2; j++)
            {
                if (first)
                {
                    temp.intCheckers.Add(num1.intCheckers[(count2 - 1) - j] * num2.intCheckers[(count1 - 1) - i]);
                }
                else
                {
                    temp.intCheckers.Add(num1.intCheckers[(count1 - 1) - i] * num2.intCheckers[(count2 - 1) - j]);
                }
            }
            temp.intCheckers.Reverse();
            temp.UpdateBigNumber();
            res += temp;
            res.UpdateBigNumber();
        }
        try
        {
            for (int i = 0; i < numberAfterDot; i++)
            {
                res.floatCheckers.Add(res.intCheckers[(res.intCheckers.Count - 1) - i]);
            }
        }
        catch
        {
            Debug.Log($"{numberAfterDot}");
        }
        res.intCheckers.Reverse();
        res.intCheckers.RemoveRange(0, numberAfterDot);
        res.intCheckers.Reverse();
        res.floatCheckers.Reverse();
        res.UpdateBigNumber();
        return res; 
    }

    public static UBigNumber operator /(UBigNumber bigNumber1, UBigNumber bigNumber2)
    {
        if (bigNumber1 < bigNumber2)
        {
            return new UBigNumber("0");
        }
        else if (bigNumber1 == bigNumber2)
        {
            return new UBigNumber("1");
        }

        UBigNumber num1 = new UBigNumber(bigNumber1.GetIntPart().ToString());
        UBigNumber num2 = new UBigNumber(bigNumber2.GetIntPart().ToString());
        UBigNumber res = new UBigNumber();
        UBigNumber temp = new UBigNumber();


        for (int i = 0; i < num1.intCheckers.Count; i++)
        {
            int downNums = 0;
            while (temp < num2)
            {
                downNums++;
                temp.intCheckers.Add(num1.intCheckers[i]);
                if (temp < num2)
                {
                    i++;
                }
                if (downNums >= 2)
                {
                    res.intCheckers.Add(0);
                }
                if (i == num1.intCheckers.Count)
                {
                    break;
                }
            }
            if (temp >= num2)
            {
                int j = 0;
                while (temp >= num2)
                {
                    j++;
                    temp -= num2;
                }
                res.intCheckers.Add(j);
            }
            if (temp == new UBigNumber("0") && temp.intCheckers.Count >= 2)
            {
                res.intCheckers.Add(0);
                temp.intCheckers.Remove(0);
            }
        }
        res.UpdateBigNumber();
        return res;
    }
    #endregion

    #region Logic Operators

    public static bool operator !=(UBigNumber bigNumber1, int intNumber2)
    {
        ConvertToBigNumber(intNumber2);
        return bigNumber1 != intNumber2;
    }

    public static bool operator ==(UBigNumber bigNumber1, int intNumber2)
    {
        ConvertToBigNumber(intNumber2);
        return !(bigNumber1 != intNumber2);
    }

    public static bool operator ==(UBigNumber bigNumber1, UBigNumber bigNumber2)
    {
        UBigNumber num1 = new UBigNumber(bigNumber1.ToString());
        UBigNumber num2 = new UBigNumber(bigNumber2.ToString());
        num1.UpdateBigNumber();
        num2.UpdateBigNumber();
        if (num1.intCheckers.Count != num2.intCheckers.Count || num1.floatCheckers.Count != num2.floatCheckers.Count)
        {
            return false;
        }
        for (int i = 0; i < num1.intCheckers.Count; i++)
        {
            if (num1.intCheckers[i] != num2.intCheckers[i])
            {
                return false;
            }
        }
        for (int i = 0; i < num1.floatCheckers.Count; i++)
        {
            if (num1.floatCheckers[i] != num2.floatCheckers[i])
            {
                return false;
            }
        }
        return true;
    }

    public static bool operator !=(UBigNumber bigNumber1, UBigNumber bigNumber2)
    {
        if (bigNumber1 == bigNumber2)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public static bool operator >(UBigNumber bigNumber1, UBigNumber bigNumber2)
    {
        UBigNumber num1 = new UBigNumber(bigNumber1.ToString());
        UBigNumber num2 = new UBigNumber(bigNumber2.ToString());
        num1.UpdateBigNumber();
        num2.UpdateBigNumber();
        if (num1.intCheckers.Count > num2.intCheckers.Count)
        {
            return true;
        }
        else if (num1.intCheckers.Count < num2.intCheckers.Count)
        {
            return false;
        }
        for (int i = 0; i < num1.intCheckers.Count; i++)
        {
            if (num1.intCheckers[i] > num2.intCheckers[i])
            {
                return true;
            }
            else if (num1.intCheckers[i] < num2.intCheckers[i])
            {
                return false;
            }
        }
        for (int i = 0; i < num1.floatCheckers.Count; i++)
        {
            if (num1.floatCheckers[i] > num2.floatCheckers[i])
            {
                return true;
            }
            else if (num1.floatCheckers[i] < num2.floatCheckers[i])
            {
                return false;
            }
        }
        return false;
    }

    public static bool operator <(UBigNumber bigNumber1, UBigNumber bigNumber2)
    {
        if (!(bigNumber1 > bigNumber2 || bigNumber1 == bigNumber2))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool operator <=(UBigNumber bigNumber1, UBigNumber bigNumber2)
    {
        UBigNumber num1 = new UBigNumber(bigNumber1.ToString());
        UBigNumber num2 = new UBigNumber(bigNumber2.ToString());
        num1.UpdateBigNumber();
        num2.UpdateBigNumber();
        if ((num1 < num2) || (num1 == num2))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool operator >=(UBigNumber bigNumber1, UBigNumber bigNumber2)
    {
        UBigNumber num1 = new UBigNumber(bigNumber1.ToString());
        UBigNumber num2 = new UBigNumber(bigNumber2.ToString());
        num1.UpdateBigNumber();
        num2.UpdateBigNumber();
        if ((num1 > num2) || (num1 == num2))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion

    public void UpdateBigNumber()
    {
        if (floatCheckers.Count > 0 && intCheckers.Count == 0)
        {
            intCheckers.Add(0);
        }
        for (int i = floatCheckers.Count - 1; i >= 0; i--)
        {
            if (floatCheckers[i] >= 10)
            {
                if (i == 0)
                {
                    intCheckers[intCheckers.Count - 1] += floatCheckers[i] / 10;
                    floatCheckers[i] %= 10;
                }
                else
                {
                    floatCheckers[i - 1] += floatCheckers[i] / 10;
                    floatCheckers[i] %= 10;
                }
            }
        }
        for (int i = this.intCheckers.Count - 1; i > 0; i--)
        {
            if (intCheckers[i] >= 10)
            {
                if (intCheckers.Count == 0)
                {
                    intCheckers.Insert(0, intCheckers[i] / 10);
                    intCheckers[i] %= 10;
                }
                else
                {
                    intCheckers[i - 1] += intCheckers[i] / 10;
                    intCheckers[i] %= 10;
                }
            }
            if (intCheckers[i] < 0)
            {
                intCheckers[i - 1] -= (intCheckers[i] / 10) + 1;
                intCheckers[i] = 10 - Math.Abs(intCheckers[i] % 10);
            }
        }

        int j = 0;
        if (intCheckers.Count > 1)
        {
            while (intCheckers[j] == 0)
            {
                j++;
                if (j >= intCheckers.Count)
                {
                    break;
                }
            }
        }
        for (int i = 0; i < j; i++)
        {
            if (intCheckers.Count == 1)
            {
                break;
            }
            intCheckers.Remove(0);
        }
        j = 0;
        if (floatCheckers.Count > 0)
        {
            int count = floatCheckers.Count - 1;
            while (floatCheckers[count] == 0)
            {
                j++;
                count--;
                if (count < 0)
                {
                    break;
                }
            }
        }
        floatCheckers.Reverse();
        for (int i = 0; i < j; i++)
        {
            floatCheckers.Remove(0);
        }
        floatCheckers.Reverse();
    }

    public override string ToString()
    {
        string res = "";
        for (int i = 0; i < this.intCheckers.Count; i++)
        {
            res += this.intCheckers[i].ToString();
        }
        if (this.floatCheckers.Count != 0)
        {
            res += '.';
            for (int i = 0; i < this.floatCheckers.Count; i++)
            {
                res += this.floatCheckers[i].ToString();
            }
        }
        return res;
    }

    public int GetNumByIndex(int i) => intCheckers[i];

    public static UBigNumber ConvertToBigNumber<T>(T num)
    {
        try
        {
            return new UBigNumber(num.ToString());
        }
        catch
        {
            return new UBigNumber();
        }
    }

    public UBigNumber GetFloatPart()
    {
        UBigNumber res = new UBigNumber();
        for (int i = 0; i < floatCheckers.Count; i++)
        {
            res.intCheckers.Add(floatCheckers[i]);
        }
        return res;
    }

    public UBigNumber GetIntPart()
    {
        UBigNumber res = new UBigNumber();
        for (int i = 0; i < intCheckers.Count; i++)
        {
            res.intCheckers.Add(intCheckers[i]);
        }
        return res;
    }

    public UBigNumber FromPercentToNum()
    {
        UBigNumber num = new UBigNumber(this.ToString());
        if (num.intCheckers.Count - 1 >= 0)
        {
            num.floatCheckers.Insert(0, num.intCheckers[num.intCheckers.Count - 1]);
        }
        else
        {
            num.floatCheckers.Insert(0, 0);
        }
        if (num.intCheckers.Count - 2 >= 0)
        {
            num.floatCheckers.Insert(0, num.intCheckers[num.intCheckers.Count - 2]);
        }
        else
        {
            num.floatCheckers.Insert(0, 0);
        }
        if (num.intCheckers.Count - 1 >= 0)
        {
            num.intCheckers.RemoveAt(num.intCheckers.Count - 1);
        }
        else
        {
            num.intCheckers.Clear();
            num.intCheckers.Add(0);
        }
        if (num.intCheckers.Count - 1 >= 0)
        {
            num.intCheckers.RemoveAt(num.intCheckers.Count - 1);
        }
        else
        {
            num.intCheckers.Clear();
            num.intCheckers.Add(0);
        }
        num.UpdateBigNumber();
        return num;
    }

}
