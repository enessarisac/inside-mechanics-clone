using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public bool goBoss;
    public void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("Player")){
               goBoss=true;
        }
    }
}
