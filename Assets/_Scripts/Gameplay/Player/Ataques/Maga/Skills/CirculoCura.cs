using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirculoCura : MonoBehaviour
{

    [SerializeField] Cura cura;
    [SerializeField] PlayerAttack attack;
    [SerializeField] public float quantosTiks, tempoParaCadaTik, tamanhoCirculo, danoAttack;
    [SerializeField] ParticleSystem sus;


    private void Start()
    {

        
        attack.dano = danoAttack;
        transform.position = cura.targetCura.transform.position + new Vector3(0, 0.1f, 0);
        transform.SetParent(cura.targetCura.transform);
        StartCoroutine(DestroyCura());
    }

    private void FixedUpdate()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, 
                           new Vector3(tamanhoCirculo, transform.localScale.y, tamanhoCirculo), 0.01f);

        transform.Rotate(0,45f * Time.deltaTime,0);
    }

    IEnumerator DestroyCura()
    {
        yield return new WaitForSeconds(tempoParaCadaTik);
        for (int i = 0; i < quantosTiks; i++)
        {
            cura.enabled = true;
            yield return new WaitForSeconds(tempoParaCadaTik);
        }
        Destroy(gameObject);
    }


}
