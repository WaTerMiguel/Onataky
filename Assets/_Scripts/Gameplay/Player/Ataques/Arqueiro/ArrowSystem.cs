using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSystem : MonoBehaviour
{
    [SerializeField] PlayerAction actions;
    [SerializeField] Animator anim;
    [SerializeField] PlayerMove move;
    [SerializeField] private bool inAttack = false;
    [SerializeField] private string animAttack;
    [SerializeField] private string animReload;
    [SerializeField] private string animAttaking;
    [SerializeField] GameObject testeRapidinho;
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
            anim.Play("Idle");
            testeRapidinho.SetActive(false);

        }
    }
}
