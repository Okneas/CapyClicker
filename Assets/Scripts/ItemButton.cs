using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour, IPointerClickHandler
{
    public ItemType itemType; // ��� ��������
    public Text description; // ��������� ���� ��� ����������� �������� ��������
    public string itemName; // �������� ��������
    public string price; // ���� ��������
    public string stringCapyPerSecond; // ���������� �������, ���������� �� �������
    public UBigNumber capyPerSecond; // ���������� ��� �������� �������� ���������� �������
    public IdleItems idleItem; // ���������� ��� �������� ������� ������ IdleItems
    public Player player; // ���������� ��� �������� ������� ������ Player

    // �����, ������� ���������� ��� ������� �� ������
    public void OnPointerClick(PointerEventData eventData)
    {
        // ���� ���������� �������, ����������� ��� ������� ��������, ������ ��� ����� ���������� ������� � ������, ��:
        if (idleItem.cost <= player.capyPoints)
        {
            // ��������� ���������� ������� � ������ �� ��������� ��������
            idleItem.PriceChange();
            // ��������� ���������� ������
            player.UpdateStatistic();
            // ���������, ���� �� � ������ ���������������� ���������, ������� ����� ������������
            foreach (Upgrades i in player.AllUpgrades)
            {
                if (!i.alreadySet)
                {
                    if (i.condition + 1 == player.GetCountByType(i.itemType) && i.active)
                    {
                        // ���� ����, �� ���������� ��� � ��������� ���������� ������
                        i.alreadySet = true;
                        player.SetUpgrade(i);
                        break;
                    }
                }
            }
        }
    }
    // �����, ������� ���������� ��� ������� ����
    void Awake()
    {
        // ����������� ������, ���������� ���������� �������, � ������ ������ UBigNumber
        capyPerSecond = new UBigNumber(stringCapyPerSecond);
        // � ����������� �� ���� �������� ������� ������ ������ IdleItems ���������������� ����
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

    // �����, ������� ���������� ��� ������ ���������� ����
    void Update()
    {
        // ��������� �������� ��������
        UpdateDiscription();
    }

    // �����, ������� ��������� �������� ��������
    public void UpdateDiscription()
    {
        // ���������� �������� �������� � ��������� ����
        description.text = $"��������: {idleItem.name}\n����: {idleItem.cost.BeautifulString()} ����� �������: {idleItem.count-1}";
    }
}
