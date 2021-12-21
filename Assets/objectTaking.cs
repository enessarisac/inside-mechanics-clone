using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectTaking : MonoBehaviour
{
    public GameObject rightHand;
    public GameObject character;
    Vector3 handPosition;
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
        handPosition = rightHand.GetComponent<Transform>().position;
        if (Input.GetKeyDown(KeyCode.E) && canPick == true&&holding==false){
            holding = true;
            this.GetComponent<Rigidbody>().isKinematic = true;
            this.GetComponent<BoxCollider>().enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            holding = false;
        }
        if (holding == true)
        { 
            this.GetComponent<Transform>().position = handPosition;
        }
        if (holding == false)
        {
            this.GetComponent<Rigidbody>().isKinematic = false;
            this.GetComponent<BoxCollider>().enabled = true;
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
