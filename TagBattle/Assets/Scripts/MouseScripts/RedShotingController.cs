using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RedShotingController : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField]
    private Vector3 mouseCords;

    private PhotonView PV;

    //Logic
    [SerializeField]
    private float range;
    [SerializeField]
    private LayerMask blueWall;
    private bool redyToCreate;
    [SerializeField]
    private float countDown;
    private float countDownAux;

    //UI
    private LineRenderer ui_range;
    [SerializeField]
    private Material outRangeMaterial;
    [SerializeField]
    private Material inRangeMaterial;
    [SerializeField]
    private Material redyToShoot;

    private void Start()
    {
        ui_range = GetComponent<LineRenderer>();
        PV = GetComponentInParent<PhotonView>();

        redyToCreate = true;
        countDownAux = countDown;
    }

    private void Update()
    {
        GameController.GC.setShootCountingDown(countDown);
        if (PV.IsMine)
        {
            aimSystem();
            if (!redyToCreate)
            {
                StartCoutdown();
            }
        }
    }
    private void aimSystem()
    {
        ui_range.SetPosition(0, new Vector3(transform.position.x, 1.0f, transform.position.z));
        mouseCords = GetComponentInParent<MousePostinosScreen>().getMousePos();
        mouseCords.y = 1.0f;

        ui_range.SetPosition(1, new Vector3(mouseCords.x, 1, mouseCords.z));

        if (Physics.Raycast(transform.position, mouseCords, out RaycastHit hit, blueWall))
        {
            if (hit.distance > range)
            {
                //Out of range
                ui_range.material = outRangeMaterial;
            }
            else
            {
                //In range
                ui_range.material = inRangeMaterial;
                if (Input.GetMouseButtonDown(0))
                {

                    Debug.Log("OBJ Tag: " + hit.transform.tag + " OBJ Name: " + hit.transform.name);
                    redyToCreate = false;
                }
                if (hit.collider.gameObject.layer == blueWall)
                {
                    //Redy to Shoot
                    ui_range.material = redyToShoot;
                }
            }
        }
    }
    private void StartCoutdown()
    {
        countDown -= Time.deltaTime;

        if (countDown <= 0)
        {
            redyToCreate = true;
            countDown = countDownAux;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(countDown);
        }
        else
        {
            countDown = (float)stream.ReceiveNext();
        }
    }
}
