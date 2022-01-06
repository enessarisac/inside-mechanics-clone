using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetSystem : MonoBehaviour
{
    public Vector3 offset;
    public Transform player;
    public Vector3 maxOffset,minOffset;
    Vector3 velocity;
    public float smoothTime;

    float coroutineTimer=2f;
    void Start()
    {
        
    }
    IEnumerator PetLocation()
    {
        coroutineTimer=0;
        offset.x=Random.Range(minOffset.x,maxOffset.x);
        offset.y=Random.Range(minOffset.y,maxOffset.y);
        offset.z=Random.Range(minOffset.z,maxOffset.z);
        yield return new WaitForSeconds(2f);
            
        
    }
    // Update is called once per frame
    void Update()
    {
        if(coroutineTimer>1.9f){
           StartCoroutine(PetLocation()); 
         
        }
        else
        {
            coroutineTimer+=1*Time.deltaTime;
        }
        
        FollowPlayer();
    }
    void FollowPlayer()
    {

        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position+offset, ref velocity, smoothTime);
        
    }
}
