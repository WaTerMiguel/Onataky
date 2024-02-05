using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomButtonBase : MonoBehaviour
{
    public Animator anim;
    private void Start()
    {
        anim.Play("OnStatic");
    }

    private void OnMouseEnter()
    {
        anim.Play("OnMove");
    }

    private void OnMouseExit()
    {
        anim.Play("OnStatic");
    }
}
