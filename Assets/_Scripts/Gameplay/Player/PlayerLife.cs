using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private Transform barVida;
    [SerializeField] private Image imageChar;
    public float maxVida, atualVida;
    public bool estaMorto = false;
    private float porcent;
    [SerializeField] bool modoFantasma = false;
    [SerializeField] Sprite[] perfilPersonagem = new Sprite[3];
    public bool podeTomarDano = true;

    private void Start()
    {
        porcent = 1 / maxVida;
        imageChar.sprite = perfilPersonagem[0];
    }

    private void FixedUpdate()
    {
        if (atualVida > maxVida)
        {
            atualVida = maxVida;
        }

        if (atualVida <= 0f)
        {
            atualVida = 0;
            estaMorto = true;
        }

        if (estaMorto)
        {
            imageChar.sprite = perfilPersonagem[2];
        }
        else
        {
            if (modoFantasma)
            {
                imageChar.sprite = perfilPersonagem[1];
            }
            else
            {
                imageChar.sprite = perfilPersonagem[0];
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            switch (modoFantasma)
            {
                case true:
                    modoFantasma = false;
                    break;

                case false:
                    modoFantasma = true;
                    break;
            }
        }

        barVida.transform.localScale = new Vector2(porcent * atualVida, barVida.transform.localScale.y);
    }

    public void Dano(float dano, float tempo)
    {
        if (podeTomarDano)
        {
            atualVida -= dano;
            StartCoroutine(tempoInvuneravel(tempo));
        }
        
    }

    public void Cura(float vidaCurada)
    {
        atualVida += vidaCurada;
    }

    IEnumerator tempoInvuneravel(float tempoKB)
    {
        yield return new WaitForSeconds(tempoKB);
        podeTomarDano = true;
    }
}
