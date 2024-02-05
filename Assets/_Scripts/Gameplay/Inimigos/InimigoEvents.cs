using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoEvents : MonoBehaviour
{
    [SerializeField] InimigoBase iniBase;
    [SerializeField] InimigoVida iniVida;
    [SerializeField] PuxandoMaterialCorpo materiais;

    public void Awake()
    {
        iniVida.InimigoFoiMorto += iniBase.Morte;
        iniVida.InimigoFoiMorto += MorteInimigo;
        NewAwake();
    }

    public virtual void NewAwake()
    {

    }


    public InimigoBase getBase()
    {
        return iniBase;
    }

    public InimigoVida getVida()
    {
        return iniVida;
    }

    public PuxandoMaterialCorpo getMaterials()
    {
        return materiais;
    }

    public void MorteInimigo()
    {
        StartCoroutine(EfeitoFade());
    }
    IEnumerator EfeitoFade()
    {
        yield return new WaitForSeconds(0.7f);
        gameObject.AddComponent<EfeitoDesaparecer>();
        TryGetComponent(out EfeitoDesaparecer fade);
        fade.getMaterials = materiais;
        StartCoroutine(TempoAteSumir());

    }

    IEnumerator TempoAteSumir()
    {
        yield return new WaitForSeconds(2f);
        Destroy(transform.parent.gameObject);
    }
}
