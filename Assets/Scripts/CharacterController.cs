using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    ClimbPosition climbPosition;
    public GameObject climbPos;
    Animator anim;
    Rigidbody rb;
    MoveObject moveObject;
    public GameObject controll;
    public bool isGround;
    public bool othercont=false;
    public bool isRunning;
    public bool sprint;
    TakeObject takeObjects;
    public GameObject takingObj;
    BoxCollider coll;
    private void Start()
    {
        climbPosition=climbPos.GetComponent<ClimbPosition>();
        takeObjects=takingObj.GetComponent<TakeObject>();
        anim = gameObject.GetComponent<Animator>();
        moveObject = controll.GetComponent<MoveObject>();
        rb = gameObject.GetComponent<Rigidbody>();
        coll=gameObject.GetComponent<BoxCollider>();
    }
    public void Update()
    {
        if (moveObject.isMine==true&&takeObjects.isHolding==false && hang==false)
        {
            Movement();
            
            othercont = false;
        }
        if(moveObject.isMine==false)
        {
            MoveOther();
            isRunning=false;
        }
        if (isRunning&&isGround)
        {
            anim.SetBool("isRunning", true);
        }
        if(!isRunning)
        {
            anim.SetBool("isRunning", false);
        }
        if (isRunning)
        {
            
             if (Input.GetKey(KeyCode.LeftShift))
                {
               sprint = true;
                if (sprint == true)
                         {
                             speed = 6;
                             
                             anim.SetBool("Sprint",true);
                             if(Input.GetKeyDown(KeyCode.LeftControl))
                             {
                                 anim.SetTrigger("Slide");
                             }
                             
                         }
                
                }
                if(Input.GetKeyUp(KeyCode.LeftShift))
                {
                    sprint=false;
                }    
        }
        else if (!isRunning)
        {
            sprint = false;
        }
        if (sprint == false)
        {
            anim.SetBool("Sprint",false);
            speed = 3;
        }
        if(takeObjects.isHolding==true)
        {
            PushMovement();
 
        }
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Slide")&&slidable==true||hang&& slidable==true)
                             {
                                 
                                 coll.enabled=false;
                                 rb.useGravity=false;
                             }
                             else
                             {
                                 coll.enabled=true;
                                 rb.useGravity=true;
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
        anim.SetBool("Push",false);
        anim.SetBool("Pull",false);
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();
        if (horizontalInput != 0 || verticalInput != 0 && hang==false)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
        transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);
        if (Input.GetKeyDown(KeyCode.Space)&&isGround&&trig==false)
        {

            if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
            {
                
              anim.SetTrigger("Jump");
            }
            
           
        }
        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
    public bool hang;
    public void PushMovement()
    {
        
        isRunning=false;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
         if(verticalInput>0&&!anim.GetCurrentAnimatorStateInfo(0).IsName("Push"))
         {
          speed=3;
          anim.SetBool("Push",true);
          anim.SetBool("Pull",false);
          
         }
         if(verticalInput<0&& !anim.GetCurrentAnimatorStateInfo(0).IsName("Pull"))
         {
          speed=1;
           anim.SetBool("Push",false);
          anim.SetBool("Pull",true);
         }
         if(verticalInput==0)
         {
          anim.SetBool("Push",false);
          anim.SetBool("Pull",false);
         }
        
        transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime);
    }
    public bool trig;
    public bool slidable;
    public bool climb;
    private void OnTriggerStay(Collider other) 
    {
         climbPos.transform.position=GameObject.Find("Ledge").transform.position;
         
        if(other.gameObject.CompareTag("Slidable"))
              {
             slidable=true;
              }  
        if(other.gameObject.CompareTag("Ledge"))
        {
            
            trig=true;
            if(Input.GetKey(KeyCode.Space))
            {
                hang=true;
              
              anim.SetBool("ToLedge",true);

            }
            if(Input.GetKey(KeyCode.Q)&&hang)
              {
                  
                  anim.SetTrigger("Drop");
                  anim.SetBool("ToLedge",false);
                  hang=false;
              }
             if(Input.GetMouseButton(0)&&hang)
              {     
                  anim.SetBool("ToLedge",false);             
                  hang=false;
                  anim.SetTrigger("Climb"); 
                  climb=true;    
                  if(Input.GetMouseButtonUp(0))
                  {
                      climb=false;

                  }    
              }
              
                           
        }
    }
    private void OnTriggerExit(Collider other) {
        trig=false;
        hang=false;
       climb=false;
       slidable=false;
    }
    public void MoveOther()
    {
        othercont = true;
    }
   }
         
           
                    

            
        