using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectarInimigos : MonoBehaviour
{
    public float radius;
    public GameObject target;
    [SerializeField] float espacoLivre;
    [SerializeField] LayerMask enemiesLayer;
    MoveIA move;
    [Range(0f, 180f)] public float teste;

    private void Awake()
    {
        move = GetComponent<MoveIA>();
    }

    private void FixedUpdate()
    {
        EncontrarTodosInimigos();
        if (target != null)
        {
            move.target = target.transform;
        }
        else
        {
            OrientacaoBasica();
        }
        
    }

    void EncontrarTodosInimigos()
    {
        Collider[] enemiesFound = Physics.OverlapSphere(transform.position, radius, enemiesLayer);
        if (enemiesFound.Length != 0)
        {
            foreach (Collider enemy in enemiesFound)
            {
                if (target == null)
                {
                    target = enemy.gameObject;
                }
                else
                {
                    if (Vector3.Distance(transform.position, enemy.transform.position) < Vector3.Distance(transform.position, target.transform.position))
                    {
                        target = enemy.gameObject;
                    }
                }

                EncontrarFuga(enemy);


            }
            
        }

    }

    void EncontrarFuga(Collider enemy)
    {
        float anguloAteInimigo = (Vector3.Angle(enemy.transform.position, transform.position));
        float playerY = Mathf.Abs(transform.rotation.y);
        if ((anguloAteInimigo - playerY) < teste)
        {
            transform.Rotate(0f,teste,0f);
        }
        else
        {

        }
        Debug.Log(anguloAteInimigo);

        /*
        float angleToEnemy = Vector3.Angle(transform.forward, enemy.transform.position);
        float angleToTarget = Vector3.Angle(transform.forward, target.transform.position);
        float angleDirectionSecurity = (angleToEnemy + angleToTarget) / 2;
        Quaternion thisRotation = transform.rotation;
        Debug.Log(angleToTarget);
        Debug.Log(angleToEnemy);
        Debug.Log(angleDirectionSecurity);
//        transform.Rotate(0f,angleDirectionSecurity,0f);
        transform.forward = new ;
        

        Vector3 directionToTarget = (target.transform.position - transform.forward);
        Vector3 directionToEnemy = (enemy.transform.position - transform.forward);
        float angle = Vector3.Angle(directionToEnemy, directionToTarget);
        Vector3 teste = ((target.transform.position + enemy.transform.position) - transform.position).normalized;

        Vector3 directionMedia = Vector3.Slerp(directionToEnemy, directionToTarget, 0.5f);

        transform.forward = teste;
        */
    }

    void OrientacaoBasica()
    {
        /*
        if (Physics.Raycast(transform.position + new Vector3(0,1,0), transform.forward, out RaycastHit hitFrente, distaciaVisao))
        {
            Debug.Log(hitFrente.collider.gameObject.name);
        }
        
        if (Physics.Raycast(transform.position + new Vector3(0, 1, 0), transform.forward * -1, out RaycastHit hitTras, distaciaVisao))
        {
            Debug.Log(hitTras.collider.gameObject.name);
        }

        if (Physics.Raycast(transform.position, transform.right, out RaycastHit hitDireita, distaciaVisao))
        {
            Debug.Log(hitDireita.collider.gameObject.name);
        }
        if (Physics.Raycast(transform.position + new Vector3(0, 1, 0), transform.right * -1, out RaycastHit hitEsquerda, distaciaVisao))
        {
            Debug.Log(hitEsquerda.collider.gameObject.name);
        }

        if (Physics.Raycast(transform.position + new Vector3(0, 1, 0), transform.forward + transform.right, out RaycastHit hitFrenteDireita, distaciaVisao))
        {
            Debug.Log(hitFrenteDireita.collider.gameObject.name);
        }
        if (Physics.Raycast(transform.position + new Vector3(0, 1, 0), transform.forward + transform.right * -1, out RaycastHit hitFrenteEsquerda, distaciaVisao))
        {
            Debug.Log(hitFrenteEsquerda.collider.gameObject.name);
        }

        if (Physics.Raycast(transform.position + new Vector3(0, 1, 0), transform.forward * -1 + transform.right, out RaycastHit hitTrasDireita, distaciaVisao))
        {
            Debug.Log(hitTrasDireita.collider.gameObject.name);
        }
        if (Physics.Raycast(transform.position + new Vector3(0, 1, 0), transform.forward * -1 + transform.right * -1, out RaycastHit hitTrasEsquerda, distaciaVisao))
        {
            Debug.Log(hitTrasEsquerda.collider.gameObject.name);
        }

        Debug.DrawRay(transform.position, transform.forward * distaciaVisao, Color.red);
        Debug.DrawRay(transform.position, transform.forward * -1 * distaciaVisao, Color.red);
        Debug.DrawRay(transform.position, transform.right * distaciaVisao, Color.yellow);
        Debug.DrawRay(transform.position, transform.right * -1f * distaciaVisao, Color.yellow);
        Debug.DrawRay(transform.position, (transform.forward + transform.right) * distaciaVisao, Color.green);
        Debug.DrawRay(transform.position, (transform.forward + transform.right * -1) * distaciaVisao, Color.green);
        Debug.DrawRay(transform.position, (transform.forward * -1 + transform.right) * distaciaVisao, Color.blue);
        Debug.DrawRay(transform.position, (transform.forward * -1 + transform.right * -1) * distaciaVisao, Color.blue);
        */



    }
}