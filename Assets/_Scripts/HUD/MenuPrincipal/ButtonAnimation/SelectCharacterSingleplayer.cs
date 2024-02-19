using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacterSingleplayer : CustomButtonBase
{
    [SerializeField] Animator anim;
    [SerializeField] GameObject telaEscura;

    private void Start()
    {
        anim.Play("OnStatic");
        telaEscura.SetActive(true);

    }

    public override void MouseEntrou()
    {
        anim.Play("OnMove");
        telaEscura.SetActive(false);
    }

    public override void MouseSaiu()
    {
        anim.Play("OnStatic");
        telaEscura.SetActive(true);
    }
}
