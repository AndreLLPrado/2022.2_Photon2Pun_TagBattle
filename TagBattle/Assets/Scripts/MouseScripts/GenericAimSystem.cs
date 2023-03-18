using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GenericAimSystem : MonoBehaviourPunCallbacks
{
    //mouse
    [SerializeField]
    private Transform pointer;
    [SerializeField]
    private Vector3 mouseCords;

    //network
    private PhotonView PV;

    //logic
    [SerializeField]
    private float range;
    private bool ready;
    private bool inRange;
    [SerializeField]
    private float countDown;
    private float countDownAux;
    
    enum playerMode { red, blue}
    private playerMode PL;

    //UI
    private LineRenderer ui_range;
    [SerializeField]
    private Material outRangeMaterial;
    [SerializeField]
    private Material inRangeMaterial;

    private void Start()
    {
        ui_range = GetComponent<LineRenderer>();
        PV = GetComponent<PhotonView>();

        ready = true;
        // countDownAux = countDown;
        Debug.Log("Player mode: " + PL.ToString());
    }

    private void Update()
    {
        if (PV.IsMine)
        {
            aimSystem();
        }
    }

    private void aimSystem()
    {
        mouseCords = GetComponentInParent<MousePostinosScreen>().getMousePos();
        mouseCords.y = 1f;

        pointer.transform.position = mouseCords;
        ui_range.SetPosition(0, new Vector3(transform.position.x, 1.0f, transform.position.z));
        ui_range.SetPosition(1, mouseCords);

        //range calc
        float actualDist = Mathf.Pow((mouseCords.x - transform.position.x), 2) + Mathf.Pow((mouseCords.z - transform.position.z), 2);
        // Debug.Log("Distance between player and the pointer: " + actualDist.ToString());
        if(actualDist < range)// In range
        {
            ui_range.material = inRangeMaterial;
            inRange = true;
        }
        else// Out of range
        {
            ui_range.material = outRangeMaterial;
            inRange = false;
        }

        

        if (PL == playerMode.red)// Is red player
        {
            shootingSystem();
        }
        else // Is blue player
        {

        }
    }
    private void shootingSystem() 
    {
        GameController.GC.setShootCountingDown(countDown);
        if (ready)
        {
            //mouse click
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 100f))
                {
                    Debug.Log("raycast: " + hit.transform.name);
                    if (inRange)
                    {
                        if (hit.transform.tag == "blueWall")
                        {
                            hit.transform.GetComponent<BluePlayerWall>().takeDamage(1);
                        }
                        ready = false;
                    }
                    else
                    {
                        Debug.Log("Out of range!");
                    }
                }
            }
        }
        else
        {
            StartCoutdown();
        }
    }
    private void StartCoutdown()
    {
        countDown -= Time.deltaTime;

        if (countDown <= 0)
        {
            ready = true;
            countDown = countDownAux;
        }
    }

    public void setCountDown(float c)
    {
        countDown = countDownAux = c;
    }

    public void setRange(float range)
    {
        this.range = range;
    }
    public void setPlayerMode(int mode)
    {
        if(mode == 0)
        {
            PL = playerMode.red;
        }
        else if (mode == 1)
        {
            PL = playerMode.blue;
        }
        else
        {
            Debug.LogError("Invalid mode value! " + mode);
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
