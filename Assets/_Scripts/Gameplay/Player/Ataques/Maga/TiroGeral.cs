using System.Collections;
using UnityEngine;

public class TiroGeral : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    public Vector3 d;
    [SerializeField] float speed, mudanca, rangeAlcance;
    private readonly bool alvoEncontrado;
    [SerializeField] GameObject target;
    public Transform player;
    private float tempoVivo = 0;
    [SerializeField] bool temEfeito;
    [SerializeField] float tempoEfeito;
    public bool efeito;
    public LayerMask enemyLayer;

    public delegate void EfeitoDoTiro();
    public event EfeitoDoTiro TiroFezContato;

    private void Start()
    {
        if (temEfeito)
        {
            StartCoroutine(ContadorEfeito());
        }

    }

    IEnumerator ContadorEfeito()
    {
        yield return new WaitForSeconds(tempoEfeito);
        efeito = true;
    }

    private void Update()
    {
        if (Vector3.Distance(player.position, transform.position) > 35f)
        {
            Destroy(this.gameObject);
        }

    }

    private void FixedUpdate() 
    {
        Collider[] inimigosPerto = Physics.OverlapSphere(transform.position, rangeAlcance, enemyLayer);
        if (inimigosPerto.Length > 0)
        {
            if (inimigosPerto[0] != null)
            {
                d = (inimigosPerto[0].transform.position - transform.position).normalized;
                rb.velocity = d * speed;
            }
        }
        else
        {
            rb.velocity = new Vector3(d.x * speed, rb.velocity.y, d.z * speed);
        }
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Inimigo")
        {
            if (other.transform.parent != null)
            {
                if (other.transform.parent.TryGetComponent(out InimigoVida inimigo))
                {
                    if (temEfeito == true)
                    {
                        if (TiroFezContato != null)
                        {
                            TiroFezContato();
                        }

                        efeito = true;
                    }
                }
            }
                
        }
    }
}
