using System.Collections.Generic;
using UnityEngine;

public class RangeEncontrarInimigos : MonoBehaviour
{
    [SerializeField] PlayerMove move;
    public GameObject target, player;
    public List<GameObject> inimigos;

    private void Update()
    {
        if (inimigos.Count == 0)
        {
            target = null;
        }

        if (target != null)
        {
            if (target.transform.parent.TryGetComponent(out InimigoBase inim) == false)
            {
                inimigos.Remove(target);
                target = null;
            }
        }
        

        move.target = target;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Inimigo" && 
            other.transform.parent.TryGetComponent(out InimigoBase inimigo) && 
            inimigo.enabled == true && 
            inimigos.Contains(other.gameObject) == false)
        {
            inimigos.Add(other.gameObject);
            
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (inimigos.Contains(other.gameObject) && other.transform.parent.TryGetComponent(out InimigoBase inimigo))
        {
            if (target == null || Vector3.Distance(other.transform.position, player.transform.position) <
                                  Vector3.Distance(target.transform.position, player.transform.position))
            {
                target = other.gameObject;
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (inimigos.Contains(other.gameObject))
        {
            inimigos.Remove(other.gameObject);

        }
    }

}
