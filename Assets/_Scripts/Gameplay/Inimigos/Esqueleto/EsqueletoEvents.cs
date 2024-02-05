using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsqueletoEvents : InimigoEvents
{
    public delegate void MovimentoEsqueleto(int i);
    public event MovimentoEsqueleto podeMover;
    public event MovimentoEsqueleto podeVirar;

    private bool podeAtacar = true;

    public override void NewAwake()
    {
        base.NewAwake();
        getBase().Attack += Ataque;
        podeMover += getBase().PodeSeMover;
        podeVirar += getBase().PodeVirar;
    }

    void Ataque()
    {
        if (podeAtacar)
        {
            getBase().anim.Play("Attack");
            podeAtacar = false;
        }
        
    }

    void OnAttacked()
    {
        podeAtacar = true;
    }

    void PodeSeMover(int i)
    {
        podeMover(i);
    }

    void PodeSeVirar(int i)
    {
        podeVirar(i);
    }
}
