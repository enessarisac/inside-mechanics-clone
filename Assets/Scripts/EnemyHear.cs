using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHear : MonoBehaviour
{
    public Transform player;
    public float hearableFootDist;
    public float catchDist;
    public float hearableBreathDistance;


    private void Start() 
    {
        
    }
    private void Update() {
        if(Vector3.Distance(transform.position,player.position) <= hearableFootDist)
        {
            IsEnemyHear();
            
        }
         if(Vector3.Distance(transform.position,player.position) <= hearableBreathDistance)
        {
            IsEnemyHear();
        }
        
    }
    public void IsEnemyHear()
    {
        transform.LookAt(player);
        
    }
}
