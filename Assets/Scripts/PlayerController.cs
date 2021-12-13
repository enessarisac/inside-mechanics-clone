using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public GameObject climbPos;
    Animator anim;
    Rigidbody rb;
    MoveObject moveObject;
    public GameObject controll;
    public bool isGround;
    public bool othercont=false;
    public bool isRunning;
    public bool sprint;
    Vector3 trans=new Vector3();
    TakeObject takeObjects;
    LevelController levelController;
    public GameObject takingObj,gameManager;
    public float runTimer;
    BoxCollider coll;

    public float gravity;
    private void Start()
    {
        takeObjects=takingObj.GetComponent<TakeObject>();
        anim = gameObject.GetComponent<Animator>();
        moveObject = controll.GetComponent<MoveObject>();
        rb = gameObject.GetComponent<Rigidbody>();
        coll=gameObject.GetComponent<BoxCollider>();
        gravity=0;
        runTimer=0;
        levelController=gameManager.GetComponent<LevelController>();
    }
    public void Update()
    {
        if(isWorking==true)
        {
             transform.position=Vector3.SmoothDamp(transform.position,climbPos.transform.position,ref velocity,smoothTime); 
        }
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
            runTimer+=1*Time.deltaTime;
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
        if(!isRunning)
        {
            
            anim.SetBool("isRunning", false);
            sprint = false;
        }
        if (!sprint)
        {
            anim.SetBool("Sprint",false);
            speed = 3;
        }
        if(takeObjects.isHolding==true)
        {
            PushMovement();
 
        }
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Slide")&&slidable==true||hang||isWorking)
                             {
                                 
                                 coll.enabled=false;
                                 rb.isKinematic=true;
                             }
                             else
                             {
                                 coll.enabled=true;
                                 rb.isKinematic=false;;
                             }
        if(!isGround)
        {
            gravity+=1*Time.deltaTime;
        }else if(isGround&&gravity<1)
        {
            gravity=0;
        }
        if(gravity>=1)
        {
            if(isGround)
            {
                levelController.RestartLevel();
            }
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

        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        
    }
    public bool hang;
    public float smoothTime=0.1f;
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
    public void JumpToWall() {
        isWorking=true;                
    }
    public void toTheIdle()
    {
        isWorking=false; 
    }

    public bool isWorking;
    private Vector3 velocity=Vector3.zero;
    public bool trig;
    public bool slidable;
    public bool climb;
    private void OnTriggerStay(Collider other) 
    {
        if(other.gameObject.CompareTag("Finish"))
        {
                levelController.NextLevel();
        }
        if(other.gameObject.CompareTag("Slidable"))
              {
             slidable=true;
              }  
        if(other.gameObject.CompareTag("Ledge"))
        {         
            trig=true;
            Vector3 ledPos = GameObject.Find("Ledge").transform.position;            
            Vector3 pos = new Vector3 (transform.position.x , ledPos.y+0.7f , ledPos.z-2.2f);
            trans = pos;
            climbPos.transform.position=trans;
            
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
             if(Input.GetMouseButtonDown(0)&&hang)
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
         
           
                    

            
        