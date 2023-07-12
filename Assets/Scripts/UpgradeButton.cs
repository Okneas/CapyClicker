using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour, IPointerClickHandler
{
    public Upgrades upgrade;
    public Player player;
    public bool fill = false;

    private void Start()
    {
        fill = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (fill)
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
