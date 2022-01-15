using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public GameObject looker,boss,girlBoss; 
    public AudioSource girl,me,looky;
    EnemySee enemySee;
    BossTrigger bossTrigger;
    
    private void Start() 
    {
        enemySee=looker.GetComponent<EnemySee>();
        bossTrigger=boss.GetComponent<BossTrigger>();  
        looky = looker.GetComponent<AudioSource>();
        girl= girlBoss.GetComponent<AudioSource>();
        me=GetComponent<AudioSource>();
        me.Play();
        
    }
    private void Update() 
    {
        if(enemySee.see==false && bossTrigger.goBoss==false)
        {
            if(!me.isPlaying)
            me.Play();
            looky.Stop();
            girl.Stop();
        }
        else if(enemySee.see==true)
        {
            if(!looky.isPlaying)
            looky.Play();
            me.Stop();
            girl.Stop();
        }
        else if (bossTrigger.goBoss==true)
        {
            if(!girl.isPlaying)
            girl.Play();
            me.Stop();
            looky.Stop();
        }
        

        

    }
}
