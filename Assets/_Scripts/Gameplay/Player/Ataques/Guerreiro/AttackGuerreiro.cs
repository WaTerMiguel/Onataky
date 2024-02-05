using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackGuerreiro : MonoBehaviour
{

    public delegate void QualColisorAtivado(int indexColisorAtivado);
    public static event QualColisorAtivado ColisorAtivado;
    public static event QualColisorAtivado MachadoAtivado;
    public delegate void AcaoAtaqueGuerreiro(bool tendoAtaque);
    public static event AcaoAtaqueGuerreiro OnGuerreiroAttacked;
    public static event AcaoAtaqueGuerreiro OnGuerreiroMoved;
    public static event AcaoAtaqueGuerreiro OnGuerreiroCanAttack;
    public delegate void Skill02();
    public static event Skill02 Impulso;

    [SerializeField] Collider[] co;
    [SerializeField] GameObject[] efeitos;
    [SerializeField] TrailRenderer trailEffect;

    void ComecoAnimacao()
    {
        if (OnGuerreiroAttacked != null && OnGuerreiroCanAttack != null && OnGuerreiroMoved != null)
        {
            OnGuerreiroAttacked(false);
            OnGuerreiroCanAttack(false);
            OnGuerreiroMoved(false);

        }
    }
    
    void MachadoOn(int qualColisor)
    {
        if (MachadoAtivado != null)
        {
            MachadoAtivado(qualColisor);
        }
    }

    void OnAttackGuerreiro(int ataque)
    {
        if (OnGuerreiroAttacked != null)
        {
            if (ataque == 1)
            {
                OnGuerreiroAttacked(true);
            }
            else
            {
                OnGuerreiroAttacked(false);
            }
        }
    }

    void OnCanAttackGuerreiro(int podeAtacar)
    {
        if (OnGuerreiroCanAttack != null)
        {
            if (podeAtacar == 1)
            {
                OnGuerreiroCanAttack(true);
            }
            else
            {
                OnGuerreiroCanAttack(false);
            }
        }
    }

    void OnMoveGuerreiro(int podeMover)
    {
        if (OnGuerreiroMoved != null)
        {
            if (podeMover == 1)
            {
                OnGuerreiroMoved(true);
            }
            else
            {
                OnGuerreiroMoved(false);
            }
        }
    }

    void Skill2Impulso()
    {
        if (Impulso != null)
        {
            Impulso();
        }
    }

    void Skill2Dano(int indexColisor)
    {
        if (ColisorAtivado != null)
        {
            ColisorAtivado(indexColisor);
        }
    }


    void Attack3(int i)
    {
        if (i == 1)
        {
            co[0].enabled = true;
            Instantiate(efeitos[0], co[0].transform.position, Quaternion.identity);
        }
        else
        {
            co[0].enabled = false;
        }
    }

    void AtivarTrail()
    {
        trailEffect.Clear();
        efeitos[1].SetActive(true);
    }

    void DesativarTrail()
    {
        trailEffect.Clear();
        efeitos[1].SetActive(false);
    }
}
