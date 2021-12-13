using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    public Transform player;
    public float MaxDist;
    public float MinDist;
    BossTrigger bossTrigger;
    public GameObject bossTrig;
    public float speed;
    LevelController levelController;
  
    public void Start ()
    {
        levelController = GameObject.FindWithTag("LevelManager").GetComponent<LevelController>();
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
        if(Vector3.Distance(transform.position,player.position) >= MinDist)
        {
          transform.position += transform.forward*speed*Time.deltaTime;
        if(Vector3.Distance(transform.position,player.position) <= MinDist)
              {
                 Busted();
              }     
        }   
    }
    public void Busted()
    {
        levelController.RestartLevel();
    }
}


    


