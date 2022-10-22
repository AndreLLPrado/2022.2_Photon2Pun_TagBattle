using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Timer : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField] Text timer;
    [SerializeField] InputField timerValue;
    public float time;
    private float aux;
    public bool sTimer;
    public bool rTimer;
    public bool eTime;
    bool validValue;
    //string value;

    PhotonView view;


    private void Start()
    {
        view = GetComponent<PhotonView>();
        //aux = time;
        timer.text = time.ToString() + " s";

        setNewValues(10);

        //aux = time;

        
    }

    private void Update()
    {
        if (view.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                rTimer = true;
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                sTimer = true;
            }

            if (!sTimer)
            {
                UpdateTimer();
            }

            if (sTimer && !eTime)
            {
                view.RPC("RPC_StartTimer",RpcTarget.All);
            }

            if (rTimer)
            {
                view.RPC("RPC_ResetTimer", RpcTarget.All);
            }

            if (eTime)
            {
                time = 0.0f;
                timer.text = time.ToString() + " s";
            }
        }
    }

    [PunRPC]
    void RPC_StartTimer()
    {
        
        /*if (!GameObject.Find("GameManager").GetComponent<GameManagerScript>().tPause)
        {
            time -= 1 * Time.deltaTime;
        }*/
        //time -= 1 * Time.deltaTime;
        //time -= 1 * PhotonNetwork.Time;

        timer.text = time.ToString("F2") + " s";
        if(time <= 0)
        {
            eTime = true;
        }
    }

    [PunRPC]
    void RPC_ResetTimer()
    {
        sTimer = false;
        rTimer = false;
        eTime = false;
        time = aux;
        timer.text = time.ToString() + " s";
    }

    [PunRPC]
    void RPC_SetTimerValue()
    {
        string value = timerValue.text;
        //value = timerValue.text;

        if (value.Length > 0)
        {
            Debug.Log("values Length:" + value.Length.ToString());
            
            for (int i = 0; i < value.Length; i++)
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
                if (value[i] < 58 && value[i] > 47)
                /*if (value[i] < '0' || value[i] > '1' || value[i] > '2' || value[i] > '3' || value[i] > '4' || value[i] > '5'
                    || value[i] > '6' || value[i] > '7' || value[i] > '8' || value[i] > '9')*/
                {
                    Debug.Log((i + 1).ToString() + "° Digito válido");
                    validValue = true;
                }
                else
                {
                    Debug.Log((i + 1).ToString() + "° Digito inválido");
                    validValue = false;
                    break;
                }
            }
            if (validValue)
            {
                time = float.Parse(value);
                //aux = time;
                //timer.text = time.ToString() + " s";
                UpdateTimer();
            }
            else
            {
                Debug.LogError("Invalid String");
            }
        }
        
    }

    public void setTimerButton()
    {
        Debug.Log("Set Timer Button Clicked!");
        if (view.IsMine)
        {
            if (!sTimer) 
            {
                Debug.Log("Start setting timer values");
                view.RPC("RPC_SetTimerValue", RpcTarget.All);
            }
        }
        UpdateTimer();
    }

    void UpdateTimer()
    {
        aux = time;
        timer.text = time.ToString() + " s";
    }
    void setNewValues(float t)
    {
        aux = time = t;
        timer.text = t.ToString() + " s";
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(time);
        }
        else
        {
            time = (float)stream.ReceiveNext();
        }
    }
}
