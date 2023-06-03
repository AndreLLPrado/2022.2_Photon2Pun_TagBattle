using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CollisionController : MonoBehaviourPunCallbacks
{
    private enum PlayerMode {red, blue};

    RaycastHit hit;
    [SerializeField] LayerMask layer;
    [SerializeField] float sphereRadius = 2f;
    float sphereDistance = 10f;
    public bool drawSphereCast = true;
    public Vector3 sphereOffset = Vector3.zero;
    public Vector3 sphereCenter = Vector3.zero;
    [SerializeField] float corectionZ = 0f;
    [SerializeField] float corectionY = 0f;
    [SerializeField] private PlayerMode playerMode;
    PhotonView PV;
    void Start()
    {
        PV = gameObject.GetComponent<PhotonView>();
    }

    void Update()
    {
        if (PV.IsMine)
        {
            sphereCenter = transform.position + sphereOffset;
            sphereCenter.z += corectionZ;
            sphereCenter.y += corectionY;
            if (Physics.SphereCast(sphereCenter, sphereRadius, transform.forward, out hit, sphereDistance, layer))
            {
                Debug.Log(hit.transform.gameObject.name);
                //win coditions
                string otherPlayerMode = hit.transform.gameObject.GetComponent<CollisionController>().GetPlayerMode();
                if(playerMode == PlayerMode.red)
                {
                    if (otherPlayerMode == "red")
                    {
                        //DRAW
                    }
                    else if(otherPlayerMode == "blue")
                    {
                        //LOSE
                        Debug.Log("Blue Win!");
                        GameController.GC.setCaptured(true);
                        GameController.GC.setGameOver(true);
                    }
                    else
                    {
                        //INVALID
                    }
                }
                else if(playerMode == PlayerMode.blue)
                {
                    if (otherPlayerMode == "red")
                    {
                        //WIN
                        Debug.Log("Blue Win!");
                        GameController.GC.setCaptured(true);
                        GameController.GC.setGameOver(true);
                    }
                    else if (otherPlayerMode == "blue")
                    {
                        //DRAW
                    }
                    else
                    {
                        //INVALID
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (drawSphereCast)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(sphereCenter + transform.forward * sphereDistance, sphereRadius);
        }
    }

    public void setPlayerMode(string pMode)
    {
        if(pMode == "red")
        {
            playerMode = PlayerMode.red;
        }
        else if(pMode == "blue")
        {
            playerMode = PlayerMode.blue;
        }
        else
        {
            Debug.LogError("Inválid Mode");
        }
    }

    public string GetPlayerMode()
    {
        if (playerMode == PlayerMode.red)
            return "red";
        else
            return "blue";
    }
}
