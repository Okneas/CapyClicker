using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Player : MonoBehaviour
{
    public UBigNumber capyPoints = new UBigNumber("0");
    public UBigNumber capyPointsPerSecond = new UBigNumber("0");
    public UBigNumber capyPointsPerTap = new UBigNumber("1");

    public Text textCapyPoints;
    public Text textCapyPointsPerSecond;
    public Text AllAnotherStats;

    public int countOfBabyCapyUpgrades;
    public int countOfZooUpgrades;
    public int countOfNatureUpgrades;
    public int countOfLabUpgrades;
    public int countOfGodUpgrades;
    public int countOfMultiverseUpgrades;

    public UBigNumber capyPointsPerSecondBabyCapy;
    public UBigNumber capyPointsPerSecondZoo;
    public UBigNumber capyPointsPerSecondNature;
    public UBigNumber capyPointsPerSecondLab;
    public UBigNumber capyPointsPerSecondGod;
    public UBigNumber capyPointsPerSecondMultiverse;

    public List<Upgrades> AllUpgrades;

    public GameObject[] upgradesSlots;

    public UBigNumber percentAll = new UBigNumber("0");
    public UBigNumber percentBabyCapy = new UBigNumber("0");
    public UBigNumber percentZoo = new UBigNumber("0");
    public UBigNumber percentNature = new UBigNumber("0");
    public UBigNumber percentLab = new UBigNumber("0");
    public UBigNumber percentGod = new UBigNumber("0");
    public UBigNumber percentMultiverse = new UBigNumber("0");
    public UBigNumber percentTap = new UBigNumber("100");
    public UBigNumber percentTapFromAll = new UBigNumber("0");

    private void Start()
    {
        upgradesSlots = GameObject.FindGameObjectsWithTag("Upgrade");
    }

    private void Update()
    {
        textCapyPoints.text = $"{capyPoints} CapyDollars";
        textCapyPointsPerSecond.text = $"{capyPointsPerSecond} CapyDollars Per Second";
    }

    public void SetUpgrade(Upgrades upgrade)
    {
        foreach (GameObject i in upgradesSlots)
        {
            if(!i.GetComponent<UpgradeButton>().fill)
            {
                i.GetComponent<UpgradeButton>().SetUpgrade(upgrade);
                if (i.GetComponent<UpgradeButton>().upgrade.icon != null)
                {
                    i.GetComponent<Image>().sprite = i.GetComponent<UpgradeButton>().upgrade.icon;
                }
                else
                {
                    i.GetComponent<Image>().color = Color.blue;
                }
                AllUpgrades.Remove(upgrade);
                break;
            }
        }
    }

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

        capyPointsPerSecond = percentAll.FromPercentToNum() * (capyPointsPerSecondBabyCapy + capyPointsPerSecondZoo + capyPointsPerSecondNature +
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
