using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMode : MonoBehaviourPunCallbacks
{
    [SerializeField]
    public int myMode;//0 = no mode,1 = red, 2 = blue

    public int getMyMode()
    {
        return myMode;
    }

    public void setMyMode(int mMode)
    {
        myMode = mMode;
    }
}
