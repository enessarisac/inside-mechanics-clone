using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public GameObject wall;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Other"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                Destroy(wall);
            }
        }
    }
}
