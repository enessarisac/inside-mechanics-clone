using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    public Transform player;
    Enemy enemy;
    public float MaxDist;
    public float MinDist;
    BossTrigger bossTrigger;
    public GameObject bossTrig;
    public float speed;
  
    public void Start ()
    {
        bossTrigger=bossTrig.GetComponent<BossTrigger>();
        
    }
    public void Update()
    {
       if(bossTrigger.goBoss==true){
           Chase();
       }
       
    }
    public void Chase()
    {
        transform.LookAt(player);
             if(Vector3.Distance(transform.position,player.position) >= MinDist){
     
          transform.position += transform.forward*speed*Time.deltaTime;
         


          if(Vector3.Distance(transform.position,player.position) <= MinDist)
              {
                 Debug.Log("yakalandın");
    }     
    }   
    }
}


    


