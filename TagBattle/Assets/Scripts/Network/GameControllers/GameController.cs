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
            }
        }
    }

    private void Update()
    {
        if (timer.GetComponent<TimerController>().endTimer)
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

    [PunRPC]
    public void RPC_SendCaptured(bool capturedIn) 
    {
        captured = capturedIn;
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
