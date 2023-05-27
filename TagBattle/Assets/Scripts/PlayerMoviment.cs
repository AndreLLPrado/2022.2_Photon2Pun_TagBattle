using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMoviment : MonoBehaviour
{
    CharacterController cc;
    Vector3 move;
    PhotonView view;

    private Rigidbody rb;

    public float speed;
    public float grativy;

    bool posRes = false;

    // Debug
    private BoxCollider boxCollider;
    private Vector3 colliderSize;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
        view = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody>();

        boxCollider = GetComponent<BoxCollider>();
        colliderSize = boxCollider.size;
    }

    void Update()
    {
        if (view.IsMine && !GameController.GC.getGameOver())
        {
            // MovePlayer();
            MovePlayerByPhysics();
        }
    }

    void GamerOverRespawner(bool p)
    {
        float x, z;

        cc.enabled = !p;
        if (p && !posRes)
        {
            x = Random.Range(-7.5f, 8f);
            z = Random.Range(-8.5f, 8.5f);
            transform.position = new Vector3(x, 10f, z);
            posRes = true;
        }
    }
    void RespawnPlayer()
    {
        cc.enabled = false;
        transform.position = new Vector3(0f, 10f, 0f);
        cc.enabled = true;
    }

    private void MovePlayerByPhysics()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical) * speed;

        rb.velocity = movement;
    }
    private void MovePlayer()
    {
        if (cc.isGrounded)
        {
            move = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            move = transform.TransformDirection(move);
            move *= speed;
        }

        move.y -= grativy * Time.deltaTime;

        cc.Move(move * Time.deltaTime);
    }
}
