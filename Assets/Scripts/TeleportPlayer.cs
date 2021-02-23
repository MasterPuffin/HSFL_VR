using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{

    Animator anim;
    public string boolName;        
    int boolID;

    Transform player;

    bool canMove = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        boolID = Animator.StringToHash(boolName);
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void StartTransition()
    {
        anim.SetBool(boolID, true);
    }

    public void Teleport()      //wenn der Bildschirm schwarz ist, dann canMove
    {
        canMove = true;     
    }

    public bool GetCanMove()
    {
        return canMove;
    }

    public void FadeIn()
    {
        anim.SetBool(boolID, false);
        canMove = false;
    }


}
