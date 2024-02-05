using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruirEfeito : MonoBehaviour
{
    [SerializeField] EfeitoDesaparecer fade;

    private void Awake()
    {
        fade.TerminouFade += Destruir;
    }

    void Destruir()
    {
        Destroy(this.gameObject);
    }
}
