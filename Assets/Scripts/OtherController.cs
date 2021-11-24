using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherController : MonoBehaviour
{
    public float speed, rotationSpeed;
    CharacterController characterController;
    public GameObject player;
    private void Start()
    {
        characterController = player.GetComponent<CharacterController>();
    }
    private void Update()
    {
        if (characterController.othercont==true)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);
            transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime);
        }

        
    }
}
