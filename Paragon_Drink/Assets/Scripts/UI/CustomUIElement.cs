using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomUIElement : MonoBehaviour
{
    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            Select();
        } else
        {
            Deselect();
        }
    }

    public virtual void Select()
    {
        
    }

    public virtual void Deselect()
    {

    }
}
