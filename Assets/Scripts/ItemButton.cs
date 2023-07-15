using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour, IPointerClickHandler
{
    public ItemType itemType; // тип предмета
    public Text description; // текстовое поле для отображения описания предмета
    public string itemName; // название предмета
    public string price; // цена предмета
    public string stringCapyPerSecond; // количество капитул, получаемое за секунду
    public UBigNumber capyPerSecond; // переменная для хранения значения количества капитул
    public IdleItems idleItem; // переменная для хранения объекта класса IdleItems
    public Player player; // переменная для хранения объекта класса Player

    // Метод, который вызывается при нажатии на кнопку
    public void OnPointerClick(PointerEventData eventData)
    {
        // Если количество капитул, необходимое для покупки предмета, меньше или равно количеству капитул у игрока, то:
        if (idleItem.cost <= player.capyPoints)
        {
            // Уменьшаем количество капитул у игрока на стоимость предмета
            idleItem.PriceChange();
            // Обновляем статистику игрока
            player.UpdateStatistic();
            // Проверяем, есть ли у игрока неактивированные улучшения, которые можно активировать
            foreach (Upgrades i in player.AllUpgrades)
            {
                if (!i.alreadySet)
                {
                    if (i.condition + 1 == player.GetCountByType(i.itemType) && i.active)
                    {
                        // Если есть, то активируем его и обновляем статистику игрока
                        i.alreadySet = true;
                        player.SetUpgrade(i);
                        break;
                    }
                }
            }
        }
    }
    // Метод, который вызывается при запуске игры
    void Awake()
    {
        // Преобразуем строку, содержащую количество капитул, в объект класса UBigNumber
        capyPerSecond = new UBigNumber(stringCapyPerSecond);
        // В зависимости от типа предмета создаем объект класса IdleItems соответствующего типа
        switch (itemType)
        {
            case(ItemType.BabyCapy):
                idleItem = new BabyCapy(itemName, price, capyPerSecond);
                break;
            case (ItemType.God):
                idleItem = new God(itemName, price, capyPerSecond);
                break;
            case (ItemType.Lab):
                idleItem = new Lab(itemName, price, capyPerSecond);
                break;
            case (ItemType.MultiUniverse):
                idleItem = new Multiverse(itemName, price, capyPerSecond);
                break;
            case (ItemType.Nature):
                idleItem = new Nature(itemName, price, capyPerSecond);
                break;
            case (ItemType.Zoo):
                idleItem = new Zoo(itemName, price, capyPerSecond);
                break;
        }
    }

    // Метод, который вызывается при каждом обновлении игры
    void Update()
    {
        // Обновляем описание предмета
        UpdateDiscription();
    }

    // Метод, который обновляет описание предмета
    public void UpdateDiscription()
    {
        // Отображаем описание предмета в текстовом поле
        description.text = $"Название: {idleItem.name}\nЦена: {idleItem.cost.BeautifulString()} Всего куплено: {idleItem.count-1}";
    }
}
