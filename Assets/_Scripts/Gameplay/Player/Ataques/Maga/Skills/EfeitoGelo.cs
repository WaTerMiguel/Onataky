using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfeitoGelo : MonoBehaviour
{
    public float porcentDiminuicaoDeVelocidade, tSlow;
    [SerializeField] InimigoBase inim;

    private void Start()
    {
        if (TryGetComponent(out InimigoBase inimigo))
        {
            inim = inimigo;
            ComecandoEfeito();
        }

    }

    public void RecebendoValores(float por, float t)
    {
        StopAllCoroutines();
        tSlow = t;
        porcentDiminuicaoDeVelocidade = por;
        porcentDiminuicaoDeVelocidade = porcentDiminuicaoDeVelocidade / 100f;
    }

    public void ComecandoEfeito()
    {
        inim.speedAtual = inim.speed * porcentDiminuicaoDeVelocidade;
        StartCoroutine(TempoSlow(tSlow));
    }

    public IEnumerator TempoSlow(float t)
    {
        
        yield return new WaitForSeconds(tSlow);
        if (inim != null) 
        { 
            inim.speedAtual = inim.speed; 
        }
        Destroy(this);
    }
}
