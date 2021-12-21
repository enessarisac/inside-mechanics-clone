using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectTaking : MonoBehaviour
{
    public GameObject rightHand;
    public GameObject character;
    public GameObject throwingangle;
    Vector3 handPosition;
    Vector3 ta;
    Vector3 a;
    public float throwforce;
    PlayerController playerController;
    public bool canPick;
    public bool holding;

    
    // Start is called before the first frame update
    void Start()
    {
        handPosition = rightHand.GetComponent<Transform>().position;
        playerController = character.GetComponent<PlayerController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //Eðer canPick doðruysa ve oyuncu E'ye basarsa 
        
        
        if (Input.GetKeyDown(KeyCode.E) && canPick == true&&holding==false){
            
            holding = true;
            this.GetComponent<Rigidbody>().isKinematic = true;
            this.GetComponent<BoxCollider>().enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            holding = false;
            this.GetComponent<Rigidbody>().isKinematic = false;
            this.GetComponent<BoxCollider>().enabled = true;
        }
        if (holding == true)
        {
            handPosition = rightHand.GetComponent<Transform>().position;
            this.GetComponent<Transform>().position = handPosition;
            if (Input.GetMouseButtonDown(0))
            {
                ta = throwingangle.GetComponent<Transform>().position-handPosition;
                
                this.GetComponent<Rigidbody>().isKinematic = false;
                this.GetComponent<Rigidbody>().AddForce( ta * throwforce);
                this.GetComponent<BoxCollider>().enabled = true;
                holding = false;
        }
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canPick = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canPick = false;
        }
    }
}
