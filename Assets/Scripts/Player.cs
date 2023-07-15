using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Player : MonoBehaviour
{
    // Свойства для хранения значений переменных
    public UBigNumber capyPoints = new UBigNumber("0"); // количество капидолларов
    public UBigNumber AllCapyPointsPerRun = new UBigNumber("0"); // общее количество капидолларов за все игры
    public UBigNumber maxCapyDollars = new UBigNumber("0"); // максимальное количество капидолларов, которое можно получить за один раз
    public UBigNumber capyPointsPerSecond = new UBigNumber("0"); // количество капидолларов, получаемое за каждую секунду
    public UBigNumber capyPointsPerTap = new UBigNumber("1"); // количество капидолларов, получаемое за каждый нажатие на кнопку

    public UBigNumber goldenCapy = new UBigNumber("0"); // количество золотых капибар
    public UBigNumber profitOfgoldenCapy = new UBigNumber("1");
    public UBigNumber goldenCapyOnThisRun = new UBigNumber("0"); // количество золотых капибар, полученных на этой итерации
    public UBigNumber capyDollarsForGoldenCapy = new UBigNumber("1000000000"); // количество капидолларов, необходимое для получения золотой капибары

    public Text textCapyPoints; // компонент текста для отображения количества капидолларов
    public Text textCapyPointsPerSecond; // компонент текста для отображения количества капидолларов, получаемое за каждую секунду
    public Text textGoldenCapyOnThisRun; // компонент текста для отображения количества золотых капибар
    public Text textAllGoldenCapy;

    public int maxConditionsForUpgrades = 50; // максимальное количество условий для улучшений

    public int countOfBabyCapyUpgrades; // количество улучшений "ребенка"
    public int countOfZooUpgrades; // количество улучшений "зоопарка"
    public int countOfNatureUpgrades; // количество улучшений "природа"
    public int countOfLabUpgrades; // количество улучшений "лаборатория"
    public int countOfGodUpgrades; // количество улучшений "бог"
    public int countOfMultiverseUpgrades; // количество улучшений "мультиверсия"

    public UBigNumber capyPointsPerSecondBabyCapy; // количество капидолларов, получаемое за каждую секунду при улучшении "ребенка"
    public UBigNumber capyPointsPerSecondZoo; // количество капиткапидолларовул, получаемое за каждую секунду при улучшении "зоопарка"
    public UBigNumber capyPointsPerSecondNature; // количество капидолларов, получаемое за каждую секунду при улучшении "природа"
    public UBigNumber capyPointsPerSecondLab; // количество капидолларов, получаемое за каждую секунду при улучшении "лаборатория"
    public UBigNumber capyPointsPerSecondGod; // количество капидолларов, получаемое за каждую секунду при улучшении "бог"
    public UBigNumber capyPointsPerSecondMultiverse; // количество капидолларов, получаемое за каждую секунду при улучшении "мультиверсия"

    public List<Upgrades> AllUpgrades; // список всех улучшений
    public List<PrestigeUpgrade> PrestigeAllUpgradesObtained;
    public List<PrestigeUpgrade> AllPrestigeUpgrades;

    public List<UpgradeButton> upgradesSlots; // список кнопок улучшений

    public UBigNumber percentAll = new UBigNumber("0");// процент "все"
    public UBigNumber percentBabyCapy = new UBigNumber("0"); // процент "ребенка"
    public UBigNumber percentZoo = new UBigNumber("0"); // процент  "зоопарка"
    public UBigNumber percentNature = new UBigNumber("0"); // процент  "природа"
    public UBigNumber percentLab = new UBigNumber("0"); // процент  "лаборатория"
    public UBigNumber percentGod = new UBigNumber("0"); // процент  "бог"
    public UBigNumber percentMultiverse = new UBigNumber("0"); // процент  "мультивселенная"
    public UBigNumber percentTap = new UBigNumber("100"); // процент для капидолларов, получаемый за каждый нажатие на кнопку
    public UBigNumber percentTapFromAll = new UBigNumber("0"); // процент капидолларов, получаемый за каждый нажатие на кнопку, если все улучшения включены

    // Метод, который вызывается при запуске игры
    private void Awake()
    {
        foreach (PrestigeUpgrade i in AllPrestigeUpgrades)
        {
            i.CheckConditions();
        }
        // Включение улучшений в зависимости от их условий
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
        // Поиск панели улучшений и добавление в список кнопок улучшений
        upgradePanel = GameObject.FindGameObjectWithTag("Upgrade");
        for (int i = 0; i < GameObject.FindGameObjectWithTag("Upgrade").transform.childCount; i++)
        {
           upgradesSlots.Add(upgradePanel.transform.GetChild(i).GetComponent<UpgradeButton>());
        }
    }

    // Метод, который вызывается каждый кадр
    private void Update()
    {
        // Увеличение количества золотых капибар на этой итерации, если достигнута определенная сумма
        if (AllCapyPointsPerRun > UBigNumber.IntPow(goldenCapyOnThisRun + UBigNumber.ConvertToBigNumber(1), 3) * UBigNumber.ConvertToBigNumber(1000000))
        {
            goldenCapyOnThisRun++;
        }
        // Обновление текстовых компонентов на экране
        textCapyPoints.text = $"{capyPoints.BeautifulString()} CapyDollars";
        textCapyPointsPerSecond.text = $"{capyPointsPerSecond.BeautifulString()} CapyDollars Per Second";
        textGoldenCapyOnThisRun.text = $"+{goldenCapyOnThisRun.BeautifulString()} Golden Capy's On Prestige";
        textAllGoldenCapy.text = $"{goldenCapy.BeautifulString()} all Golden Capy's";

    }
    // Метод, который устанавливает улучшение
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

    // Метод, который обновляет статистику
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
