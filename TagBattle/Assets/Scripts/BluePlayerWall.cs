using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class BluePlayerWall : MonoBehaviourPunCallbacks, IPunObservable
{
    private PhotonView PV;

    [SerializeField]
    private int hp;

    [SerializeField]
    private Material damageMat;

    [SerializeField]
    private Material normalMat;

    [SerializeField]
    private Material lowHpMat;

    private Renderer renderer;

    void Start()
    {
        PV = GetComponent<PhotonView>();
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (PV.IsMine)
        {
            if (GameController.GC.getGameOver())
            {
                Destroy(gameObject);
            }

            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    takeDamage(1);

            //    if (hp <= 0)
            //    {
            //        StartCoroutine(damageVFX());
            //    }
            //}

        }
    }

    public void takeDamage(int damage)
    {
        StartCoroutine(damageVFX());
        if (hp > 0)
        {
            hp -= damage;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator damageVFX()
    {   
        renderer.material = damageMat;
        yield return new WaitForSeconds(.125f);
        renderer.material = lowHpMat;
        yield return new WaitForSeconds(.125f);
        renderer.material = damageMat;
        yield return new WaitForSeconds(.125f);
        renderer.material = normalMat;

        if(hp <= 0)
        {
            yield return new WaitForSeconds(.125f);
            renderer.material = lowHpMat;
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(hp);
        }
        else
        {
            hp = (int)stream.ReceiveNext();
        }
    }
}
