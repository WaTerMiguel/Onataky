using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class PlayerMove : MonoBehaviour
{
    [Header("Puxar Componentes")]
    [SerializeField] PlayerAction actions;
    public Rigidbody rb;
    public Animator anim;

    [Header("Valores de Movimento")]
    public float speed, speedAtual;
    public GameObject target;
    public bool podeAndar = true;
    public Vector2 direciton;
    Vector2 value;

    [Header("Dash")]
    public bool dashPeloMouse = false;

    [Header("HUD")]
    [SerializeField] Image dashImage;
    public bool estaSendoControlado;

    [Header("Animacoes")]
    [SerializeField] string idle, walk;
    public bool podeTrocarDeAnimacao = true;
    public string[] normalAttacks = new string[3];

    private void Start()
    {
        podeAndar = true;
        speedAtual = speed;
    }

    private void FixedUpdate()
    {
        Movimento();
    }

    private void Update()
    {
        value = actions.MoveValue();
    }

    void Movimento()
    {
        if (podeAndar)
        {
            rb.velocity = new Vector3(value.x * speedAtual, rb.velocity.y, value.y * speedAtual);

            if (podeTrocarDeAnimacao)
            {
                if (value != Vector2.zero)
                {
                    Vector3 forward = new Vector3(value.x, 0f, value.y);

                    transform.forward = forward;
                    anim.SetBool("estaAndando", true);
                }
                else
                {
                    anim.SetBool("estaAndando", false);
                }
            }

        }
    }

    public void VirandoParaInimigo()
    {
        transform.forward = (target.transform.position - transform.position).normalized;
    }

    public IEnumerator KB(Vector3 dir, float forcaKB, float tempoKB)
    {
        podeAndar = false;
        rb.velocity = Vector3.zero;
        rb.AddForce(dir * forcaKB, ForceMode.Impulse);
        yield return new WaitForSeconds(tempoKB);
        podeAndar = true;

    }
}
