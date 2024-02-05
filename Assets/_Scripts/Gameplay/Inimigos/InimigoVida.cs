using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoVida : MonoBehaviour
{
    [SerializeField] float vidaInimigo;
    [SerializeField] Animator anim;
    public delegate void MorteDoInimigo();
    public event MorteDoInimigo InimigoFoiMorto;
    

    private void Update()
    {
        if (vidaInimigo <= 0)
        {
            vidaInimigo = 0;
            anim.Play("Death");

            if (InimigoFoiMorto != null)
            {
                InimigoFoiMorto();
            }

            enabled = false;
        }
    }

    public void Dano(float dano, GameObject text)
    {
        vidaInimigo -= dano;
        var t = Instantiate(text, transform.position, Quaternion.identity);
        if (t.transform.GetChild(0).gameObject.TryGetComponent(out TextMesh textMesh))
        {
            textMesh.text = dano.ToString();
        }
    }
}
