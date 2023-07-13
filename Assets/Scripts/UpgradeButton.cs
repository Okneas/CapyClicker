using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Upgrades upgrade;
    public Player player;
    public bool fill = false;
    public GameObject description;
    GameObject tempDes;
    public Canvas mainCanvas;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        fill = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (fill)
        {
            description.transform.GetChild(0).GetComponent<Image>().sprite = upgrade.icon;
            description.transform.GetChild(1).GetComponent<Text>().text = upgrade.name;
            description.transform.GetChild(2).GetComponent<Text>().text = $"Стоимость: {upgrade.price}\n";
            if(upgrade.effectOnCapyPerSec != UBigNumber.ConvertToBigNumber(0))
            {
                description.transform.GetChild(2).GetComponent<Text>().text += $"Весь доход +{upgrade.stringEffectOnCapyPerSec}%\n";
            }
            if (upgrade.effectOnCapyPerSecOnItem != UBigNumber.ConvertToBigNumber(0))
            {
                description.transform.GetChild(2).GetComponent<Text>().text += $"доход строения +{upgrade.stringEffectOnCapyPerSecOnItem}%\n";
            }
            if (upgrade.effectOnTap != UBigNumber.ConvertToBigNumber(0))
            {
                description.transform.GetChild(2).GetComponent<Text>().text += $"Увеличение дохода за клик +{upgrade.stringEffectOnTap}%\n";
            }
            if (upgrade.effectOnTapFromAll != UBigNumber.ConvertToBigNumber(0))
            {
                description.transform.GetChild(2).GetComponent<Text>().text += $"Увеличение дохода за клик от дохода +{upgrade.stringEffectOnTapFromAll}%\n";
            }
            tempDes = Instantiate(description,mainCanvas.transform);
            tempDes.transform.position = new Vector2(Input.mousePosition.x - 352, Input.mousePosition.y - 152);
            tempDes.transform.SetParent(mainCanvas.transform);
        }
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(tempDes);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (fill)
        {
            try
            {
                if (upgrade.price <= player.capyPoints)
                {
                    player.capyPoints -= upgrade.price;
                    upgrade.DoUpgrade();
                    player.UpdateStatistic();
                    upgrade = null;
                    gameObject.GetComponent<Image>().sprite = null;
                    gameObject.GetComponent<Image>().color = Color.white;
                    fill = false;
                }
            }
            catch
            {
                Debug.Log($"{player.capyPoints}");
            }
        }
    }

    public void SetUpgrade(Upgrades up)
    {
        up.price = new UBigNumber(up.stringPrice);
        up.effectOnCapyPerSec = new UBigNumber(up.stringEffectOnCapyPerSec);
        up.effectOnCapyPerSecOnItem = new UBigNumber(up.stringEffectOnCapyPerSecOnItem);
        up.effectOnTap = new UBigNumber(up.stringEffectOnTap);
        up.effectOnTapFromAll = new UBigNumber(up.stringEffectOnTapFromAll);
        this.upgrade = up;
        fill = true;
    }
}
