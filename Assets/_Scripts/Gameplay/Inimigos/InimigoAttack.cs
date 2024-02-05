using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoAttack : MonoBehaviour
{
    [SerializeField] float dano, tempoKB, forcaKB;

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.tag == "Player")
            {
                if (other.transform.parent != null)
                {
                    if (other.transform.parent.TryGetComponent(out PlayerLife vida) &&
                        other.transform.parent.TryGetComponent(out PlayerMove move))
                    {
                        Vector3 dir = (other.transform.position - transform.position).normalized;

                        vida.Dano(dano, tempoKB);
                        move.StartCoroutine(move.KB(dir, tempoKB, forcaKB));
                    }
                }
            }
        }
        
    }

}
