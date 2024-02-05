using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsBarProgress : MonoBehaviour
{
    [SerializeField] Transform progressBar;
    private float porcent;

    public void AtualizarValoresPorcentagem(float totalInimigos)
    {
        porcent = 1 / totalInimigos;
    }

    public void AtualizarProgressBar(int inimigosDerrotados)
    {
        progressBar.localScale = new Vector3(inimigosDerrotados * porcent, progressBar.localScale.y, progressBar.localScale.z);
    }
    
}
