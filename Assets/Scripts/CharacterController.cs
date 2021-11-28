using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float speed;
    public float jumpSpeed;
    public float rotationSpeed;
    public GameObject text;
    Animator anim;
    Rigidbody rb;
    MoveObject moveObject;
    public GameObject controll;
    public bool isGround;
    public bool othercont=false;
    public bool isRunning;
    private float decimalEnergy=5;
    public int energy;
    public bool canSprint,sprint;
    TakeObject takeObjects;
    public GameObject takingObj;
    private void Start()
    {
        takeObjects=takingObj.GetComponent<TakeObject>();
        anim = gameObject.GetComponent<Animator>();
        moveObject = controll.GetComponent<MoveObject>();
        rb = gameObject.GetComponent<Rigidbody>();
    }
    private void Update()
    {
        energy = Mathf.RoundToInt(decimalEnergy);

        if (moveObject.isMine==true&&takeObjects.isHolding==false)
        {
            Movement();
            
            othercont = false;
        }
        if(moveObject.isMine==false)
        {
            MoveOther();
            
        }
        if (isRunning&&isGround)
        {
            anim.SetBool("isRunning", true);
        }
        if(!isRunning)
        {
            anim.SetBool("isRunning", false);
        }
        if (isRunning && canSprint)
        {
            
             if (Input.GetKey(KeyCode.LeftShift))
                {
               sprint = true;
                if (sprint == true)
                         {
                             speed = 7;
                             decimalEnergy -= 1 * Time.deltaTime;
                         }
                
                }
                
        }
        else if (!isRunning || canSprint==false)
        {
            sprint = false;
        }


        if (energy == 5)
        {
            canSprint = true;
        }
        if (energy == 0)
        {
            canSprint = false;
           
        }
        if (energy != 5 && sprint == false)
        {
            decimalEnergy += 1 * Time.deltaTime;
            if (energy == 5)
            {
                canSprint = true;
            }

            
        }
        if (sprint == false)
        {
            speed = 5;
        }
        if (!canSprint)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                text.SetActive(true);

            }
        }
        if (canSprint)
        {
           text.SetActive(false);
        }
        if(takeObjects.isHolding==true)
        {
            PushMovement();
 
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        isGround = false;
    }

    public void Movement()
    {
        
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();
        if (horizontalInput != 0 || verticalInput != 0)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
        transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);
        if (Input.GetKey(KeyCode.Space)&&isGround)
        {
            
            rb.AddForce(Vector3.up * jumpSpeed);
        }
        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
    public void PushMovement()
    {
       
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
         if(verticalInput>0)
         {
          speed=3;
          isRunning=true;
         }
         if(verticalInput<0)
         {
          speed=1;
          isRunning=true;
         }
         if(verticalInput==0)
         {
          isRunning=false;
         }
        
        transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime);
    }
    public void MoveOther()
    {
        othercont = true;
    }
   }
         
           
                    

            
        