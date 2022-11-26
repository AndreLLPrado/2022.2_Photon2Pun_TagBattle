using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class NicknameUI : MonoBehaviour
{
    [SerializeField]
    private Text nickName;

    private Transform camera;
    void Start()
    {
        camera = Camera.main.transform;
        nickName.text = PhotonNetwork.NickName;
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + camera.rotation * Vector3.forward,
           camera.rotation * Vector3.up);
    }
}
