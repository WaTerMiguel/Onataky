using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CustomButtonBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Animator anim;
    private void Start()
    {
        anim.Play("OnStatic");
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        anim.Play("OnMove");
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        anim.Play("OnStatic");
    }
}
