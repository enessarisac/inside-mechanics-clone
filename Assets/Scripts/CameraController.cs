using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    CharacterController characterController;
    public GameObject other;

    private Vector3 offset;


    void Start()
    {
        characterController = player.GetComponent<CharacterController>();
        offset = transform.position - player.transform.position;
    }


    void LateUpdate()
    {
        if (characterController.othercont == false)
        {
            transform.position = player.transform.position + offset;
        }
        
        if (characterController.othercont == true)
        {
            transform.position = other.transform.position + offset;
        }
    }
}
