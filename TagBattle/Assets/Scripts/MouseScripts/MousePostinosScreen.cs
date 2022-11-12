using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class MousePostinosScreen : MonoBehaviour
{
    [SerializeField]
    private Vector3 mousePos;
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            mousePos = hit.point;
        }
    }

    public Vector3 getMousePos()
    {
        return mousePos;
    }
}
