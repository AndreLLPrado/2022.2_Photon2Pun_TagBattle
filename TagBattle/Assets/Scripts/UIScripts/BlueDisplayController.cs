using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueDisplayController : MonoBehaviour
{
    [SerializeField]
    private Text wallCounter;

    [SerializeField]
    private Text wallTimer;

    private void Update()
    {
        wallCounter.text = GameController.GC.getWallCount().ToString() + " / " +
            GameController.GC.getMaxWalls().ToString();

        if(GameController.GC.getWallCountingDown() >= 5 && 
            GameController.GC.getWallCount() < GameController.GC.getMaxWalls())
        {
            wallTimer.text = "Ready to Create!";
        }
        else if (GameController.GC.getWallCount() >= GameController.GC.getMaxWalls())
        {
            wallTimer.text = "No more walls!";
        }
        else
        {
            wallTimer.text = GameController.GC.getWallCountingDown().ToString("F2");
        }
    }
}
