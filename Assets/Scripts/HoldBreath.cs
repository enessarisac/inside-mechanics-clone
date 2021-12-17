using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldBreath : MonoBehaviour
{
   PlayerController playerController;
   public GameObject player;
   public bool isHoldingBreath;

   public bool canHoldBreath;
   public float lungeCapasity=10;
   public float holdTime;
   private void Start() 
   {
       playerController=player.GetComponent<PlayerController>();
   }
   private void Update() 
   {
       if(Input.GetKeyDown(KeyCode.Space)&&canHoldBreath)
       {
        holdBreath();
       }
       if(Input.GetKeyUp(KeyCode.Space))
       {
           isHoldingBreath=false;
       }
       if(canHoldBreath==false)
       {
           if(holdTime>0)
           holdTime-=2*Time.deltaTime;
           else 
           canHoldBreath=true;
       }
   }
   public void holdBreath()
   {
       isHoldingBreath=true;
       holdTime+=1*Time.deltaTime;

       if(holdTime==lungeCapasity)
       {
           MaxHold();
       }

   }
   public void MaxHold()
   {
       canHoldBreath=false;
   }
}
