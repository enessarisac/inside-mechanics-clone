using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public bool isMine;
    
    private void Start()
    {
        isMine = true;
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("TakeObjects"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                isMine = false;
            }
        }
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Q) && !isMine)
        {
            isMine = true;
        }
    }
}
