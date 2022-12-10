using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class NicknameUI : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text nickName;

    private Transform camera;
    private PhotonView PV;

    void Start()
    {
        camera = Camera.main.transform;
        nickName.text = PhotonNetwork.NickName;

        PV = GetComponentInParent<PhotonView>();
    }

    private void LateUpdate()
    {
        if (PV.IsMine)
        {
            transform.LookAt(transform.position + camera.rotation * Vector3.forward,
               camera.rotation * Vector3.up);
        }
    }
}
