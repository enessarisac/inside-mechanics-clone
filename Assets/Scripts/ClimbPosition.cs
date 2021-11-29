using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbPosition : MonoBehaviour
{
    BoxCollider coll;
    Rigidbody rb;
    Animator anim;
    public GameObject player;
    CharacterController characterController;
    public Transform climbPos;
    private void Start() {
        coll=gameObject.GetComponentInParent<BoxCollider>();
        rb=gameObject.GetComponentInParent<Rigidbody>();
        characterController=player.GetComponent<CharacterController>();
        anim=gameObject.GetComponentInParent<Animator>();
        climbPos=GetComponent<Transform>();
    }
    private void FixedUpdate() {
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Climb")&&characterController.climb==true)
            {   
                   player.transform.position=climbPos.transform.position;
                  
            }
    }
    
    
}
