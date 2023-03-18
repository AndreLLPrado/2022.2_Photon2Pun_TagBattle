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

    // Blue Player Mechenics
    [SerializeField]
    private int wallCount;
    [SerializeField]
    private int maxWalls;
    [SerializeField]
    private float wallCountingDown;

    // Red Player Mechenics
    [SerializeField]
    private float shootCountingDown;

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
        setWallCount(0);
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
        setWallCount(0);
        if (PhotonNetwork.IsMasterClient)
        {
            PV.RPC("RPC_SendCaptured", RpcTarget.Others, captured);
            PV.RPC("RPC_SendGameOver", RpcTarget.Others, gameOver);
        }
        foreach(GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            int spawnPicker = Random.Range(0, GameSetup.GS.spawnPoints.Length);
            player.transform.SetPositionAndRotation(GameSetup.GS.spawnPoints[spawnPicker].position, Quaternion.identity);
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

    public void setShootCountingDown(float shootCountingDown)
    {
        this.shootCountingDown = shootCountingDown;
    }
    public float getShootCountingDown()
    {
        return shootCountingDown;
    }
    public void setMaxWalls(int maxWall)
    {
        this.maxWalls = maxWall;
    }
    public int getMaxWalls()
    {
        return maxWalls;
    }

    public void setWallCount(int wallCount)
    {
        this.wallCount = wallCount;
    }
    public int getWallCount()
    {
        return wallCount;
    }
    public void addWallCount()
    {
        this.wallCount++;
    }
    public void dropWallCount()
    {
        this.wallCount--;
    }

    public void setWallCountingDown(float wallCountingDown)
    {
        this.wallCountingDown = wallCountingDown;
    }
    public float getWallCountingDown()
    {
        return wallCountingDown;
    }

    public bool getGameOver()
    {
        return gameOver;
    }
    public void setGameOver(bool gmo)
    {
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
