using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerReposition : MonoBehaviourPunCallbacks
{
    public void Reposition()
    {
        int spawnPicker = Random.Range(0, GameSetup.GS.spawnPoints.Length);

        gameObject.transform.Translate(GameSetup.GS.spawnPoints[spawnPicker].position);
        Debug.Log("Translate this pos: " + transform.position + " to: " + GameSetup.GS.spawnPoints[spawnPicker].position);
    }
}
