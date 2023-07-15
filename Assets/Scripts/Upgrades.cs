using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Entity/Upgrade")]
public class Upgrades: ScriptableObject
{
    public int id; // ������������� ���������
    public Sprite icon; // ������ ���������
    public string stringPrice; // ������, ���������� ���� ���������
    public UBigNumber price; // ���������� ��� �������� �������� ���� ���������
    public int condition; // ������� ��� ��������� ���������
    public string stringEffectOnTapFromAll; // ������, ���������� ������, ������� ����� ������������� ��� ������� �� ���������
    public string stringEffectOnTap; // ������, ���������� ������, ������� ����� ������������� ��� ������� �� ���������
    public string stringEffectOnCapyPerSec; // ������, ���������� ������, ������� ����� ������������� ��� ������� �� ���������
    public string stringEffectOnCapyPerSecOnItem; // ������, ���������� ������, ������� ����� ������������� ��� ������� �� ���������
    public UBigNumber effectOnTap; // ���������� ��� �������� �������� �������, ������� ����� ������������� ��� ������� �� ���������
    public UBigNumber effectOnCapyPerSec; // ���������� ��� �������� �������� �������, ������� ����� ������������� ��� ������� �� ���������
    public UBigNumber effectOnCapyPerSecOnItem; // ���������� ��� �������� �������� �������, ������� ����� ������������� ��� ������� �� ���������
    public UBigNumber effectOnTapFromAll; // ���������� ��� �������� �������� �������, ������� ����� ������������� ��� ������� �� ���������
    public ItemType itemType; // ��� ��������, ��� �������� ����� ������������� ���������
    public bool active; // ����, �����������, ������������ �� ���������
    public bool alreadySet = false; // ����, �����������, ���� �� ��������� ��� �����������
    public delegate void UpgradeJob(); // ������� ��� ���������� ������ ���������

    // �����, ������� ���������� ��� ��������� ���������
    public virtual void DoUpgrade()
    {
        UpgradeJob job = () => {};
        // ���� ��������� ����� ������ ��� ������� �� ��� ��������, ��:
        if (effectOnTap != UBigNumber.ConvertToBigNumber(0))
        {
            job += ChangeEffectOnTap;
            job.Invoke();
            job -= ChangeEffectOnTap;
        }
        // ���� ��������� ����� ������ ��� ������� �� �������, ��:
        if (effectOnCapyPerSec != UBigNumber.ConvertToBigNumber(0))
        {
            job += ChangeEffectOnCapyPerSec;
            job.Invoke();
            job -= ChangeEffectOnCapyPerSec;
        }
        // ���� ��������� ����� ������ ��� ������� �� ��������, ��:
        if (effectOnCapyPerSecOnItem != UBigNumber.ConvertToBigNumber(0))
        {
            job += ChangeEffectOnCapyPerSecOnItem;
            job.Invoke();
            job -= ChangeEffectOnCapyPerSecOnItem;
        }
        // ���� ��������� ����� ������ ��� ������� �� �������� ��� ������������� ��������, ��:
        if (effectOnTapFromAll != UBigNumber.ConvertToBigNumber(0))
        {
            job += ChangeEffectOnTapFromAll;
            job.Invoke();
            job -= ChangeEffectOnTapFromAll;
        }
    }

    // �����, ������� ���������� ��� ��������� ������� ��� ������� �� ��� ��������
    public void ChangeEffectOnTap()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().percentTap += effectOnTap;
    }
    // �����, ������� ���������� ��� ��������� ������� ��� ������� �� �������
    public void ChangeEffectOnCapyPerSec()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().percentAll += effectOnCapyPerSec;
    }
    // �����, ������� ���������� ��� ��������� ������� ��� ������� �� �������� ��� ������������� ��������
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
    // �����, ������� ���������� ��� ��������� ������� ��� ������� �� ��� ��������
    public void ChangeEffectOnTapFromAll()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().percentTapFromAll += effectOnTapFromAll;
    }

    public string GetStringItemType()
    {
        switch (itemType)
        {
            case ItemType.BabyCapy:
                return "����� ��������";
            case ItemType.Zoo:
                return "��������";
            case ItemType.Nature:
                return "����� �������";
            case ItemType.Lab:
                return "������ �����������";
            case ItemType.God:
                return "��� �������";
            case ItemType.MultiUniverse:
                return "���������������";
            default:
                return "";
        }
    }
}
