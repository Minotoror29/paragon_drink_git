using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomButton : CustomUIElement, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float scaleFactor = 1.25f;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Select();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Deselect();
    }

    public override void Select()
    {
        base.Select();

        transform.localScale = Vector3.one * scaleFactor;
    }

    public override void Deselect()
    {
        base.Deselect();

        transform.localScale = Vector3.one;
    }
}
