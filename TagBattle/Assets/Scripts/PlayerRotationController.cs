using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerRotationController : MonoBehaviourPunCallbacks
{
    private PhotonView PV;
    void Start()
    {
        PV = GetComponentInParent<PhotonView>();
    }

    void Update()
    {
        if(PV.IsMine && !GameController.GC.getGameOver())
        {
            RotationPlayer();
        }
    }

    private void RotationPlayer()
    {
        float horizontalAxis = Input.GetAxisRaw("Horizontal") * -90f;
        float verticalAxis = Input.GetAxisRaw("Vertical") * 180;
        float rotation = (verticalAxis - horizontalAxis);


        transform.eulerAngles = new Vector3(0.0f, rotation, 0.0f);
    }
}
