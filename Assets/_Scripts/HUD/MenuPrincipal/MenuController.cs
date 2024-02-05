using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject MenuInicio, EscolhaDePersonagem;
    private bool novoJogo = false;
    public void NewGame()
    {
        novoJogo = true;
        EscolhaDePersonagem.SetActive(true);
        MenuInicio.SetActive(false);
    }

    public void NewGameMaga()
    {
        PlayerPrefs.SetString("JogadorUm", "Maga");
        PlayerPrefs.SetInt("QuantosJogadores", 1);
        StartCoroutine(StartLevel1());
    }

    public void NewGameGuerreiro()
    {
        PlayerPrefs.SetString("JogadorUm", "Guerreiro");
        PlayerPrefs.SetInt("QuantosJogadores", 1);
        StartCoroutine(StartLevel1());
    }

    IEnumerator StartLevel1()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Level1");
    }
}
