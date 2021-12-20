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
    public bool isGround;
    public bool othercont = false;
    public bool sprint;
    Vector3 trans = new Vector3();
    LevelController levelController;
    public GameObject gameManager;
    BoxCollider coll;
    public float gravity;
    float horizontalInput,verticalInput;
    public bool isCrouching = false;
    public bool isMoving;
    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();
        coll = gameObject.GetComponent<BoxCollider>();
        gravity = 0;
        levelController = gameManager.GetComponent<LevelController>();
    }
    public void Update()
    {
        anim.SetBool("Sprint", sprint);
        anim.SetBool("isRunning",isMoving);
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical"); 
        if (hang == false && isGround)
        {
            Movement();
            
            othercont = false;
        }
            if (horizontalInput == 0 && verticalInput == 0 || hang == true)
            {
                isMoving=false; 
                speed=3;
                if (Input.GetKey(KeyCode.LeftControl))
            {
                stillCrouch();
            } 
            }   
        //hangi durumda collider ve rigidbody kaldırılacak onu kontrol ediyoruz
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Slide") && slidable == true || hang || isWorking)
        {
            coll.enabled = false;
            rb.isKinematic = true;
        }
        else
        {
            coll.enabled = true;
            rb.isKinematic = false; ;
        }
        //düşüşü kontrol ediyoruz                    
        if (isGround)
        {
            if(gravity<1)
            gravity = 0;  
            else
            fall();
        }
        else
        {
            gravity += 1 * Time.deltaTime;
        }
        // Crouchta mı değil mi diye kontrol ediyor   
    }
    public float timer;
    //enerjiyi ayarlıyoruz
    public void energy()
    {
        if (speed == 6)
        {
            timer += 1 * Time.deltaTime;
        }
        else if (speed != 6 && timer > 0)
        {
            timer -= 2 * Time.deltaTime;
        }
        if (timer > 10)
        {
            //fall
        }
    }
    //tırmanma halinde transformu ayarlıyoruz
    public void Climb()
    {
        transform.position = Vector3.SmoothDamp(transform.position, climbPos.transform.position, ref velocity, smoothTime);
    }
    public void fall()
    {
        levelController.RestartLevel();
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
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;
        }
    }
    //hareket bölümü
    public void Movement()
    {
        anim.SetBool("Push", false);
        anim.SetBool("Pull", false);
        
        //yatay ve dikey girdileri değişkene atıyoruz
        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();
        //kullanıcının hareket ettiğini öğreniyoruz
        if (horizontalInput != 0 || verticalInput != 0 && hang == false)
        {   
            isMoving=true;
            anim.SetBool("isRunning", true);
            if(isMoving)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
               sprint = true;
                if (sprint == true)
                energy();
                         {
                             speed = 6;

                             anim.SetBool("Sprint",true);
                             if(Input.GetKeyDown(KeyCode.LeftControl))
                             {
                                 anim.SetTrigger("Slide");
                             }      
                         }
                }
                else
                {
                 sprint=false;
                }
            }          
            if (!sprint)
            {
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    isCrouching = true;
                    movingCrouch();
                }
                if (Input.GetKeyUp(KeyCode.LeftControl))
                {
                    isCrouching = false;
                    speed = 3;
                    //anim.SetTrigger("Walking"); yeniden yürümeye döndürme
                }
            }             
        }
        //girilen girdiye göre karakteri hareket ettiriyoruz
        transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);
        //girilen girdi 0a eşit değilse hareket girdisine göre karakterin bakacağı yönü seçiyoruz
        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

    }
    public bool hang;
    public float smoothTime = 0.1f;
    //karakterimiz bir şeyi itiyor ya da çekiyorsa bu bölüm çalışıyor
    public void PushMovement()
    {

        
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (verticalInput > 0 && !anim.GetCurrentAnimatorStateInfo(0).IsName("Push"))
        {
            speed = 3;
            anim.SetBool("Push", true);
            anim.SetBool("Pull", false);

        }
        if (verticalInput < 0 && !anim.GetCurrentAnimatorStateInfo(0).IsName("Pull"))
        {
            speed = 1;
            anim.SetBool("Push", false);
            anim.SetBool("Pull", true);
        }
        if (verticalInput == 0)
        {
            anim.SetBool("Push", false);
            anim.SetBool("Pull", false);
        }

        transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime);
    }
    public void JumpToWall()
    {
        Climb();
        isWorking = true;
    }
    public void toTheIdle()
    {
        isWorking = false;
    }
    public bool isWorking;
    private Vector3 velocity = Vector3.zero;
    public bool trig;
    public bool slidable;
    public bool climb;
    private void OnTriggerStay(Collider other)
    {
        //sonraki levele geçiş kısmı
        if (other.gameObject.CompareTag("Finish"))
        {
            levelController.NextLevel();
        }
        //normalin aksine kayılabilir alanlarda kaydığımızda gerçekleşen bölüm
        if (other.gameObject.CompareTag("Slidable"))
        {
            slidable = true;
        }
        //tırmnılabilecek böllgelerde çalışacak
        if (other.gameObject.CompareTag("Ledge"))
        {
            trig = true;
            //gerekli pozisyonları alıyoruz
            Vector3 ledPos = GameObject.Find("Ledge").transform.position;
            Vector3 pos = new Vector3(transform.position.x, ledPos.y + 0.7f, ledPos.z - 2.2f);
            trans = pos;
            climbPos.transform.position = trans;
            //tırmanılacak pozisyonu hesaplanan pozisyona eşitliyoruz

            if (Input.GetKey(KeyCode.Space))
            {

                hang = true;

                anim.SetBool("ToLedge", true);

            }
            if (Input.GetKey(KeyCode.Q) && hang)
            {

                anim.SetTrigger("Drop");
                anim.SetBool("ToLedge", false);
                hang = false;
            }
            if (Input.GetMouseButtonDown(0) && hang)
            {


                anim.SetBool("ToLedge", false);
                hang = false;
                anim.SetTrigger("Climb");
                climb = true;
                if (Input.GetMouseButtonUp(0))
                {
                    climb = false;

                }
            }


        }
    }
    private void OnTriggerExit(Collider other)
    {
        trig = false;
        hang = false;
        climb = false;
        slidable = false;
    }
    //eğer kontrol bizde değilse çalışacak bölüm
    public void MoveOther()
    {
        othercont = true;
    }
    //Hareket ederken Crouch yapmak
    public void movingCrouch()
    {
        speed = 1.5f;

        //anim.SetTrigger("walking Crouch"); //Crouch animasyonu 

    }
    //Yerinde dururken crouch yapmak
    public void stillCrouch()
    {
        //anim.SetTrigger("still Crouch"); //Still Crouch animasyonu
        Debug.Log("Crouch");
    }
}






