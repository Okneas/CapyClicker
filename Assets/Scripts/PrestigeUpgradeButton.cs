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
        // устанавливаем иконку улучшения в изображении
        if (prestigeUpgrade.icon != null)
        {
            description.transform.GetChild(0).GetComponent<Image>().sprite = prestigeUpgrade.icon;
        }
        description.transform.GetChild(1).GetComponent<Text>().text = prestigeUpgrade.name;// устанавливаем название улучшения в тексте
        description.transform.GetChild(2).GetComponent<Text>().text = $"Стоимость: {prestigeUpgrade.price}\n"; // устанавливаем стоимость улучшения в тексте
        if (prestigeUpgrade.effectOnGoldenCapyProfit != UBigNumber.ConvertToBigNumber(0))
        {
            description.transform.GetChild(2).GetComponent<Text>().text += $"Процент от золотых капибар +{prestigeUpgrade.stringEffectOnGoldenCapyProfit}%\n";
        }
        if (prestigeUpgrade.effectOnConditions != 0)
        {
            description.transform.GetChild(2).GetComponent<Text>().text += $"Разблокировка улучшений на +{prestigeUpgrade.effectOnConditions} строений\n";
        }
        if (prestigeUpgrade.effectOnCapyPerSec != UBigNumber.ConvertToBigNumber(0))// если у улучшения есть эффект на процент капитулы в секунду, то:
        {
            description.transform.GetChild(2).GetComponent<Text>().text += $"Весь доход +{prestigeUpgrade.stringEffectOnCapyPerSec}%\n";// устанавливаем текст с эффектом на процент капитулы в секунду
        }
        if (prestigeUpgrade.effectOnCapyPerSecOnItem != UBigNumber.ConvertToBigNumber(0))// если у улучшения есть эффект на процент капитулы в секунду для определенного предмета, то:
        {
            description.transform.GetChild(2).GetComponent<Text>().text += $"Доход строения {upgrade.GetStringItemType()} +{prestigeUpgrade.stringEffectOnCapyPerSecOnItem}%\n";// устанавливаем текст с эффектом на процент капитулы в секунду для определенного предмета
        }
        if (prestigeUpgrade.effectOnTap != UBigNumber.ConvertToBigNumber(0)) // если у улучшения есть эффект на процент дохода за клик, то:
        {
            description.transform.GetChild(2).GetComponent<Text>().text += $"Увеличение дохода за клик +{prestigeUpgrade.stringEffectOnTap}%\n";
        }
        if (prestigeUpgrade.effectOnTapFromAll != UBigNumber.ConvertToBigNumber(0))// если у улучшения есть эфффект на процент дохода за клик от дохода, то:
        {
            description.transform.GetChild(2).GetComponent<Text>().text += $"Увеличение дохода за клик от дохода +{prestigeUpgrade.stringEffectOnTapFromAll}%\n";// устанавливаем текст с эффектом на процент дохода за клик
        }
        if (prestigeUpgrade.active)
        {
            description.transform.GetChild(2).GetComponent<Text>().text += $"Доступен";
        }
        else
        {
            description.transform.GetChild(2).GetComponent<Text>().text += $"Не доступен";
        }
        tempDes = Instantiate(description, mainCanvas.transform);// создаем временный объект, который будет отображать описание улучшения
        tempDes.transform.position = new Vector2(Input.mousePosition.x - 352, Input.mousePosition.y - 152);// устанавливаем позицию временного объекта в координатах мыши
        tempDes.transform.SetParent(mainCanvas.transform);// устанавливаем родительский объект временного объекта в канвасе
    }
    public override void OnPointerClick(PointerEventData eventData) // метод, который вызывается при нажатии на кнопку улучшения
    {
        if (prestigeUpgrade.active) // если кнопка улучшения заполнена, то:
        {
            if (prestigeUpgrade.price <= player.goldenCapy) // если у игрока достаточно капитул, то:
            {
                player.PrestigeAllUpgradesObtained.Add(prestigeUpgrade);
                for (int i = 0; i < player.AllPrestigeUpgrades.Count; i++)
                {
                    player.AllPrestigeUpgrades[i].CheckConditions();
                }
                prestigeUpgrade.active = false; // устанавливаем флаг активности улучшения в false
                player.goldenCapy -= prestigeUpgrade.price; // уменьшаем количество капитул у игрока на стоимость улучшения
                prestigeUpgrade.DoUpgrade(); // вызываем метод DoUpgrade у объекта улучшения для изменения его параметров
                player.UpdateStatistic(); // вызываем метод UpdateStatistic у объекта игрока для обновления его статистики
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
