using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuriaSystem : MonoBehaviour
{
    [Header("Puxando Valores")]
    [SerializeField] private PlayerAction actions;
    [SerializeField] private PlayerMove player;
    [SerializeField] AnimationClip teste;
    public float maxFuria, atualFuria;

    [Header("HUD")]
    [SerializeField] private Transform barManaFuria;
    [SerializeField] private Image barCor;

    [Header("Valores Ataque")]
    public bool podeAtacar = true;
    [SerializeField] private float cdPerderFuria;
    [SerializeField] int qualAtaque;
    private float porcent, cont;
    [SerializeField] GameObject[] ataqueMachado;
    [SerializeField] public float[] furiaAtaque;
    [SerializeField] float[] gastoFuriaAtaques;
    [SerializeField] public bool[] podeUsarSkill = new bool[3];
    [SerializeField] float[] tempoRecarga = new float[3];
    [SerializeField] float[] tempoExecucao = new float[3];
    [SerializeField] ParticleSystem particulasFuria;

    [Header("Valores Skills Especificas")]
    [SerializeField] float forcaImpulsoSkill2;
    public delegate void UsoDeHabilidade(int qualSkillRecarga, float tempoRecarga);
    public static event UsoDeHabilidade HabilidadeUsada;

    private void Awake()
    {
        GanhoFuria.OnGanhouFuria += Ataque;
        AttackGuerreiro.OnGuerreiroAttacked += OnMachado;
        AttackGuerreiro.OnGuerreiroMoved += GuerreiroPodeMover;
        AttackGuerreiro.OnGuerreiroCanAttack += GuerreiroPodeAtacar;
        AttackGuerreiro.Impulso += ImpulsoAtaque;
        AttackGuerreiro.MachadoAtivado += AtivandoColisorMachado;
        SelectSkillsGuerreiro.TempoPassado += VoltarHabilidade;
    }

    private void Start()
    { 

        porcent = 1 / maxFuria;
        cont = 0f;
        if (player.estaSendoControlado == true)
        {
            barCor.color = new Color(1f, 0.15f, 0f, 1f);
        }

        qualAtaque = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && podeAtacar == true && podeUsarSkill[0] && atualFuria >= gastoFuriaAtaques[0])
        {
            player.podeTrocarDeAnimacao = false;
            player.anim.Play("Skill01");
            player.speedAtual = player.speed * 0.8f;
            podeAtacar = false;
            podeUsarSkill[0] = false;
            ataqueMachado[1].SetActive(true);
            Skill(gastoFuriaAtaques[0]);
            StartCoroutine(TempoGiro());
        }

        if (Input.GetKeyDown(KeyCode.E) && podeAtacar == true && podeUsarSkill[1] && atualFuria >= gastoFuriaAtaques[1])
        {
            if (player.target != null)
            {
                player.VirandoParaInimigo();
            }
            player.podeAndar = false;
            player.anim.Play("Skill02");
            podeAtacar = false;
            podeUsarSkill[1] = false;
            Skill(gastoFuriaAtaques[1]);
            if (HabilidadeUsada != null)
            {
                HabilidadeUsada(1, tempoRecarga[1]);
            }

        }

        if (actions.input.Player.Tiro.WasPressedThisFrame() && podeAtacar && qualAtaque < 3)
        {
            player.anim.Play(player.normalAttacks[qualAtaque]);
            qualAtaque += 1;
            if (player.target != null)
            {
                player.VirandoParaInimigo();
            }

            player.rb.velocity = new Vector3(0f, player.rb.velocity.y, 0f);
            if (qualAtaque < 2)
            {
                player.rb.AddForce(transform.forward * player.speed * 0.6f, ForceMode.Impulse);
            }
            else
            {
                player.rb.AddForce(transform.forward * (player.speed * 1.2f) * 0.6f, ForceMode.Impulse);
            }
        }

        if (atualFuria >= maxFuria / 2)
        {
            if (particulasFuria.isPlaying == false)
            {
                particulasFuria.Play();
                player.speedAtual = player.speed + 2.5f;
            }
            
        }
        else
        {
            if (particulasFuria.isPlaying == true)
            {
                particulasFuria.Stop();
                player.speedAtual = player.speed + -2.5f;
            }
            
        }

        var rateEmission = particulasFuria.emission;
        rateEmission.rate = (((atualFuria - maxFuria / 2f) / (maxFuria / 2f)) * 10f) + 1f;
    }

    IEnumerator TempoGiro()
    {
        yield return new WaitForSeconds(tempoExecucao[0]);
        player.podeAndar = true;
        player.podeTrocarDeAnimacao = true;
        podeAtacar = true;
        player.anim.Play("Idle");
        player.speedAtual = player.speed;
        ataqueMachado[1].SetActive(false);
        if (HabilidadeUsada != null)
        {
            HabilidadeUsada(0, tempoRecarga[0]);
        }
    }

    void VoltarHabilidade(int qualSkill)
    {
        podeAtacar = true;
        podeUsarSkill[qualSkill] = true;
    }

    void AtivandoColisorMachado(int qualColisor)
    {
        ataqueMachado[qualColisor].SetActive(true);
    }

    void OnMachado(bool tendoAtaque)
    {
        player.rb.velocity = new Vector3(0f, player.rb.velocity.y, 0f);
        ataqueMachado[0].SetActive(tendoAtaque);
    }

    void GuerreiroPodeAtacar(bool consegueAtacar)
    {
        podeAtacar = consegueAtacar;
    }

    void GuerreiroPodeMover(bool podeMover)
    {
        player.podeAndar = podeMover;
        if (podeMover == true)
        {
            qualAtaque = 0;
        }

    }

    void ImpulsoAtaque()
    {
        if (player.target != null)
        {
            player.rb.AddForce(transform.forward * Vector3.Distance(transform.position, player.target.transform.position), ForceMode.Impulse);
        }
        else
        {
            player.rb.AddForce(transform.forward * (player.speed * forcaImpulsoSkill2), ForceMode.Impulse);
        }
        
    }

    private void FixedUpdate()
    {
        if (atualFuria > maxFuria)
        {
            atualFuria = maxFuria;
        }

        if (atualFuria < 0f)
        {
            atualFuria = 0f;
        }

        if (player.estaSendoControlado == true)
        {
            barManaFuria.localScale = new Vector2(porcent * atualFuria, barManaFuria.localScale.y);
        }

        cont += Time.unscaledDeltaTime;

        if (cont > cdPerderFuria && atualFuria > 0f)
        {
            atualFuria -= cont - cdPerderFuria;
        }

        
    }

    public void Ataque()
    {
        cont = 0f;
        atualFuria += furiaAtaque[qualAtaque -1];
    }
    
    public void Skill(float furiaGasta)
    {
        cont = 0f;
        atualFuria -= furiaGasta;
    }

    
}
