using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BluePlayerWall : MonoBehaviour
{
    private PhotonView PV;
    void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (PV.IsMine)
        {
            if (GameController.GC.getGameOver())
            {
                Destroy(gameObject);
            }
        }
    }
}
