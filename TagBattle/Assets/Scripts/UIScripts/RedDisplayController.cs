using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedDisplayController : MonoBehaviour
{
    [SerializeField]
    private Text shootingText;

    [SerializeField]
    private Text shootingCountDown;

    private void Update()
    {
        shootingCountDown.text = GameController.GC.getShootCountingDown().ToString("F2");
        shootingText.text = "Reloading!";
    }
}
