using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoBase : MonoBehaviour
{
    public Transform player;
    public float speed, rangeAttack, speedAtual;
    public Rigidbody rb;
    [SerializeField] Collider[] co;
    bool podeSeVirar = true, podeSeMover = true;
    //[SerializeField] GameObject c;
    private Vector3 direction;

    public delegate void InimigoAtacando();
    public event InimigoAtacando Attack;
    public delegate void DerrotaDoInimigo(int qualPlayer);
    public static event DerrotaDoInimigo Derrota;
    public bool contaNoProgresso = true;
    public int alvoDoInimigo = 1;

    public Animator anim;

    private void Start()
    {
        speed = Random.Range((int)(speed - 2), (int)(speed + 2));
        speedAtual = speed;
    }

    private void FixedUpdate()
    {
        direction = (player.position - transform.position).normalized;
        direction.y = 0;

        if (podeSeVirar == true)
        {
            transform.forward = direction;
        }
        

        if (podeSeMover)
        {
            rb.velocity = direction * speedAtual;
        }
        


        if (Vector3.Distance(player.position, transform.position) <= rangeAttack)
        {
            anim.SetBool("estaAndando", false);
            if (Attack != null)
            {
                Attack();
            }
            
        }
        else if (podeSeMover)
        {
            anim.SetBool("estaAndando", true);
        }
    }

    public void Morte()
    {
        Destroy(this.rb);
        foreach(Collider c in co)
        {
            Destroy(c);
        }
        if (Derrota != null && contaNoProgresso)
        {
            Derrota(alvoDoInimigo);
        }
        Destroy(this);
    }

    public void PodeSeMover(int i)
    {
        if (i == 1)
        {
            podeSeMover = true;
        }
        else
        {
            rb.velocity = Vector3.zero;
            podeSeMover = false;
        }
        
    }

    public void PodeVirar(int i)
    {
        if (i == 1)
        {
            podeSeVirar = true;
        }
        else
        {
            podeSeVirar = false;
        }
    }

    public IEnumerator KnockBack(Vector3 direction, float forca, float tempoKB)
    {
        rb.velocity = Vector3.zero;
        podeSeMover = false;
        podeSeVirar = false;
        rb.AddForce(direction * forca, ForceMode.Impulse);
        co[0].enabled = false;
        yield return new WaitForSeconds(tempoKB);
        co[0].enabled = true;
        podeSeMover = true;
        podeSeVirar = true;
        
    }
}
