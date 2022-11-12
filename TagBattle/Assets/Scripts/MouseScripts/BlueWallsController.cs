using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BlueWallsController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private float range;
    [SerializeField]
    private LayerMask mask;

    [SerializeField]
    private LineRenderer ui_range;

    [SerializeField]
    private Vector3 mouseCord;

    private PhotonView PV;

    private void Start()
    {
        ui_range = GetComponent<LineRenderer>();
        PV = GetComponentInParent<PhotonView>();
    }
    private void Update()
    {
        ui_range.SetPosition(0, new Vector3(transform.position.x, 1.0f, transform.position.z));
        mouseCord = GetComponentInParent<MousePostinosScreen>().getMousePos();
        mouseCord.y = 1;

        if (Physics.Raycast(transform.position, mouseCord, out RaycastHit hit, range))
        {
            ui_range.SetPosition(1, new Vector3(mouseCord.x, 1, mouseCord.z));
        }
        else
        {
            ui_range.SetPosition(1, new Vector3(mouseCord.x, 1, mouseCord.z));
        }
    }
}
