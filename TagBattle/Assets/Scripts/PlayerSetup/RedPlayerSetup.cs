using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPlayerSetup : MonoBehaviour
{
    private void Start()
    {
        GetComponentInParent<PlayerMoviment>().speed = 6;
        // GetComponentInParent<PlayerCollisions>().setPlayerMode(1);
        GetComponentInParent<GenericAimSystem>().setPlayerMode(0);//0 - red, 1 - blue
        GetComponentInParent<GenericAimSystem>().setRange(100);
        GetComponentInParent<GenericAimSystem>().setCountDown(2.5f);
        GetComponentInParent<CollisionController>().setPlayerMode("red");
    }
}
