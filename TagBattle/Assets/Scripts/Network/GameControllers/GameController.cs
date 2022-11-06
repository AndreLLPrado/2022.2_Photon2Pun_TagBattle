using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviourPunCallbacks, IPunObservable
{
    public static GameController GC;

    [SerializeField]
    private Text gameOverPanelText;
    [SerializeField]
    private GameObject timer;

    [SerializeField]
    private bool captured;
    [SerializeField]
    private bool gameOver;

    private PhotonView PV;

    private void OnEnable()
    {
        if (GameController.GC == null)
        {
            GameController.GC = this;
        }
        else
        {
            Destroy(GameController.GC.gameObject);
            GameController.GC = this;
        }
    }
    
    private void Start()
    {
        PV = GetComponent<PhotonView>();
        if (PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PV.RPC("RPC_SendCaptured", RpcTarget.Others, captured);
                PV.RPC("RPC_SendGameOver", RpcTarget.Others, gameOver);
            }
        }
    }

    private void Update()
    {
        if (captured)
        {
            setGameOver(true);
        }

        if (gameOver)
        {
            gamerOver();
        }
    }

    private void gamerOver()
    {
        if (captured)
        {
            //blue win!
            gameOverPanelText.text = "Blue player Win!";
            gameOverPanelText.color = Color.blue;
        }
        else
        {
            //red win!
            gameOverPanelText.text = "Red player Win!";
            gameOverPanelText.color = Color.red;
        }
    }

    public void restarGame()
    {
        setCaptured(false);
        setGameOver(false);
        if (PhotonNetwork.IsMasterClient)
        {
            PV.RPC("RPC_SendCaptured", RpcTarget.Others, captured);
            PV.RPC("RPC_SendGameOver", RpcTarget.Others, gameOver);
        }
    }

    [PunRPC]
    public void RPC_SendCaptured(bool capturedIn) 
    {
        captured = capturedIn;
    }
    [PunRPC]
    public void RPC_SendGameOver(bool gameOverIn)
    {
        gameOver = gameOverIn;
    }

    public bool getGameOver()
    {
        return gameOver;
    }
    public void setGameOver(bool gmo)
    {
        //if(PhotonNetwork.IsMasterClient)
        //    PV.RPC("RPC_SendGameOver", RpcTarget.All, gmo);
        //gameOver = gmo;

        gameOver = gmo;
    }
    public bool getCaptured()
    {
        return captured;
    }
    public void setCaptured(bool cap)
    {
        captured = cap;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(captured);
        }
        else
        {
            captured = (bool)stream.ReceiveNext();
        }
    }
}
