using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [Header("Players")]
    [SerializeField] GameObject magaPlayer, guerreiroPlayer;
    [SerializeField] Transform localSpawnPlayer1, localSpawnPlayer2;
    [SerializeField] GameObject ObjectsPlayer2;
    [SerializeField] PlayerAction inputMaga, inputGuerreiro;
    [Header("Alvos")]
    [SerializeField] CameraSeguindo[] targetCamera;
    [SerializeField] SpawnerManager[] targetInimigos;
    [SerializeField] GameObject magaHUD, guerreiroHUD;
    [SerializeField] Camera[] camerasPlayer;
    [Header("Valores")]
    public static int quantosJogadores = 2;
    public static string jogadorUm = "Guerreiro";
    public bool escolhendoPersonagem = false;
    public delegate void TerminouPlayer();
    public static event TerminouPlayer ManagerPlayerFinished;
    public static event TerminouPlayer MultiplayerAtivado;
    public static event TerminouPlayer SingleplayerAtivado;

    private void Awake()
    {
        if (!escolhendoPersonagem)
        {
            ConfiguracaoPlayers(quantosJogadores, jogadorUm);

            ReposicaoHUD(quantosJogadores,jogadorUm);

            MudancaCamera(quantosJogadores, jogadorUm);

            if (ManagerPlayerFinished != null)
            {
                ManagerPlayerFinished();
            }
        }
    }

    public void RecebendoValores(int quantPlayers, string playerUm)
    {
        quantosJogadores = quantPlayers;
        jogadorUm = playerUm;
    }

    void ConfiguracaoPlayers(int quantiPlayers, string qualPlayerUm)
    {
        if (quantosJogadores == 1)
        {
            ObjectsPlayer2.SetActive(false);
            switch (jogadorUm)
            {
                case "Maga":
                    magaPlayer.SetActive(true);
                    magaHUD.SetActive(true);
                    targetInimigos[0].qualAlvo = 0;
                    targetInimigos[0].target = magaPlayer.transform;
                    magaPlayer.transform.position = localSpawnPlayer1.position;
                    inputMaga.tipoDeInput = PlayerPrefs.GetString("InputJogador1");
                    break;

                case "Guerreiro":
                    guerreiroPlayer.SetActive(true);
                    guerreiroHUD.SetActive(true);
                    targetInimigos[0].qualAlvo = 1;
                    targetInimigos[0].target = guerreiroPlayer.transform;
                    guerreiroPlayer.transform.position = localSpawnPlayer1.position;
                    inputGuerreiro.tipoDeInput = PlayerPrefs.GetString("InputJogador1");
                    break;

            }
        }

        if (quantosJogadores == 2)
        {
            ObjectsPlayer2.SetActive(true);
            magaPlayer.SetActive(true);
            magaHUD.SetActive(true);
            guerreiroPlayer.SetActive(true);
            guerreiroHUD.SetActive(true);

            switch (jogadorUm)
            {
                case "Maga":
                    
                    targetInimigos[0].qualAlvo = 0;
                    targetInimigos[0].target = magaPlayer.transform;
                    targetInimigos[1].qualAlvo = 1;
                    targetInimigos[1].target = guerreiroPlayer.transform;
                    magaPlayer.transform.position = localSpawnPlayer1.position;
                    guerreiroPlayer.transform.position = localSpawnPlayer2.position;
                    inputMaga.tipoDeInput = PlayerPrefs.GetString("InputJogador1");
                    inputGuerreiro.tipoDeInput = PlayerPrefs.GetString("InputJogador2");
                    break;

                case "Guerreiro":
                    
                    targetInimigos[0].qualAlvo = 1;
                    targetInimigos[0].target = guerreiroPlayer.transform;
                    targetInimigos[1].qualAlvo = 0;
                    targetInimigos[1].target = magaPlayer.transform;
                    guerreiroPlayer.transform.position = localSpawnPlayer1.position;
                    magaPlayer.transform.position = localSpawnPlayer2.position;
                    inputGuerreiro.tipoDeInput = PlayerPrefs.GetString("InputJogador1");
                    inputMaga.tipoDeInput = PlayerPrefs.GetString("InputJogador2");
                    break;

            }
        }
    }

    void ReposicaoHUD(int quantiPlayers, string qualPlayerUm)
    {
        switch (quantiPlayers)
        {
            case 1:

                switch (qualPlayerUm)
                {
                    case "Maga":
                        magaHUD.transform.localPosition = new Vector3(0, 0, 0);
                        break;

                    case "Guerreiro":
                        guerreiroHUD.transform.localPosition = new Vector3(0, 0, 0);
                        break;
                }

                break;

            case 2:

                switch (qualPlayerUm)
                {
                    case "Maga":
                        magaHUD.transform.localPosition = new Vector3(-685, 0, 0);
                        guerreiroHUD.transform.localPosition = new Vector3(685, 0, 0);
                        break;

                    case "Guerreiro":
                        magaHUD.transform.localPosition = new Vector3(685, 0, 0);
                        guerreiroHUD.transform.localPosition = new Vector3(-685, 0, 0);
                        break;
                }

                break;
        }
    }

    void MudancaCamera(int quantiPlayers, string qualPlayerUm)
    {
        switch (quantiPlayers)
        {
            case 1:

                camerasPlayer[0].gameObject.SetActive(true);
                camerasPlayer[1].gameObject.SetActive(false);
                camerasPlayer[0].rect = new Rect(0f,0f,1f,1f);
                camerasPlayer[0].orthographicSize = 13f;
                switch (qualPlayerUm)
                {
                    case "Maga":
                        targetCamera[0].target = magaPlayer.transform;
                        break;

                    case "Guerreiro":
                        targetCamera[0].target = guerreiroPlayer.transform;
                        break;
                }

                break;

            case 2:

                camerasPlayer[0].gameObject.SetActive(true);
                camerasPlayer[0].rect = new Rect(0f, 0f, 0.5f, 1f);
                camerasPlayer[0].orthographicSize = 18f;
                camerasPlayer[1].gameObject.SetActive(true);
                camerasPlayer[1].rect = new Rect(0.5f, 0f, 0.5f, 1f);
                camerasPlayer[1].orthographicSize = 18f;



                switch (qualPlayerUm)
                {
                    case "Maga":
                        targetCamera[0].target = magaPlayer.transform;
                        targetCamera[1].target = guerreiroPlayer.transform;
                        break;

                    case "Guerreiro":
                        targetCamera[0].target = guerreiroPlayer.transform;
                        targetCamera[1].target = magaPlayer.transform;
                        break;
                }

                break;
        }
    }
}
