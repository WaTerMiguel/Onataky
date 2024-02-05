using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEvents : InimigoEvents
{
    [SerializeField] Transform corpo;
    private Vector3 posInicial;
    public float tempoAtePlayer, smooth;
    private float cont;
    private Vector3 target;


    public override void NewAwake()
    {
        base.NewAwake();
        getBase().Attack += Attack01;
    }

    void Attack01()
    {
        getBase().anim.Play("Attack01");
        getBase().PodeSeMover(0);
        getBase().PodeVirar(1);
    }
    void AtivarMovimentacao(int valor)
    {
        getBase().PodeSeMover(valor);
        getBase().PodeVirar(valor);
    }
    
    void AtePlayerAttack(int value)
    {
        if (value == 1)
        {
            getBase().PodeVirar(0);
            posInicial = corpo.position;
            target = getBase().player.transform.position;
            cont = Time.time + tempoAtePlayer;
        }
        else
        {
            target = posInicial;
        }
    }

    private void FixedUpdate()
    {
        if (Time.time < cont && target != null)
        {
            corpo.position = Vector3.Lerp(target, corpo.position, smooth);
        }
    }
}
