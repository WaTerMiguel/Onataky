using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject MenuInicio, EscolhaDePersonagemSinglePlayer, EscolhaDePersonagemMultiPlayer;
    [SerializeField] PlayerManager pm;
    [SerializeField] Transform[] botoes;
    [SerializeField] SelectCharacterMultiPlayer[] botaoMulti;
    [SerializeField] Animator[] multiAnim;
    [SerializeField] GameObject jogador2;
    private bool novoJogo = false, multiplayer = false;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("InputJogador1"))
        {
            PlayerPrefs.SetString("InputJogador1", "WASD");
            PlayerPrefs.SetString("InputJogador2", "SETAS");
            PlayerPrefs.Save();
        }
    }

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (MenuInicio.activeSelf == false)
            {
                EscolhaDePersonagemMultiPlayer.SetActive(false);
                EscolhaDePersonagemSinglePlayer.SetActive(false);
                MenuInicio.SetActive(true);
            }
        }
    }

    public void NewGameSinglePlayer()
    {
        novoJogo = true;
        multiplayer = false;
        EscolhaDePersonagemSinglePlayer.SetActive(true);
        MenuInicio.SetActive(false);
    }

    public void NewGameMultiPlayer()
    {
        novoJogo = true;
        multiplayer = true;
        EscolhaDePersonagemMultiPlayer.SetActive(true);
        MenuInicio.SetActive(false);
    }

    public void NewGameMagaOnePlayer()
    {
        pm.RecebendoValores(1,"Maga");
        StartCoroutine(StartLevel1());
    }

    public void NewGameGuerreiroOnePlayer()
    {
        pm.RecebendoValores(1, "Guerreiro");
        StartCoroutine(StartLevel1());
    }

    public void NewGameMagaTwoPlayers()
    {
        pm.RecebendoValores(2, "Maga");
        StartCoroutine(StartLevel1());
        jogador2.SetActive(true);
        jogador2.transform.localPosition = botoes[0].localPosition;
        botaoMulti[0].enabled = false;
        botaoMulti[1].enabled = false;
        multiAnim[1].Play("OnMove");
    }

    public void NewGameGuerreiroTwoPlayers()
    {
        pm.RecebendoValores(2, "Guerreiro");
        StartCoroutine(StartLevel1());
        jogador2.SetActive(true);
        jogador2.transform.localPosition = botoes[1].localPosition;
        botaoMulti[0].enabled = false;
        botaoMulti[1].enabled = false;
        multiAnim[0].Play("OnMove");
    }

    IEnumerator StartLevel1()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Level1");
    }
}
