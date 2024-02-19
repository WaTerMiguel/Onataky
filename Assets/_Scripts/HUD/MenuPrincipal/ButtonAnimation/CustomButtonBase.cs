using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CustomButtonBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        MouseEntrou();
    }

    public virtual void MouseEntrou()
    {
        
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        MouseSaiu();
    }

    public virtual void MouseSaiu()
    {

    }

    void IPointerMoveHandler.OnPointerMove(PointerEventData eventData)
    {
        MouseMexeu();
    }

    public virtual void MouseMexeu()
    {

    }
}
