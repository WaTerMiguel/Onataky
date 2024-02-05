using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkGelo : MonoBehaviour
{
    [SerializeField] GameObject tiroParent;
    [SerializeField] TiroGeral tiro;
    [SerializeField] List<GameObject> inimigosDentro;
    [SerializeField] float tempoSlow, porcentSlow;

    private void Awake()
    {
        tiro.TiroFezContato += EmContatoComInimigo;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.tag == "Inimigo")
            {
                if (other.transform.parent != null)
                {
                    if (other.transform.parent.TryGetComponent(out InimigoBase inimigo))
                    {
                        GameObject GOInimigo = other.transform.parent.gameObject;
                        inimigosDentro.Add(GOInimigo);
                    }
                }
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other != null)
        {
            if (other.tag == "Inimigo")
            {
                if (inimigosDentro.Contains(other.transform.parent.gameObject))
                {
                    inimigosDentro.Remove(other.transform.parent.gameObject);
                }
            }
        }
    }

    void EmContatoComInimigo()
    {
        foreach(GameObject InimEmContato in inimigosDentro)
        {
            if (InimEmContato.TryGetComponent(out EfeitoGelo g))
            {
                g.RecebendoValores(porcentSlow, tempoSlow);
                g.ComecandoEfeito();

            }
            else
            {
                InimEmContato.AddComponent<EfeitoGelo>();
                InimEmContato.TryGetComponent(out EfeitoGelo efeito);
                efeito.RecebendoValores(porcentSlow, tempoSlow);
            }
        }
        Destroy(tiroParent);
    }
}
