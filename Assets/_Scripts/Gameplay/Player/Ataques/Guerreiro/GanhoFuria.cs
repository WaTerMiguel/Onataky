using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GanhoFuria : MonoBehaviour
{
    public delegate void TomandoDano();
    public static event TomandoDano OnGanhouFuria;
    [SerializeField] float forcaKB, tempoKB;

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.transform.parent != null)
            {
                if (other.transform.parent.TryGetComponent(out InimigoBase inimigo))
                {
                    if (OnGanhouFuria != null)
                    {
                        OnGanhouFuria();
                    }

                    Vector3 direction = (other.transform.position - transform.position).normalized;
                    direction.y = 0;

                    inimigo.StartCoroutine(inimigo.KnockBack(direction, forcaKB, tempoKB));
                }
            } 
        }
    }

}
