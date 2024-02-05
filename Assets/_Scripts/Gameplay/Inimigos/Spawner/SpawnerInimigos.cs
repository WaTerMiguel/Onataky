using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerInimigos : MonoBehaviour
{
    public int alvo;
    public int inimigosTotais; 
    [SerializeField] int inimigosJaSpawnados;
    public GameObject inimigo;
    [SerializeField] float cdInimigos;
    private WaitForSeconds cooldown;
    [SerializeField] GameObject refLocais;
    private Transform[] locaisParaSpawnar;

    public Transform target;

    private void Awake()
    {
        locaisParaSpawnar = refLocais.GetComponentsInChildren<Transform>();
        cooldown = new WaitForSeconds(cdInimigos);
    }
    private void Start()
    {
        StartCoroutine(Spawner());
    }

    public IEnumerator Spawner()
    {
        while (inimigosJaSpawnados < inimigosTotais)
        {
            GameObject enemySpawned = Instantiate(inimigo, locaisParaSpawnar[Random.Range(0,locaisParaSpawnar.Length - 1)].transform.position, Quaternion.identity);
            enemySpawned.transform.SetParent(transform);
            var enemyScript = enemySpawned.GetComponent<InimigoBase>();
            enemyScript.alvoDoInimigo = alvo;
            enemyScript.player = target;
            inimigosJaSpawnados++;
            yield return cooldown;
        }
        enabled = false;
    }
}
