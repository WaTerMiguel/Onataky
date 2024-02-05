using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ManaSystem : MonoBehaviour
{
    [Header("Puxando Valores")]
    [SerializeField] private PlayerMove player;
    [SerializeField] private PlayerLife vida;
    [SerializeField] private PlayerAction actions;
    
    [Header("Materiais e Texturas")]
    [SerializeField] Material material;
    [SerializeField] Texture[] texturesCajado;
    [SerializeField] Color[] coresAtaques;
    [SerializeField] float brilhoBola;

    [Header("HUD e Mana")]
    [SerializeField] private Transform barManaFuria;
    [SerializeField] private Image barCor;
    private float porcent;
    private float cont;
    public float maxMana, atualMana, MPS;
    
    [Header("Ataques")]
    private bool podeAtacar = true;
    [SerializeField] Transform localTiro;
    [SerializeField] int qualAtaque;
    [SerializeField] GameObject[] ataquesGO, skillAtaquesGO;
    [SerializeField] bool usouSkill = false;
    [SerializeField] float[] quantiManaSkill = new float[3];
    [SerializeField] public bool[] podeUsarSkill = new bool[3], podeTrocarSkill = new bool[3];
    [SerializeField] float[] tempoRecarga = new float[3];
    public delegate void TrocaDeHabilidades(int skills);
    public static event TrocaDeHabilidades HabilidadeTrocada;
    public delegate void UsoDeHabilidade(int qualSkillRecarga, float tempoRecarga);
    public static event UsoDeHabilidade HabilidadeUsada;


    private void Awake()
    {
        AttackMaga.OnMagaAttacked += AttackMagaV;
        AttackMaga.OnMagaCanAttack += PodeAtacar;
        AttackMaga.OnMagaMoved += PodeMover;
        SelectSkillsMaga.TempoPassado += TempoSkill;
    }

    private void Start()
    {
        porcent = 1 / maxMana;
        cont = 0f;
        if (player.estaSendoControlado == true)
        {
            barCor.color = new Color(0f, 0.5f, 1f, 1f);
        }
        material.SetColor("_EmissionColor", coresAtaques[qualAtaque] * brilhoBola);
    }

    private void Update()
    { 

        if (Input.GetKeyDown(KeyCode.Q))
        {
            qualAtaque = 0;
            material.mainTexture = texturesCajado[0];
            material.color = coresAtaques[qualAtaque];
            if (HabilidadeTrocada != null)
            {
                HabilidadeTrocada(qualAtaque);
            }
            if (podeUsarSkill[qualAtaque])
            {
                usouSkill = true;
                material.SetColor("_EmissionColor", coresAtaques[qualAtaque] * brilhoBola * 1.5f);
            }
            else
            {
                material.SetColor("_EmissionColor", coresAtaques[qualAtaque] * brilhoBola);
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            qualAtaque = 1;
            material.mainTexture = texturesCajado[1];
            material.color = coresAtaques[qualAtaque];
            if (HabilidadeTrocada != null)
            {
                HabilidadeTrocada(qualAtaque);
            }
            if (podeUsarSkill[qualAtaque])
            {
                usouSkill = true;
                material.SetColor("_EmissionColor", coresAtaques[qualAtaque] * brilhoBola * 2f);
            }
            else
            {
                material.SetColor("_EmissionColor", coresAtaques[qualAtaque] * brilhoBola);
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            qualAtaque = 2;
            material.mainTexture = texturesCajado[2];
            material.color = coresAtaques[qualAtaque];
            if (HabilidadeTrocada != null)
            {
                HabilidadeTrocada(qualAtaque);
            }
            if (podeUsarSkill[qualAtaque])
            {
                usouSkill = true;
                material.SetColor("_EmissionColor", coresAtaques[qualAtaque] * brilhoBola * 1.5f);
            }
            else
            {
                material.SetColor("_EmissionColor", coresAtaques[qualAtaque] * brilhoBola);
            }

        }

        if (actions.input.Player.Tiro.IsPressed() && podeAtacar == true)
        {
            if (usouSkill == false)
            {
                if (atualMana > 10f)
                {
                    if (player.target != null)
                    {
                        player.VirandoParaInimigo();
                    }
                    material.SetColor("_EmissionColor", coresAtaques[qualAtaque] * brilhoBola);
                    player.rb.velocity = new Vector3(0, player.rb.velocity.y, 0);
                    player.anim.Play(player.normalAttacks[0]);
                }
            }
            else
            {
                if (atualMana > quantiManaSkill[qualAtaque])
                {
                    if (player.target != null)
                    {
                        player.VirandoParaInimigo();
                    }
                    material.SetColor("_EmissionColor", coresAtaques[qualAtaque] * brilhoBola);
                    player.rb.velocity = new Vector3(0, player.rb.velocity.y, 0);
                    player.anim.Play(player.normalAttacks[0]);
                }
            }
        }

    }

    void TempoSkill(int qualSkill)
    {
        podeUsarSkill[qualSkill] = true;
    }

    void AttackMagaV()
    {
        if(usouSkill == false)
        {
            GameObject ataque = Instantiate(ataquesGO[qualAtaque], localTiro.position, Quaternion.identity);
            if (ataque.TryGetComponent(out TiroGeral stiro))
            {
                stiro.d = transform.forward;
                stiro.player = transform;
                GastoDeMana(10f);

            }
            if (ataque.TryGetComponent(out Cura cura))
            {
                cura.targetCura = vida;
            }
        }
        else
        {
            GameObject ataque = Instantiate(skillAtaquesGO[qualAtaque], localTiro.position, Quaternion.identity);
            if (ataque.TryGetComponent(out TiroGeral stiro))
            {
                stiro.d = transform.forward;
                stiro.player = transform;

            }
            if (ataque.TryGetComponent(out Cura cura))
            {
                cura.targetCura = vida;
            }
            if (HabilidadeUsada != null)
            {
                HabilidadeUsada(qualAtaque, tempoRecarga[qualAtaque]);
                podeUsarSkill[qualAtaque] = false;
            }
            usouSkill = false;
            GastoDeMana(50f);
        }
    }

    void PodeAtacar(bool b)
    {
        podeAtacar = b;
    }

    void PodeMover(bool b)
    {
        player.podeAndar = b;
    }

    private void FixedUpdate()
    {
        if (atualMana > maxMana)
        {
            atualMana = maxMana;
        }

        if (atualMana < 0f)
        {
            atualMana = 0f;
        }

        if (player.estaSendoControlado == true)
        {
            barManaFuria.localScale = new Vector2(porcent * atualMana, barManaFuria.localScale.y);
        }

        cont += Time.unscaledDeltaTime;

        if (cont > 1f && atualMana < maxMana)
        {
            cont = 0f;
            StartCoroutine(MudandoMana(MPS));
        }

    }

    public void GastoDeMana(float manaGasta)
    {
        atualMana -= manaGasta;
    }

    IEnumerator MudandoMana(float mana)
    {
        for(float i = 0; i <= mana; i += Time.unscaledDeltaTime * mana)
        {
            atualMana += Time.deltaTime * mana;
            yield return new WaitForSecondsRealtime(Time.unscaledDeltaTime);
        }
    }

}
