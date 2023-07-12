using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Entity/Upgrade")]
public class Upgrades: ScriptableObject
{
    public Sprite icon;
    public string stringPrice;
    public UBigNumber price;
    public int condition;
    public string stringEffectOnTapFromAll;
    public string stringEffectOnTap;
    public string stringEffectOnCapyPerSec;
    public string stringEffectOnCapyPerSecOnItem;
    public UBigNumber effectOnTap;
    public UBigNumber effectOnCapyPerSec;
    public UBigNumber effectOnCapyPerSecOnItem;
    public UBigNumber effectOnTapFromAll;
    public ItemType itemType;

    public void DoUpgrade()
    {
        if(effectOnTap != UBigNumber.ConvertToBigNumber(0))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().percentTap += effectOnTap;
        }
        if (effectOnCapyPerSec != UBigNumber.ConvertToBigNumber(0))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().percentAll += effectOnCapyPerSec;
        }
        if (effectOnCapyPerSecOnItem != UBigNumber.ConvertToBigNumber(0))
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
        if (effectOnTapFromAll != UBigNumber.ConvertToBigNumber(0))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().percentTapFromAll += effectOnTapFromAll;
        }
    }
}
