using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PrestigeUpgradeButton : UpgradeButton
{
    public PrestigeUpgrade prestigeUpgrade;
    public void Awake()
    {
        SetUpgrade(prestigeUpgrade);
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        // ������������� ������ ��������� � �����������
        if (prestigeUpgrade.icon != null)
        {
            description.transform.GetChild(0).GetComponent<Image>().sprite = prestigeUpgrade.icon;
        }
        description.transform.GetChild(1).GetComponent<Text>().text = prestigeUpgrade.name;// ������������� �������� ��������� � ������
        description.transform.GetChild(2).GetComponent<Text>().text = $"���������: {prestigeUpgrade.price}\n"; // ������������� ��������� ��������� � ������
        if (prestigeUpgrade.effectOnGoldenCapyProfit != UBigNumber.ConvertToBigNumber(0))
        {
            description.transform.GetChild(2).GetComponent<Text>().text += $"������� �� ������� ������� +{prestigeUpgrade.stringEffectOnGoldenCapyProfit}%\n";
        }
        if (prestigeUpgrade.effectOnConditions != 0)
        {
            description.transform.GetChild(2).GetComponent<Text>().text += $"������������� ��������� �� +{prestigeUpgrade.effectOnConditions} ��������\n";
        }
        if (prestigeUpgrade.effectOnCapyPerSec != UBigNumber.ConvertToBigNumber(0))// ���� � ��������� ���� ������ �� ������� �������� � �������, ��:
        {
            description.transform.GetChild(2).GetComponent<Text>().text += $"���� ����� +{prestigeUpgrade.stringEffectOnCapyPerSec}%\n";// ������������� ����� � �������� �� ������� �������� � �������
        }
        if (prestigeUpgrade.effectOnCapyPerSecOnItem != UBigNumber.ConvertToBigNumber(0))// ���� � ��������� ���� ������ �� ������� �������� � ������� ��� ������������� ��������, ��:
        {
            description.transform.GetChild(2).GetComponent<Text>().text += $"����� �������� {upgrade.GetStringItemType()} +{prestigeUpgrade.stringEffectOnCapyPerSecOnItem}%\n";// ������������� ����� � �������� �� ������� �������� � ������� ��� ������������� ��������
        }
        if (prestigeUpgrade.effectOnTap != UBigNumber.ConvertToBigNumber(0)) // ���� � ��������� ���� ������ �� ������� ������ �� ����, ��:
        {
            description.transform.GetChild(2).GetComponent<Text>().text += $"���������� ������ �� ���� +{prestigeUpgrade.stringEffectOnTap}%\n";
        }
        if (prestigeUpgrade.effectOnTapFromAll != UBigNumber.ConvertToBigNumber(0))// ���� � ��������� ���� ������� �� ������� ������ �� ���� �� ������, ��:
        {
            description.transform.GetChild(2).GetComponent<Text>().text += $"���������� ������ �� ���� �� ������ +{prestigeUpgrade.stringEffectOnTapFromAll}%\n";// ������������� ����� � �������� �� ������� ������ �� ����
        }
        if (prestigeUpgrade.active)
        {
            description.transform.GetChild(2).GetComponent<Text>().text += $"��������";
        }
        else
        {
            description.transform.GetChild(2).GetComponent<Text>().text += $"�� ��������";
        }
        tempDes = Instantiate(description, mainCanvas.transform);// ������� ��������� ������, ������� ����� ���������� �������� ���������
        tempDes.transform.position = new Vector2(Input.mousePosition.x - 352, Input.mousePosition.y - 152);// ������������� ������� ���������� ������� � ����������� ����
        tempDes.transform.SetParent(mainCanvas.transform);// ������������� ������������ ������ ���������� ������� � �������
    }
    public override void OnPointerClick(PointerEventData eventData) // �����, ������� ���������� ��� ������� �� ������ ���������
    {
        if (prestigeUpgrade.active) // ���� ������ ��������� ���������, ��:
        {
            if (prestigeUpgrade.price <= player.goldenCapy) // ���� � ������ ���������� �������, ��:
            {
                player.PrestigeAllUpgradesObtained.Add(prestigeUpgrade);
                for (int i = 0; i < player.AllPrestigeUpgrades.Count; i++)
                {
                    player.AllPrestigeUpgrades[i].CheckConditions();
                }
                prestigeUpgrade.active = false; // ������������� ���� ���������� ��������� � false
                player.goldenCapy -= prestigeUpgrade.price; // ��������� ���������� ������� � ������ �� ��������� ���������
                prestigeUpgrade.DoUpgrade(); // �������� ����� DoUpgrade � ������� ��������� ��� ��������� ��� ����������
                player.UpdateStatistic(); // �������� ����� UpdateStatistic � ������� ������ ��� ���������� ��� ����������
                Restart();
            }
        }
    }

    public void Restart()
    {
        {
            GameObject.FindWithTag("Baby Capy").GetComponent<ItemButton>().idleItem.count = 1;
            GameObject.FindWithTag("God").GetComponent<ItemButton>().idleItem.count = 1;
            GameObject.FindWithTag("Lab").GetComponent<ItemButton>().idleItem.count = 1;
            GameObject.FindWithTag("Zoo").GetComponent<ItemButton>().idleItem.count = 1;
            GameObject.FindWithTag("Nature").GetComponent<ItemButton>().idleItem.count = 1;
            GameObject.FindWithTag("Multiverse").GetComponent<ItemButton>().idleItem.count = 1;
            player.percentAll = new UBigNumber("0");
            player.percentBabyCapy = new UBigNumber("0");
            player.percentZoo = new UBigNumber("0");
            player.percentNature = new UBigNumber("0");
            player.percentLab = new UBigNumber("0");
            player.percentGod = new UBigNumber("0");
            player.percentMultiverse = new UBigNumber("0");
            player.percentTap = new UBigNumber("100");
            player.percentTapFromAll = new UBigNumber("0");
            foreach(PrestigeUpgrade i in player.PrestigeAllUpgradesObtained)
            {
                if(i.effectOnCapyPerSec != UBigNumber.ConvertToBigNumber(0))
                {
                    player.percentAll += i.effectOnCapyPerSec;
                }
                if (i.effectOnCapyPerSecOnItem != UBigNumber.ConvertToBigNumber(0))
                {
                    switch (i.itemType)
                    {
                        case ItemType.BabyCapy:
                            player.percentBabyCapy += i.effectOnCapyPerSecOnItem;
                            break;
                        case ItemType.Zoo:
                            player.percentZoo += i.effectOnCapyPerSecOnItem;
                            break;
                        case ItemType.Nature:
                            player.percentNature += i.effectOnCapyPerSecOnItem;
                            break;
                        case ItemType.Lab:
                            player.percentLab += i.effectOnCapyPerSecOnItem;
                            break;
                        case ItemType.God:
                            player.percentGod += i.effectOnCapyPerSecOnItem;
                            break;
                        case ItemType.MultiUniverse:
                            player.percentMultiverse += i.effectOnCapyPerSecOnItem;
                            break;
                    }
                }
                if(i.effectOnTap != UBigNumber.ConvertToBigNumber(0))
                {
                    player.percentTap += i.effectOnTap;
                }
                if(i.effectOnTapFromAll != UBigNumber.ConvertToBigNumber(0))
                {
                    player.percentTapFromAll += i.effectOnTapFromAll;
                }
            }
            player.capyPoints = new UBigNumber("0");
            player.AllCapyPointsPerRun = new UBigNumber("0");
            player.maxCapyDollars = new UBigNumber("0");
            player.capyPointsPerSecond = new UBigNumber("0");
            player.capyPointsPerTap = new UBigNumber("1");
            player.goldenCapy += player.goldenCapyOnThisRun;
            player.goldenCapyOnThisRun = new UBigNumber("0");
            player.countOfBabyCapyUpgrades = 0;
            player.countOfZooUpgrades = 0;
            player.countOfNatureUpgrades = 0;
            player.countOfLabUpgrades = 0;
            player.countOfGodUpgrades = 0;
            player.countOfMultiverseUpgrades = 0;
            for (int i = 0; i < player.AllUpgrades.Count; i++)
            {
                if (player.AllUpgrades[i].condition <= player.maxConditionsForUpgrades)
                {
                    player.AllUpgrades[i].active = true;
                }
            }
            for (int i = 0; i < player.upgradesSlots.Count; i++)
            {
                player.upgradesSlots[i].upgrade = null;
                player.upgradesSlots[i].GetComponent<Image>().sprite = null;
                player.upgradesSlots[i].GetComponent<Image>().color = Color.white;
                player.upgradesSlots[i].fill = false;
            }
            for (int i = 0; i < player.AllUpgrades.Count; i++)
            {
                player.AllUpgrades[i].alreadySet = false;
            }
            GameObject.FindWithTag("Baby Capy").GetComponent<ItemButton>().idleItem.cost = GameObject.FindWithTag("Baby Capy").GetComponent<ItemButton>().idleItem.baseCost;
            GameObject.FindWithTag("God").GetComponent<ItemButton>().idleItem.cost = GameObject.FindWithTag("God").GetComponent<ItemButton>().idleItem.baseCost;
            GameObject.FindWithTag("Lab").GetComponent<ItemButton>().idleItem.cost = GameObject.FindWithTag("Lab").GetComponent<ItemButton>().idleItem.baseCost;
            GameObject.FindWithTag("Zoo").GetComponent<ItemButton>().idleItem.cost = GameObject.FindWithTag("Zoo").GetComponent<ItemButton>().idleItem.baseCost;
            GameObject.FindWithTag("Nature").GetComponent<ItemButton>().idleItem.cost = GameObject.FindWithTag("Nature").GetComponent<ItemButton>().idleItem.baseCost;
            GameObject.FindWithTag("Multiverse").GetComponent<ItemButton>().idleItem.cost = GameObject.FindWithTag("Multiverse").GetComponent<ItemButton>().idleItem.baseCost;
        }
    }

    public void SetUpgrade(PrestigeUpgrade up)
    {
        up.effectOnGoldenCapyProfit = new UBigNumber(up.stringEffectOnGoldenCapyProfit);
        base.SetUpgrade(up);
    }
}
