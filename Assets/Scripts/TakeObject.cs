using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeObject : MonoBehaviour
{
    public GameObject hands;
    public bool isHolding;
    Rigidbody rb;
    BoxCollider coll;
    public bool isTrigger = false;
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        coll = gameObject.GetComponent<BoxCollider>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("TakeObjects"))
        {
            isTrigger = true;
            if (Input.GetKey(KeyCode.E))
            {
                isHolding = true;
                
            }
          
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        isTrigger = false;
            
    }
    private void Update()
    {
        if (isHolding)
        {
            this.transform.position = hands.transform.position;
            coll.enabled = false;
            rb.isKinematic = true;
            
        }
        else
        {
            coll.enabled = true;
            rb.isKinematic = false;
        }
          if (Input.GetKey(KeyCode.Q)&&isHolding)
            {
                isHolding = false;
            }
    }
}
