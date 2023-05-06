using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerRotationController : MonoBehaviourPunCallbacks
{
    private PhotonView PV;
    private Vector3 targetRotation;
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
        float horizontalAxis = Input.GetAxisRaw("Horizontal");
        float verticalAxis = Input.GetAxisRaw("Vertical");
        float rotation = Mathf.Atan2(horizontalAxis, verticalAxis) * Mathf.Rad2Deg;

        targetRotation = new Vector3(0.0f, rotation, 0.0f);
        transform.eulerAngles = targetRotation;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting) // enviar a rotação do jogador para outros jogadores
        {
            stream.SendNext(targetRotation);
        }
        else // receber a rotação do jogador dos outros jogadores
        {
            targetRotation = (Vector3)stream.ReceiveNext();
        }
    }
}
