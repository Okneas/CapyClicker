using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Player : MonoBehaviour
{
    // �������� ��� �������� �������� ����������
    public UBigNumber capyPoints = new UBigNumber("0"); // ���������� ������������
    public UBigNumber AllCapyPointsPerRun = new UBigNumber("0"); // ����� ���������� ������������ �� ��� ����
    public UBigNumber maxCapyDollars = new UBigNumber("0"); // ������������ ���������� ������������, ������� ����� �������� �� ���� ���
    public UBigNumber capyPointsPerSecond = new UBigNumber("0"); // ���������� ������������, ���������� �� ������ �������
    public UBigNumber capyPointsPerTap = new UBigNumber("1"); // ���������� ������������, ���������� �� ������ ������� �� ������

    public UBigNumber goldenCapy = new UBigNumber("0"); // ���������� ������� �������
    public UBigNumber profitOfgoldenCapy = new UBigNumber("1");
    public UBigNumber goldenCapyOnThisRun = new UBigNumber("0"); // ���������� ������� �������, ���������� �� ���� ��������
    public UBigNumber capyDollarsForGoldenCapy = new UBigNumber("1000000000"); // ���������� ������������, ����������� ��� ��������� ������� ��������

    public Text textCapyPoints; // ��������� ������ ��� ����������� ���������� ������������
    public Text textCapyPointsPerSecond; // ��������� ������ ��� ����������� ���������� ������������, ���������� �� ������ �������
    public Text textGoldenCapyOnThisRun; // ��������� ������ ��� ����������� ���������� ������� �������
    public Text textAllGoldenCapy;

    public int maxConditionsForUpgrades = 50; // ������������ ���������� ������� ��� ���������

    public int countOfBabyCapyUpgrades; // ���������� ��������� "�������"
    public int countOfZooUpgrades; // ���������� ��������� "��������"
    public int countOfNatureUpgrades; // ���������� ��������� "�������"
    public int countOfLabUpgrades; // ���������� ��������� "�����������"
    public int countOfGodUpgrades; // ���������� ��������� "���"
    public int countOfMultiverseUpgrades; // ���������� ��������� "������������"

    public UBigNumber capyPointsPerSecondBabyCapy; // ���������� ������������, ���������� �� ������ ������� ��� ��������� "�������"
    public UBigNumber capyPointsPerSecondZoo; // ���������� �������������������, ���������� �� ������ ������� ��� ��������� "��������"
    public UBigNumber capyPointsPerSecondNature; // ���������� ������������, ���������� �� ������ ������� ��� ��������� "�������"
    public UBigNumber capyPointsPerSecondLab; // ���������� ������������, ���������� �� ������ ������� ��� ��������� "�����������"
    public UBigNumber capyPointsPerSecondGod; // ���������� ������������, ���������� �� ������ ������� ��� ��������� "���"
    public UBigNumber capyPointsPerSecondMultiverse; // ���������� ������������, ���������� �� ������ ������� ��� ��������� "������������"

    public List<Upgrades> AllUpgrades; // ������ ���� ���������
    public List<PrestigeUpgrade> PrestigeAllUpgradesObtained;
    public List<PrestigeUpgrade> AllPrestigeUpgrades;

    public List<UpgradeButton> upgradesSlots; // ������ ������ ���������

    public UBigNumber percentAll = new UBigNumber("0");// ������� "���"
    public UBigNumber percentBabyCapy = new UBigNumber("0"); // ������� "�������"
    public UBigNumber percentZoo = new UBigNumber("0"); // �������  "��������"
    public UBigNumber percentNature = new UBigNumber("0"); // �������  "�������"
    public UBigNumber percentLab = new UBigNumber("0"); // �������  "�����������"
    public UBigNumber percentGod = new UBigNumber("0"); // �������  "���"
    public UBigNumber percentMultiverse = new UBigNumber("0"); // �������  "���������������"
    public UBigNumber percentTap = new UBigNumber("100"); // ������� ��� ������������, ���������� �� ������ ������� �� ������
    public UBigNumber percentTapFromAll = new UBigNumber("0"); // ������� ������������, ���������� �� ������ ������� �� ������, ���� ��� ��������� ��������

    // �����, ������� ���������� ��� ������� ����
    private void Awake()
    {
        foreach (PrestigeUpgrade i in AllPrestigeUpgrades)
        {
            i.CheckConditions();
        }
        // ��������� ��������� � ����������� �� �� �������
        for (int i = 0; i < AllUpgrades.Count; i++)
        {
            AllUpgrades[i].alreadySet = false;
            AllUpgrades[i].active = false;
            if (AllUpgrades[i].condition <= maxConditionsForUpgrades)
            {
                AllUpgrades[i].active = true;
            }
        }
        GameObject upgradePanel;
        // ����� ������ ��������� � ���������� � ������ ������ ���������
        upgradePanel = GameObject.FindGameObjectWithTag("Upgrade");
        for (int i = 0; i < GameObject.FindGameObjectWithTag("Upgrade").transform.childCount; i++)
        {
           upgradesSlots.Add(upgradePanel.transform.GetChild(i).GetComponent<UpgradeButton>());
        }
    }

    // �����, ������� ���������� ������ ����
    private void Update()
    {
        // ���������� ���������� ������� ������� �� ���� ��������, ���� ���������� ������������ �����
        if (AllCapyPointsPerRun > UBigNumber.IntPow(goldenCapyOnThisRun + UBigNumber.ConvertToBigNumber(1), 3) * UBigNumber.ConvertToBigNumber(1000000))
        {
            goldenCapyOnThisRun++;
        }
        // ���������� ��������� ����������� �� ������
        textCapyPoints.text = $"{capyPoints.BeautifulString()} CapyDollars";
        textCapyPointsPerSecond.text = $"{capyPointsPerSecond.BeautifulString()} CapyDollars Per Second";
        textGoldenCapyOnThisRun.text = $"+{goldenCapyOnThisRun.BeautifulString()} Golden Capy's On Prestige";
        textAllGoldenCapy.text = $"{goldenCapy.BeautifulString()} all Golden Capy's";

    }
    // �����, ������� ������������� ���������
    public void SetUpgrade(Upgrades upgrade)
    {
        foreach (UpgradeButton i in upgradesSlots)
        {
            if(!i.fill)
            {
                i.SetUpgrade(upgrade);
                break;
            }
        }
    }

    // �����, ������� ��������� ����������
    public void UpdateStatistic()
    {
        countOfBabyCapyUpgrades = GameObject.FindGameObjectWithTag("Baby Capy").GetComponent<ItemButton>().idleItem.count;
        countOfZooUpgrades = GameObject.FindGameObjectWithTag("Zoo").GetComponent<ItemButton>().idleItem.count;
        countOfNatureUpgrades = GameObject.FindGameObjectWithTag("Nature").GetComponent<ItemButton>().idleItem.count;
        countOfLabUpgrades = GameObject.FindGameObjectWithTag("Lab").GetComponent<ItemButton>().idleItem.count;
        countOfGodUpgrades = GameObject.FindGameObjectWithTag("God").GetComponent<ItemButton>().idleItem.count;
        countOfMultiverseUpgrades = GameObject.FindGameObjectWithTag("Multiverse").GetComponent<ItemButton>().idleItem.count;

        capyPointsPerSecondBabyCapy = percentBabyCapy.FromPercentToNum() * UBigNumber.ConvertToBigNumber((countOfBabyCapyUpgrades - 1) * 1) + UBigNumber.ConvertToBigNumber((countOfBabyCapyUpgrades-1) * 1);
        capyPointsPerSecondNature = percentNature.FromPercentToNum() * UBigNumber.ConvertToBigNumber((countOfNatureUpgrades - 1) * 50) + UBigNumber.ConvertToBigNumber((countOfNatureUpgrades - 1) * 50);
        capyPointsPerSecondZoo = percentZoo.FromPercentToNum() * UBigNumber.ConvertToBigNumber((countOfZooUpgrades -1) * 5) + UBigNumber.ConvertToBigNumber((countOfZooUpgrades - 1) * 5);
        capyPointsPerSecondLab = percentLab.FromPercentToNum() * UBigNumber.ConvertToBigNumber((countOfLabUpgrades-1) * 200) + UBigNumber.ConvertToBigNumber((countOfLabUpgrades-1) * 200);
        capyPointsPerSecondGod = percentGod.FromPercentToNum() * UBigNumber.ConvertToBigNumber((countOfGodUpgrades-1) * 1000) + UBigNumber.ConvertToBigNumber((countOfGodUpgrades-1) * 1000);
        capyPointsPerSecondMultiverse = percentMultiverse.FromPercentToNum() * UBigNumber.ConvertToBigNumber((countOfMultiverseUpgrades-1) * 5000) + UBigNumber.ConvertToBigNumber((countOfMultiverseUpgrades-1) * 5000);

        capyPointsPerSecond = ((goldenCapy*profitOfgoldenCapy)/UBigNumber.ConvertToBigNumber(100))*(capyPointsPerSecondBabyCapy + capyPointsPerSecondZoo + capyPointsPerSecondNature +
            capyPointsPerSecondLab + capyPointsPerSecondGod + capyPointsPerSecondMultiverse) + percentAll.FromPercentToNum() * (capyPointsPerSecondBabyCapy + capyPointsPerSecondZoo + capyPointsPerSecondNature +
            capyPointsPerSecondLab + capyPointsPerSecondGod + capyPointsPerSecondMultiverse) + (capyPointsPerSecondBabyCapy + capyPointsPerSecondZoo + capyPointsPerSecondNature +
            capyPointsPerSecondLab + capyPointsPerSecondGod + capyPointsPerSecondMultiverse);

            capyPointsPerTap = (UBigNumber.ConvertToBigNumber(1) + capyPointsPerSecond * percentTapFromAll.FromPercentToNum()) * percentTap.FromPercentToNum();
            capyPointsPerTap.UpdateBigNumber();
    }

    public int GetCountByType(ItemType type)
    {
        switch(type)
        {
            case ItemType.BabyCapy:
                return countOfBabyCapyUpgrades;
            case ItemType.God:
                return countOfGodUpgrades;
            case ItemType.Lab:
                return countOfLabUpgrades;
            case ItemType.MultiUniverse:
                return countOfMultiverseUpgrades;
            case ItemType.Nature:
                return countOfNatureUpgrades;
            case ItemType.Zoo:
                return countOfZooUpgrades;
            default:
                return 0;
        }
    }
}
