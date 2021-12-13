using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public bool isPaused=false;
    public void Update()
    {
        StartStop();
    }
    public void StartStop()
    {
    if(!isPaused)
        {
            if(Input.GetKey(KeyCode.U))
            {
                Time.timeScale=0;
                 isPaused=true;
            }
           
        }
        if(isPaused)
        {
            if (Input.GetKey(KeyCode.P))
            {
                Time.timeScale=1;
                isPaused=false;
            }
            
        }
    }
}
