using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private int playerMode;
    private PhotonView PV;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    public void setPlayerMode(int mode)
    {
        playerMode = mode;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.gameObject.tag == "Player")
        {
            int otherPlayerMode = hit.collider.gameObject.GetComponent<PlayerMode>().getMyMode();
            Debug.Log("Collide with: " + hit.collider.gameObject.tag + " his mode is: " + otherPlayerMode);
            if (PhotonNetwork.IsMasterClient)
            {
                Debug.Log("Marter Client!");
                if(otherPlayerMode > 0 && playerMode > 0)
                {
                    Debug.Log("Valid Values");
                    if(playerMode != otherPlayerMode)
                    {
                        Debug.Log("Blue Win!");
                        GameController.GC.setCaptured(true);
                        GameController.GC.setGameOver(true);
                    }
                }
            }
        }
    }
}
