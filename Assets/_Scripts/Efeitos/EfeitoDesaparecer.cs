using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfeitoDesaparecer : MonoBehaviour
{
    public PuxandoMaterialCorpo getMaterials;
    public delegate void Desaparaceu();
    public event Desaparaceu TerminouFade;
    public float tempoStartFade = 0;
    private float cont;
    public float tempoFade = 0.1f;

    private void Start()
    {
        foreach (Renderer renderer in getMaterials.partesCorpo)
        {
            var mat = renderer.material;
            mat.SetInt("_Mode", 2);
            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            mat.SetInt("_ZWrite", 0);
            mat.DisableKeyword("_ALPHATEST_ON");
            mat.EnableKeyword("_ALPHABLEND_ON");
            mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            mat.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
        }

        cont = Time.time + tempoStartFade;
        
    }

    private void FixedUpdate()
    {
        if (Time.time > cont)
        {
            foreach (Renderer renderer in getMaterials.partesCorpo)
            {
                var mat = renderer.material;
                Color colorMat = new Color(mat.color.r, mat.color.g, mat.color.b, 0f);
                mat.color = Color.Lerp(mat.color, colorMat, tempoFade);
            }
            if (getMaterials.partesCorpo[getMaterials.partesCorpo.Length - 1].material.color.a < 0.001f)
            {
                if (TerminouFade != null)
                {
                    TerminouFade();
                }
                Destroy(this);
            }
        }
    }
}
