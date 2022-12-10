using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class BlueWallsController : MonoBehaviourPunCallbacks, IPunObservable
{
    //Logical
    [SerializeField]
    private float range;
    [SerializeField]
    private int numberOfWalls;
    [SerializeField]
    private float countDown;
    [SerializeField]
    private float wallAngle;
    private float countDownAux;
    private bool redyToCreate;

    //UI
    private LineRenderer ui_range;
    [SerializeField]
    private Material outRangeMaterial;
    [SerializeField]
    private Material inRangeMaterial;
    [SerializeField]
    private GameObject aim;

    [SerializeField]
    private Vector3 mouseCord;

    private PhotonView PV;

    private void Start()
    {
        ui_range = GetComponent<LineRenderer>();
        PV = GetComponentInParent<PhotonView>();

        redyToCreate = true;
        countDownAux = countDown;
        wallAngle = 0;

        GameController.GC.setMaxWalls(numberOfWalls);
    }
    private void Update()
    {
        GameController.GC.setWallCountingDown(countDown);
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
        mouseCord = GetComponentInParent<MousePostinosScreen>().getMousePos();
        mouseCord.y = 1;

        ui_range.SetPosition(1, new Vector3(mouseCord.x, 1, mouseCord.z));
        //aim.gameObject.transform.Translate(new Vector3(mouseCord.x, 1, mouseCord.z));
        aim.gameObject.transform.position = new Vector3(mouseCord.x, 1, mouseCord.z);
        if (!GameController.GC.getGameOver())
        {
            aim.gameObject.transform.eulerAngles = new Vector3(0.0f, wallAngle, 0.0f);
        }

        if (Physics.Raycast(transform.position, mouseCord, out RaycastHit hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Distance: " + hit.distance + " Range: " + range + "\n" + 
                    "Point: " + mouseCord);
            }
            if(hit.distance > range)
            {
                //Out of range
                ui_range.material = outRangeMaterial;
                aim.gameObject.GetComponent<Renderer>().material = outRangeMaterial;
            }
            else
            {
                //In range
                ui_range.material = inRangeMaterial;
                aim.gameObject.GetComponent<Renderer>().material = inRangeMaterial;
                if (Input.GetMouseButtonDown(0) && !GameController.GC.getGameOver())
                {
                    createWall(mouseCord);
                }

                if(Input.GetMouseButtonDown(1) && !GameController.GC.getGameOver())
                {
                    if(wallAngle < 360)
                    {
                        wallAngle += 90;                      
                    }
                    else
                    {
                        wallAngle = 0;
                    }
                }
            }
        }
    }
    private void createWall(Vector3 clickPos)
    {
        if (redyToCreate)
        {
            if(numberOfWalls > 0)
            {
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "BlueWall"), clickPos,
                    transform.rotation * Quaternion.Euler(new Vector3(0f, wallAngle, 0f)));
                numberOfWalls--;
                GameController.GC.addWallCount();
                redyToCreate = false;
            }
        }
    }

    private void StartCoutdown()
    {
        countDown -= Time.deltaTime;

        if(countDown <= 0)
        {
            redyToCreate = true;
            countDown = countDownAux;
        }
    }

    [PunRPC]
    private void RPC_SendCountdown(float countIn)
    {
        countDown = countIn;
    }

    [PunRPC]
    private void RPC_SendRedy(bool redyIn)
    {
        redyToCreate = redyIn;
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
