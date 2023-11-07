using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class changeHoveredButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Color notHoveredColor;
    public Color hoveredColor;
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        Image image = GetComponent<Image>();
        image.color = hoveredColor;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        Image image = GetComponent<Image>();
        image.color = notHoveredColor;
    }
}

