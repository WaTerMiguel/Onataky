using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [Header("Recebendo Valores")]
    public int qualAlvo;
    private SpawnerInimigos[] _todosOsSpawners;
    [SerializeField]private int _quantosInimigosParaCompletar = 0, _inimigosJaDerrotados = 0;
    [SerializeField] LevelsBarProgress progressUI;
    public Transform target;

    private void Awake()
    {
        InimigoBase.Derrota += CheckEnemyDefeat;
        PlayerManager.ManagerPlayerFinished += AssociarAlvos;
    }

    void AssociarAlvos()
    {
        _todosOsSpawners = gameObject.GetComponentsInChildren<SpawnerInimigos>();
        foreach (SpawnerInimigos spawner in _todosOsSpawners)
        {
            _quantosInimigosParaCompletar += spawner.inimigosTotais;
            spawner.alvo = qualAlvo;
            spawner.target = target;
        }
        progressUI.AtualizarValoresPorcentagem(_quantosInimigosParaCompletar);
        progressUI.AtualizarProgressBar(_inimigosJaDerrotados);
    }

    void CheckEnemyDefeat(int alvoDoInimigo)
    {
        if (alvoDoInimigo == qualAlvo)
        {
            _inimigosJaDerrotados++;
            progressUI.AtualizarProgressBar(_inimigosJaDerrotados);
        }
    }
}
