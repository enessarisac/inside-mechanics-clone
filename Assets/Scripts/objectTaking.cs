using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectTaking : MonoBehaviour
{
    public GameObject rightHand;
    public GameObject character;
    public GameObject throwingangle;
    Vector3 handPosition;
    public Vector3 ta;
    public Vector3 hitLocation;
    public float throwforce;
    PlayerController playerController;
    public bool canPick;
    public bool holding;
    public bool charIsHolding;
    public bool objThrown;


    // Start is called before the first frame update
    void Start()
    {
        handPosition = rightHand.GetComponent<Transform>().position;
        playerController = character.GetComponent<PlayerController>();
        charIsHolding = playerController.isHolding;

    }

    // Update is called once per frame
    void Update()
    {
        hold();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Ground") && objThrown == true)
        {
            hitLocation = this.GetComponent<Transform>().position;
        }
        if (other.gameObject.CompareTag("Player"))
        {
            
            pickUp();
            drop();
            
        }
    }
    
    //Eðer canPick doðruysa ve oyuncu E'ye basarsa 
    public void pickUp()
    {
        if (Input.GetKeyDown(KeyCode.E) &&holding == false)
        {

            holding = true;
            charIsHolding = true;
            objThrown = false;
            this.GetComponent<Rigidbody>().isKinematic = true;
            this.GetComponent<BoxCollider>().enabled = false;
        }
    }
    //eðer oyuncu eli doluyken Q'ya basarsa objeyi yere býrakma
    public void drop()
    {
        if (Input.GetKeyDown(KeyCode.Q) && holding == true)
        {
            holding = false;
            charIsHolding = false;
            this.GetComponent<Rigidbody>().isKinematic = false;
            this.GetComponent<BoxCollider>().enabled = true;
            objThrown = false;
        }
    }
    public void throwObj()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ta = throwingangle.GetComponent<Transform>().position - handPosition;

            this.GetComponent<Rigidbody>().isKinematic = false;
            this.GetComponent<Rigidbody>().AddForce(ta * throwforce);
            this.GetComponent<BoxCollider>().enabled = true;
            holding = false;
            charIsHolding = false;
            objThrown = true;
        }
    }
    public void hold()
    {
        if (holding == true)
        {
            handPosition = rightHand.GetComponent<Transform>().position;
            this.GetComponent<Transform>().position = handPosition;
            throwObj();
        }
    }
}
