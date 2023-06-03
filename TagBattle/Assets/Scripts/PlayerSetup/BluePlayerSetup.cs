using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePlayerSetup : MonoBehaviour
{
    private void Start()
    {
        GetComponentInParent<PlayerMoviment>().speed = 5;
        // GetComponentInParent<PlayerMode>().setMyMode(2);
        GetComponentInParent<PlayerCollisions>().setPlayerMode(2);
        GetComponentInParent<GenericAimSystem>().setPlayerMode(1);//0 - red, 1 - blue
        GetComponentInParent<GenericAimSystem>().setRange(50);
        GetComponentInParent<GenericAimSystem>().setCountDown(5);
        GetComponentInParent<GenericAimSystem>().setNumbersOfWalls(2);
        GetComponentInParent<CollisionController>().setPlayerMode("blue");
        GameController.GC.setMaxWalls(2);
    }
}
