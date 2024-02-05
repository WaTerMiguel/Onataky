using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Players")]
    [SerializeField] GameObject magaPlayer, guerreiroPlayer;
    [Header("Alvos")]
    [SerializeField] CameraSeguindo[] targetCamera;
    [SerializeField] SpawnerManager[] targetInimigos;
    [SerializeField] GameObject magaHUD, guerreiroHUD;
    public delegate void TerminouPlayer();
    public static event TerminouPlayer ManagerPlayerFinished;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("QuantosJogadores") == 1)
        {
            switch (PlayerPrefs.GetString("JogadorUm"))
            {
                case "Maga":
                    magaPlayer.SetActive(true);
                    magaHUD.SetActive(true);
                    targetCamera[0].target = magaPlayer.transform;
                    targetInimigos[0].qualAlvo = 0;
                    targetInimigos[0].target = magaPlayer.transform;
                    break;

                case "Guerreiro":
                    guerreiroPlayer.SetActive(true);
                    guerreiroHUD.SetActive(true);
                    targetCamera[0].target = guerreiroPlayer.transform;
                    targetInimigos[0].qualAlvo = 1;
                    targetInimigos[0].target = guerreiroPlayer.transform;
                    break;

            }
        }

        if (PlayerPrefs.GetInt("QuantosJogadores") == 2)
        {

        }

        if (ManagerPlayerFinished != null)
        {
            ManagerPlayerFinished();
        }
    }
}
