using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TreeButton : MonoBehaviour, IPointerClickHandler
{
    public Canvas mainCan;
    public Canvas treeCan;

    public void ChangeOnTreeCanvas()
    {
        mainCan.GetComponent<Canvas>().sortingOrder = 0;
        treeCan.GetComponent<Canvas>().sortingOrder = 1;
    }
    public void ChangeOnMainCanvas()
    {
        treeCan.GetComponent<Canvas>().sortingOrder = 0;
        mainCan.GetComponent<Canvas>().sortingOrder = 1;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(mainCan.GetComponent<Canvas>().sortingOrder > treeCan.GetComponent<Canvas>().sortingOrder)
        {
            ChangeOnTreeCanvas();
        }
        else
        {
            ChangeOnMainCanvas();
        }
    }
}
