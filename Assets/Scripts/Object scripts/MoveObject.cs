using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public bool isMine;
    
    PlayerController playerController;
    public GameObject player;
    private void Start()
    {
       
        playerController=player.GetComponent<PlayerController>();
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E))
            {
               
                playerController.MoveOther();
                isMine=false;
            }
        }
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Q) && !isMine)
        {
            
            playerController.Movement();
            isMine=true;
        }
    }
}
