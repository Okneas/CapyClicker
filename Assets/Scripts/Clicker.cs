using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Clicker : MonoBehaviour, IPointerClickHandler
{
    float timer = 0;

    public Player player;
    public void OnPointerClick(PointerEventData eventData)
    {
        player.capyPoints += player.capyPointsPerTap;
    }
    private void Update()
    {
        if (timer < 1)
            timer += Time.deltaTime;

        if (timer > 1)
        {
            Sum();
            timer = 0;
        }
    }

    public void Sum()
    {
        player.capyPoints += player.capyPointsPerSecond;
    }
}
