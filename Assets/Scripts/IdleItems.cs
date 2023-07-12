using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public enum ItemType
{
    BabyCapy,
    Zoo,
    Nature,
    Lab,
    God,
    MultiUniverse
}

public abstract class IdleItems
{
    public Image image;
    public string name;
    public UBigNumber baseCost;
    public UBigNumber cost;
    public UBigNumber capyDollarsPerSecond;
    public int count;
    public Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    public IdleItems(string name, string price, UBigNumber cdps)
    {
        this.name = name;
        this.baseCost = new UBigNumber(price);
        this.cost = this.baseCost;
        this.capyDollarsPerSecond = cdps;
        this.count = 1;
    }

    public void PriceChange()
    {
        this.count++;
        player.capyPoints -= this.cost;
        this.cost = this.baseCost * UBigNumber.ConvertToBigNumber(Mathf.Pow(count, 1.1f));
    }
}

public class BabyCapy : IdleItems
{
    public BabyCapy(string name, string price, UBigNumber cdps) : base(name, price, cdps)
    {
        this.name = name;
        this.baseCost = new UBigNumber(price);
        this.cost = this.baseCost;
        this.capyDollarsPerSecond = cdps;
        this.count = 1;
    }
}

public class Zoo : IdleItems
{
    public Zoo(string name, string price, UBigNumber cdps) : base(name, price, cdps)
    {
        this.name = name;
        this.baseCost = new UBigNumber(price);
        this.cost = this.baseCost;
        this.capyDollarsPerSecond = cdps;
        this.count = 1;
    }
}

public class Nature : IdleItems
{
    public Nature(string name, string price, UBigNumber cdps) : base(name, price, cdps)
    {
        this.name = name;
        this.baseCost = new UBigNumber(price);
        this.cost = this.baseCost;
        this.capyDollarsPerSecond = cdps;
        this.count = 1;
    }
}

public class Lab : IdleItems
{
    public Lab(string name, string price, UBigNumber cdps) : base(name, price, cdps)
    {
        this.name = name;
        this.baseCost = new UBigNumber(price);
        this.cost = this.baseCost;
        this.capyDollarsPerSecond = cdps;
        this.count = 1;
    }
}

public class God : IdleItems
{
    public God(string name, string price, UBigNumber cdps) : base(name, price, cdps)
    {
        this.name = name;
        this.baseCost = new UBigNumber(price);
        this.cost = this.baseCost;
        this.capyDollarsPerSecond = cdps;
        this.count = 1;
    }
}

public class Multiverse : IdleItems
{
    public Multiverse(string name, string price, UBigNumber cdps) : base(name, price, cdps)
    {
        this.name = name;
        this.baseCost = new UBigNumber(price);
        this.cost = this.baseCost;
        this.capyDollarsPerSecond = cdps;
        this.count = 1;
    }
}
