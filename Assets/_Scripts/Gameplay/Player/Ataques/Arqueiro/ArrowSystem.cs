using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSystem : MonoBehaviour
{
    [SerializeField] private PlayerAction actions;
    [SerializeField] private Animator anim;
    [SerializeField] private PlayerMove move;
    [SerializeField] private bool inAttack = false;
    [SerializeField] private string animAttack;
    [SerializeField] private string animReload;
    [SerializeField] private string animAttaking;
    [SerializeField] GameObject testeRapidinho;
    [SerializeField] private Transform localSpawnFlechas;
    [SerializeField] public GameObject prefFlechas;

    private void Start() 
    {
        actions = GetComponent<PlayerAction>();
        move = GetComponent<PlayerMove>();
    }

    private void Update() 
    {
        if (actions.AttackButtonDown())
        {
            inAttack = true;
            anim.Play(animAttaking);
            move.PararMovimento();
            testeRapidinho.SetActive(true);
        }

        if (inAttack)
        {
            Vector2 value = actions.MoveValue();
            Vector3 forward = new Vector3(Mathf.LerpAngle(transform.forward.x, value.x, 0.1f), 0f,
                                            Mathf.LerpAngle(transform.forward.z, value.y, 0.1f));
            transform.forward = forward;
        }

        if(actions.AttackButtonUp())
        {
            inAttack = false;
            move.podeAndar = true;
            anim.Play(animAttack);
            GameObject flecha = Instantiate(prefFlechas, localSpawnFlechas.position, Quaternion.identity);
            flecha.transform.forward = transform.forward;

            testeRapidinho.SetActive(false);

        }
    }
}
