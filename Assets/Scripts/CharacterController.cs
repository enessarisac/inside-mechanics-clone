using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    Rigidbody rb;
    MoveObject moveObject;
    public GameObject other;
    public GameObject controll;
    public bool othercont=false;
   
    private void Start()
    {
        moveObject = controll.GetComponent<MoveObject>();
        rb = gameObject.GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (moveObject.isMine==true)
        {
            Movement();
            othercont = false;
        }
        if(moveObject.isMine==false)
        {
            MoveOther();
        }
    }
    
    public void Movement()
    {
        
        float horizontalInput = Input.GetAxis("Horizontal");
    float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();
        transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);
        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
    public void MoveOther()
    {
        othercont = true;
    }


}
