using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DisplayController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject BluePlayerDisplay;

    [SerializeField]
    private GameObject RedPlayerDisplay;

    private PhotonView PV;

    [SerializeField]
    private int myMode;

    private void Start()
    {
        PV = GetComponent<PhotonView>();

        BluePlayerDisplay.SetActive(false);
        RedPlayerDisplay.SetActive(false);

        myMode = PlayerPrefs.GetInt("MyCharacter");
        Debug.Log(PhotonNetwork.NickName + ": My mode is: " + myMode.ToString());
        if (myMode == 0)//Blue
        {
            BlueDisplayController();
        }
        else if (myMode == 1)//Red
        {
            RedDisplayController();
        }
        else //Invalid Mode
        {

        }
    }

    private void BlueDisplayController()
    {
        BluePlayerDisplay.SetActive(true);
        RedPlayerDisplay.SetActive(false);
    }

    private void RedDisplayController()
    {
        BluePlayerDisplay.SetActive(false);
        RedPlayerDisplay.SetActive(true);
    }
}
