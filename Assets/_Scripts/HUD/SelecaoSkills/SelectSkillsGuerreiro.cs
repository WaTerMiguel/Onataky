using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectSkillsGuerreiro : MonoBehaviour
{
    [SerializeField] Sprite[] imagensSkills;
    [SerializeField] Image[] skills;

    public delegate void TempoDeEspera(int qualSkill);
    public static event TempoDeEspera TempoPassado;

    private void Awake()
    {
        FuriaSystem.HabilidadeUsada += UsoHabilidade;
    }

    private void SkillChange(int ataque)
    {
        for (int i = 0; i < imagensSkills.Length; i++)
        {
            if (i == ataque)
            {
                skills[ataque].color = Color.white;
                //goSelect.transform.position = skills[ataque].transform.position;
            }
        }
    }

    void UsoHabilidade(int qualSkillFoiUsada, float tempoDeRecarga)
    {
        SkillChange(qualSkillFoiUsada);
        StartCoroutine(RecargaSkill(qualSkillFoiUsada, tempoDeRecarga));
    }

    IEnumerator RecargaSkill(int qualSkill, float tempoRecar)
    {
        for (float i = 0; i <= tempoRecar; i = i + Time.deltaTime)
        {
            skills[qualSkill].fillAmount = i / tempoRecar;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        if (TempoPassado != null)
        {
            TempoPassado(qualSkill);
        }

    }
}
