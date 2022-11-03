using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviourPunCallbacks
{
    private int playerMode;
    private PhotonView PV;

    private void Start()
    {
        playerMode = GetComponent<PlayerMode>().getMyMode();
        PV = GetComponent<PhotonView>();
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
                        Debug.Log("Red Win!");
                    }
                }
            }
        }
    }
}
