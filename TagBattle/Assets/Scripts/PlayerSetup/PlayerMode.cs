using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMode : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private int myMode;//0 = no mode,1 = red, 2 = blue

    private PhotonView PV;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    public int getMyMode()
    {
        return myMode;
    }

    public void setMyMode(int mMode)
    {
        myMode = mMode;
    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Enter with: " + collision.gameObject.name);
        if(collision.gameObject.GetComponent<PlayerMode>().getMyMode() == 1)
        {
            GameController.GC.setCaptured(true);
        }
    }
}
