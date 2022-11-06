using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class TimerController : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField]
    private Text timerText;
    [SerializeField]
    private InputField timerIput;
    [SerializeField]
    private GameObject startButton;
    [SerializeField]
    private GameObject stopButton;
    [SerializeField]
    private GameObject InputButton;
    [SerializeField]
    private GameObject resetButton;

    [SerializeField]
    private GameObject gameOverPanel;

    public float timer;
    public float aux;
    public bool resetTimer;
    public bool startTimer;
    public bool endTimer;
    public bool panel;
    [SerializeField]
    private bool gmo;

    bool validValue;

    PhotonView PV;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
        setTimer(10);
        timerText.text = timer.ToString() + " s";

        if (PhotonNetwork.IsMasterClient)
        {
            InputButton.SetActive(true);
            startButton.SetActive(true);
            stopButton.SetActive(false);
        }
        else
        {
            InputButton.SetActive(false);
            startButton.SetActive(false);
            stopButton.SetActive(false);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if(PhotonNetwork.IsMasterClient)
            PV.RPC("RPC_SendTimer", RpcTarget.Others, timer);
    }

    private void Update()
    {
        if (GameController.GC.getCaptured())
        {
            endTimer = true;
            //GameControllera.GC.setGameOver(true);
        }
        TimerCoutDown();

    }
    private void TimerCoutDown()
    {
        if (startTimer && !endTimer)
        {
            timer -= Time.deltaTime;
        }
        else if (endTimer)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                panel = true;
                PV.RPC("RPC_SendActiveBool", RpcTarget.Others, panel);
            }
            gameOverPanel.SetActive(panel);

            if (PhotonNetwork.IsMasterClient)
                resetButton.SetActive(true);
            else
            {
                resetButton.SetActive(false);
            }
            //GameController.GC.restarGame();
        }

        if (timer <= 0)
        {
            endTimer = true;
            GameController.GC.setGameOver(true);
        }

        if (Input.GetKeyDown(KeyCode.R) && PhotonNetwork.IsMasterClient)
        {
            ResetTimer();
        }

        timerText.text = timer.ToString("F2") + " s";
    }
    private void StartTimer() 
    {
        startTimer = true;
    }
    private void ResetTimer()
    {
        startTimer = false;
        resetTimer = false;
        endTimer = false;
        timer = aux;
        timerText.text = timer.ToString() + " s";
        //GameController.GC.setCaptured(false);
        //GameController.GC.setGameOver(false);
        GameController.GC.restarGame();
        

        startButton.SetActive(true);
        stopButton.SetActive(false);

        if (PhotonNetwork.IsMasterClient)
        {
            panel = false;
            PV.RPC("RPC_SendActiveBool", RpcTarget.Others, panel);
        }
        gameOverPanel.SetActive(panel);
    }
    [PunRPC]
    public void RPC_SendActiveBool(bool panelIn)
    {
        panel = panelIn;
    }
    [PunRPC]
    public void RPC_SendTimer(float timeIn)
    {
        timer = timeIn;
    }
    public void setTimerClick()
    {
        string value = timerIput.text;
        
        if(value.Length > 0)
        {
            foreach(char c in value)
            {
                //ascii numbers keys id:
                /* 48 = 0
                 * 49 = 1
                 * 50 = 2
                 * 51 = 3
                 * 52 = 4
                 * 53 = 5
                 * 54 = 6
                 * 55 = 7
                 * 56 = 8
                 * 57 = 9
                 */
                if(c < 58 && c > 47)
                {
                    validValue = true;
                }
                else
                {
                    validValue = false;
                    break;
                }
            }
            if (validValue)
            {
                setTimer(float.Parse(value));
            }
        }
    }
    public void ResetClick()
    {
        ResetTimer();
    }
    public void StartClick()
    {
        startTimer = true;
        if (PhotonNetwork.IsMasterClient)
        {
            startButton.SetActive(false);
            stopButton.SetActive(true);
        }
        else
        {
            startButton.SetActive(false);
            stopButton.SetActive(false);
        }
    }
    public void StopClick()
    {
        startTimer = false;
        if (PhotonNetwork.IsMasterClient)
        {
            startButton.SetActive(true);
            stopButton.SetActive(false);
        }
        else
        {
            startButton.SetActive(false);
            stopButton.SetActive(false);
        }
    }
    public void setTimer(float timer)
    {
        this.timer = timer;
        aux = timer;
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(timer);
        }
        else
        {
            timer = (float)stream.ReceiveNext();
        }
    }
}
