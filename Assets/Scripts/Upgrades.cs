using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Entity/Upgrade")]
public class Upgrades: ScriptableObject
{
    public int id; // идентификатор улучшения
    public Sprite icon; // иконка улучшения
    public string stringPrice; // строка, содержащая цену улучшения
    public UBigNumber price; // переменная для хранения значения цены улучшения
    public int condition; // условие для активации улучшения
    public string stringEffectOnTapFromAll; // строка, содержащая эффект, который будет производиться при нажатии на улучшение
    public string stringEffectOnTap; // строка, содержащая эффект, который будет производиться при нажатии на улучшение
    public string stringEffectOnCapyPerSec; // строка, содержащая эффект, который будет производиться при нажатии на улучшение
    public string stringEffectOnCapyPerSecOnItem; // строка, содержащая эффект, который будет производиться при нажатии на улучшение
    public UBigNumber effectOnTap; // переменная для хранения значения эффекта, который будет производиться при нажатии на улучшение
    public UBigNumber effectOnCapyPerSec; // переменная для хранения значения эффекта, который будет производиться при нажатии на улучшение
    public UBigNumber effectOnCapyPerSecOnItem; // переменная для хранения значения эффекта, который будет производиться при нажатии на улучшение
    public UBigNumber effectOnTapFromAll; // переменная для хранения значения эффекта, который будет производиться при нажатии на улучшение
    public ItemType itemType; // тип предмета, для которого будет производиться улучшение
    public bool active; // флаг, указывающий, активировано ли улучшение
    public bool alreadySet = false; // флаг, указывающий, было ли улучшение уже установлено
    public delegate void UpgradeJob(); // делегат для выполнения задачи улучшения

    // Метод, который вызывается при установке улучшения
    public virtual void DoUpgrade()
    {
        UpgradeJob job = () => {};
        // Если улучшение имеет эффект при нажатии на все предметы, то:
        if (effectOnTap != UBigNumber.ConvertToBigNumber(0))
        {
            job += ChangeEffectOnTap;
            job.Invoke();
            job -= ChangeEffectOnTap;
        }
        // Если улучшение имеет эффект при нажатии на предмет, то:
        if (effectOnCapyPerSec != UBigNumber.ConvertToBigNumber(0))
        {
            job += ChangeEffectOnCapyPerSec;
            job.Invoke();
            job -= ChangeEffectOnCapyPerSec;
        }
        // Если улучшение имеет эффект при нажатии на капитулу, то:
        if (effectOnCapyPerSecOnItem != UBigNumber.ConvertToBigNumber(0))
        {
            job += ChangeEffectOnCapyPerSecOnItem;
            job.Invoke();
            job -= ChangeEffectOnCapyPerSecOnItem;
        }
        // Если улучшение имеет эффект при нажатии на капитулу для определенного предмета, то:
        if (effectOnTapFromAll != UBigNumber.ConvertToBigNumber(0))
        {
            job += ChangeEffectOnTapFromAll;
            job.Invoke();
            job -= ChangeEffectOnTapFromAll;
        }
    }

    // Метод, который вызывается при изменении эффекта при нажатии на все предметы
    public void ChangeEffectOnTap()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().percentTap += effectOnTap;
    }
    // Метод, который вызывается при изменении эффекта при нажатии на предмет
    public void ChangeEffectOnCapyPerSec()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().percentAll += effectOnCapyPerSec;
    }
    // Метод, который вызывается при изменении эффекта при нажатии на капитулу для определенного предмета
    public void ChangeEffectOnCapyPerSecOnItem()
    {
        switch (itemType)
        {
            case ItemType.BabyCapy:
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().percentBabyCapy += effectOnCapyPerSecOnItem;
                break;
            case ItemType.Zoo:
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().percentZoo += effectOnCapyPerSecOnItem;
                break;
            case ItemType.Nature:
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().percentNature += effectOnCapyPerSecOnItem;
                break;
            case ItemType.Lab:
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().percentLab += effectOnCapyPerSecOnItem;
                break;
            case ItemType.God:
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().percentGod += effectOnCapyPerSecOnItem;
                break;
            case ItemType.MultiUniverse:
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().percentMultiverse += effectOnCapyPerSecOnItem;
                break;
        }
    }
    // Метод, который вызывается при изменении эффекта при нажатии на все предметы
    public void ChangeEffectOnTapFromAll()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().percentTapFromAll += effectOnTapFromAll;
    }

    public string GetStringItemType()
    {
        switch (itemType)
        {
            case ItemType.BabyCapy:
                return "Малыш капибара";
            case ItemType.Zoo:
                return "Зоопарки";
            case ItemType.Nature:
                return "Дикая природа";
            case ItemType.Lab:
                return "Генная лаборатория";
            case ItemType.God:
                return "Бог капибар";
            case ItemType.MultiUniverse:
                return "Мультивселенная";
            default:
                return "";
        }
    }
}
