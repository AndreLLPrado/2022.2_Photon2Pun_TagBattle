using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class StartGameConfigController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text title;

    [SerializeField]
    private GameObject masterConfigs;

    [SerializeField]
    private Button startGame;

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            title.text = "Game configuration";
            // GameController.GC.setGameOver(true);
        }
        else
        {
            title.text = "Please Wait!";
            // startGame.enabled = false;
            masterConfigs.SetActive(false);
        }
    }

    private void Update()
    {
        if (!GameController.GC.getGameOver())
        {
            gameObject.SetActive(false);
        }
    }

    public void closeConfig()
    {
        GameController.GC.disableGameConfigPanel();
        Debug.LogWarning("entrou aqui!");
    }
}
