using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Upgrades upgrade; // ���������� ��� �������� ������ �� ������ ���������
    public Player player; // ���������� ��� �������� ������ �� ������ ������
    public bool fill = false; // ����, �����������, ��������� �� ������ ���������
    public GameObject description; // ������, ������� ����� ���������� �������� ��������� ��� ��������� �� ������
    public GameObject tempDes; // ��������� ������, ������� ����� ���������� �������� ���������
    public Canvas mainCanvas; // ������, ������� ����� �������� �������� ��� ����������� �������� ���������

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>(); // ������� ������ ������ � ��������� ������ �� ���� � ���������� player
        fill = false; // ������������� ���� fill � false
        if (upgrade != null) // ���� � ������ ��������� ���� ������ ���������, ��:
        {
            SetUpgrade(upgrade); // �������� ����� SetUpgrade ��� ��������� ��������� �� ������
        }
    }

    public virtual void OnPointerEnter(PointerEventData eventData)// �����, ������� ���������� ��� ��������� �� ������ �����
    {
        if (fill)// ���� ������ ��������� ���������, ��:
        {
            description.transform.GetChild(0).GetComponent<Image>().sprite = upgrade.icon; // ������������� ������ ��������� � �����������
            description.transform.GetChild(0).GetComponent<Image>().color = Color.white;
            description.transform.GetChild(1).GetComponent<Text>().text = upgrade.name;// ������������� �������� ��������� � ������
            description.transform.GetChild(2).GetComponent<Text>().text = $"���������: {upgrade.price}\n"; // ������������� ��������� ��������� � ������
            if (upgrade.effectOnCapyPerSec != UBigNumber.ConvertToBigNumber(0))// ���� � ��������� ���� ������ �� ������� �������� � �������, ��:
            {
                description.transform.GetChild(2).GetComponent<Text>().text += $"���� ����� +{upgrade.stringEffectOnCapyPerSec}%\n";// ������������� ����� � �������� �� ������� �������� � �������
            }
            if (upgrade.effectOnCapyPerSecOnItem != UBigNumber.ConvertToBigNumber(0))// ���� � ��������� ���� ������ �� ������� �������� � ������� ��� ������������� ��������, ��:
            {
                description.transform.GetChild(2).GetComponent<Text>().text += $"����� �������� {upgrade.GetStringItemType()} +{upgrade.stringEffectOnCapyPerSecOnItem}%\n";// ������������� ����� � �������� �� ������� �������� � ������� ��� ������������� ��������
            }
            if (upgrade.effectOnTap != UBigNumber.ConvertToBigNumber(0)) // ���� � ��������� ���� ������ �� ������� ������ �� ����, ��:
            {
                description.transform.GetChild(2).GetComponent<Text>().text += $"���������� ������ �� ���� +{upgrade.stringEffectOnTap}%\n";
            }
            if (upgrade.effectOnTapFromAll != UBigNumber.ConvertToBigNumber(0))// ���� � ��������� ���� ������� �� ������� ������ �� ���� �� ������, ��:
            {
                description.transform.GetChild(2).GetComponent<Text>().text += $"���������� ������ �� ���� �� ������ +{upgrade.stringEffectOnTapFromAll}%\n";// ������������� ����� � �������� �� ������� ������ �� ����
            }
            tempDes = Instantiate(description,mainCanvas.transform);// ������� ��������� ������, ������� ����� ���������� �������� ���������
            tempDes.transform.position = new Vector2(Input.mousePosition.x - 352, Input.mousePosition.y - 152);// ������������� ������� ���������� ������� � ����������� ����
            tempDes.transform.SetParent(mainCanvas.transform);// ������������� ������������ ������ ���������� ������� � �������
        }
    }


    public void OnPointerExit(PointerEventData eventData)// �����, ������� ���������� ��� ������ ������� ���� � ������ ���������
    {
        Destroy(tempDes);// ������� ��������� ������, ������� ���������� �������� ���������
    }

    public virtual void OnPointerClick(PointerEventData eventData) // �����, ������� ���������� ��� ������� �� ������ ���������
    {
        if (fill) // ���� ������ ��������� ���������, ��:
        {
            if (upgrade.price <= player.capyPoints) // ���� � ������ ���������� �������, ��:
            {
                upgrade.active = false; // ������������� ���� ���������� ��������� � false
                upgrade.alreadySet = false; // ������������� ���� ��������� ��������� � false
                player.capyPoints -= upgrade.price; // ��������� ���������� ������� � ������ �� ��������� ���������
                upgrade.DoUpgrade(); // �������� ����� DoUpgrade � ������� ��������� ��� ��������� ��� ����������
                player.UpdateStatistic(); // �������� ����� UpdateStatistic � ������� ������ ��� ���������� ��� ����������
                upgrade = null; // ������������� ������ ��������� � null
                gameObject.GetComponent<Image>().sprite = null; // ������������� ������ ������ ��������� � null
                gameObject.GetComponent<Image>().color = Color.white; // ������������� ���� ������ ��������� � �����
                fill = false; // ������������� ���� fill � false
            }
        }
    }

    public virtual void SetUpgrade(Upgrades up)// ����� ��� ��������� ��������� �� ������
    {
        up.price = new UBigNumber(up.stringPrice); // ����������� ��������� �������� ��������� ��������� � ������ UBigNumber
        up.effectOnCapyPerSec = new UBigNumber(up.stringEffectOnCapyPerSec); // ����������� ��������� �������� ������� �� ������� �������� � ������� � ������ UBigNumber
        up.effectOnCapyPerSecOnItem = new UBigNumber(up.stringEffectOnCapyPerSecOnItem); // ����������� ��������� �������� ������� �� ������� �������� � ������� ��� ������������� �������� � ������ UBigNumber
        up.effectOnTap = new UBigNumber(up.stringEffectOnTap); // ����������� ��������� �������� ������� �� ������� ������ �� ���� � ������ UBigNumber
        up.effectOnTapFromAll = new UBigNumber(up.stringEffectOnTapFromAll); // ����������� ��������� �������� ������� �� ������� ������ �� ���� �� ������ � ������ UBigNumber
        upgrade = up;// ������������� ������ ��������� � ���������� upgrade
        if (upgrade.icon != null)// ���� � ��������� ���� ������, ��:
        {
            GetComponent<Image>().sprite = GetComponent<UpgradeButton>().upgrade.icon;// ������������� ������ ��������� �� ������
        }
        fill = true;// ������������� ���� fill � true
    }
}
