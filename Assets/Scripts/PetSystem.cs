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
    public bool inObj;
    public GameObject selectedObj;

    float coroutineTimer=2f;
    void Start()
    {
        StartCoroutine(PetLocation());
        StartCoroutine(GoIntoObjects());
        StartCoroutine(offsetValue());
    }
    IEnumerator PetLocation()
    {
        while (true)
        {
            if (!inObj)
            {
                FollowPlayer();
            }
            yield return new WaitForSeconds(0);

        }
    }
    IEnumerator GoIntoObjects()
    {
        while (true)
        {
            if (inObj)
            {
                transform.position = Vector3.SmoothDamp(transform.position, selectedObj.transform.position, ref velocity, smoothTime);
            }
            if (this.transform.position == selectedObj.transform.position)
            {
                yield return new WaitForSeconds(1);
            }
            else
            {
                yield return new WaitForSeconds(0.001f);
            }
        }
    }
    IEnumerator offsetValue()
    {
        while (true)
        {
            offset.x = Random.Range(minOffset.x, maxOffset.x);
            offset.y = Random.Range(minOffset.y, maxOffset.y);
            offset.z = Random.Range(minOffset.z, maxOffset.z);
            yield return new WaitForSeconds(2f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            inObj =!inObj;
        }
        if (inObj && Input.GetKeyDown(KeyCode.T))
        {
            selectedObj.GetComponent<Light>().enabled = !selectedObj.GetComponent<Light>().enabled;
        }
        

    }
    void FollowPlayer()
    {
        if (!inObj)
            transform.position = Vector3.SmoothDamp(transform.position, player.transform.position+offset, ref velocity, smoothTime);
        
    }
    void ControllingPet()
    {

    }
    
}
