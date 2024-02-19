using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacterMultiPlayer : CustomButtonBase
{
    [SerializeField] Animator anim;
    [SerializeField] GameObject telaEscura, playerUm;

    private void Start()
    {
        anim.Play("OnStatic");
        telaEscura.SetActive(true);

    }

    public override void MouseEntrou()
    {
        anim.Play("OnMove");
        telaEscura.SetActive(false);
        playerUm.SetActive(true);
        playerUm.transform.localPosition = transform.localPosition;
    }

    public override void MouseSaiu()
    {
        anim.Play("OnStatic");
        telaEscura.SetActive(true);
    }
}
