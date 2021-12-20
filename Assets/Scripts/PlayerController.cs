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
    public bool othercont = false;
    public bool isRunning;
    public bool sprint;
    Vector3 trans = new Vector3();
    TakeObject takeObjects;
    LevelController levelController;
    public GameObject takingObj, gameManager;
    public float runTimer;
    BoxCollider coll;
    public float gravity;
    public bool isCrouching = false;
    private void Start()
    {
        takeObjects = takingObj.GetComponent<TakeObject>();
        anim = gameObject.GetComponent<Animator>();
        moveObject = controll.GetComponent<MoveObject>();
        rb = gameObject.GetComponent<Rigidbody>();
        coll = gameObject.GetComponent<BoxCollider>();
        gravity = 0;
        runTimer = 0;
        levelController = gameManager.GetComponent<LevelController>();
    }
    public void Update()
    {
        //tırmanmayı kontrol ediyor
        if (isWorking == true)
        {
            Climb();
        }
        //hareket edicek inputlar karaktere ait ise çalışacak
        if (moveObject.isMine == true && takeObjects.isHolding == false && hang == false)
        {
            Movement();

            othercont = false;
        }
        //hareket inputlar karakterin değilse çalışacak
        if (moveObject.isMine == false)
        {
            MoveOther();
            isRunning = false;
        }
        //karakterin yerde ve koştuğunu kontrol ediyoruz 
        if (isRunning && isGround)
        {
            //animasyon ve hız ayarlanıyor
            anim.SetBool("isRunning", true);
            runTimer += 1 * Time.deltaTime;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                sprint = true;
                if (sprint == true)
                    energy();
                {
                    speed = 6;

                    anim.SetBool("Sprint", true);

                    if (Input.GetKeyDown(KeyCode.LeftControl))
                    {
                        anim.SetTrigger("Slide");
                    }
                }

            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                sprint = false;
            }
            //Hareket ederken ve koşmazken çömelme
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

        if (!isRunning)
        {

            anim.SetBool("isRunning", false);
            sprint = false;
            //Hareket etmezken crouch animasyonu
            if (Input.GetKey(KeyCode.LeftControl))
            {
                stillCrouch();
            }
            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                //anim.SetTrigger("Dwarf Idle");
            }
        }
        //Koşmaz ve çömelmezken hızı normale çekiyor
        if (!sprint && !isCrouching)
        {
            anim.SetBool("Sprint", false);
            speed = 3;
        }

        if (takeObjects.isHolding == true)
        {
            PushMovement();

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
        if (!isGround)
        {
            gravity += 1 * Time.deltaTime;
        }
        else if (isGround && gravity < 1)
        {
            gravity = 0;
        }
        if (gravity >= 1)
        {
            if (isGround)
            {
                fall();
            }
        }
        if (timer > 10)
        {
            //fall animation plays
        }
        // Crouchta mı değil mi diye kontrol ediyor
        if (isCrouching == true)
        {

        }
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
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        //yatay ve dikey girdileri değişkene atıyoruz
        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();
        //kullanıcının hareket ettiğini öğreniyoruz
        if (horizontalInput != 0 || verticalInput != 0 && hang == false)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
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

        isRunning = false;
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
    }
}






