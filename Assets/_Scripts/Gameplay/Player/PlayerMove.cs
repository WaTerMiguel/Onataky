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
    private Vector3 MoveValue;
    public GameObject target;
    public bool podeAndar = true;
    public Vector2 direciton;

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
        MoveValue.x = actions.input.Player.Move.ReadValue<Vector2>().x;
        MoveValue.z = actions.input.Player.Move.ReadValue<Vector2>().y;

    }

    void Movimento()
    {
        if (podeAndar)
        {

            rb.velocity = new Vector3(MoveValue.x * speedAtual, rb.velocity.y, MoveValue.z * speedAtual);

            if (podeTrocarDeAnimacao)
            {
                if (MoveValue != Vector3.zero)
                {
                    transform.forward = MoveValue;
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
