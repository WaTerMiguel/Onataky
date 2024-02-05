using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] float danoBase;
    [SerializeField] public float dano;
    [SerializeField] bool ataquePorSegundo = false;
    [SerializeField] float tickForSecond = 1.5f;
    private float cont;

    public bool vaiSerDestruido = false;
    public float tempoParaSerDestruido;
    bool entrouEmContato = false;

    [SerializeField] GameObject textValueGO;
    [SerializeField] GameObject efeitoContato;
    

    private void OnTriggerEnter(Collider other)
    {
        if (ataquePorSegundo == false)
        {
            if (other != null)
            {
                if (other.tag == "Inimigo")
                {
                    if (other.transform.parent != null)
                    {
                        if (other.transform.parent.TryGetComponent(out InimigoVida inimigo))
                        {
                            if (vaiSerDestruido)
                            {
                                if (entrouEmContato == false)
                                {
                                    StartCoroutine(AutoDestroy());
                                    entrouEmContato = true;
                                    if (efeitoContato != null)
                                    {
                                        Instantiate(efeitoContato, other.transform.parent.position, Quaternion.identity);
                                    }
                                    
                                }
                            }
                            inimigo.Dano(dano, textValueGO);

                        }
                    }
                }

            }
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (ataquePorSegundo == true)
        {
            if (other != null)
            {
                if (other.tag == "Inimigo")
                {
                    if (other.transform.parent != null)
                    {
                        if (other.transform.parent.TryGetComponent(out InimigoVida inimigo))
                        {
                            if (Time.time > cont)
                            {
                                inimigo.Dano(dano, textValueGO);
                                cont = Time.time + tickForSecond;
                            }
                            
                        }
                    }
                }

            }
        }
    }


    IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(tempoParaSerDestruido);
        Destroy(this.gameObject);
    }
}
