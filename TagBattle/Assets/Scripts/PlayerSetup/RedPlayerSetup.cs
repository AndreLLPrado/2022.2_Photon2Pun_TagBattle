using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPlayerSetup : MonoBehaviour
{
    private void Start()
    {
        GetComponentInParent<PlayerMoviment>().speed = 6;
        GetComponentInParent<PlayerMode>().setMyMode(1);
        GetComponentInParent<PlayerCollisions>().setPlayerMode(1);
    }
}
