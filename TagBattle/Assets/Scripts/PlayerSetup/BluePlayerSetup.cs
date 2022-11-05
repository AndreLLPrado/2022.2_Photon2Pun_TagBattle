using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePlayerSetup : MonoBehaviour
{
    private void Start()
    {
        GetComponentInParent<PlayerMoviment>().speed = 5;
        GetComponentInParent<PlayerMode>().setMyMode(2);
        GetComponentInParent<PlayerCollisions>().setPlayerMode(2);
    }
}
