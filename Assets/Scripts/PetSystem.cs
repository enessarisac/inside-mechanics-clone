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
    public float radius;
    public LayerMask targetMask;
    public int i;
    public Collider[] objChecks;
    GameObject selectedObject;
    
    void Start()
    {
        i = 0;
        
        StartCoroutine(PetLocation());
        StartCoroutine(GoIntoObjects());
        StartCoroutine(offsetValue());
        StartCoroutine(objCheck());
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
                    selectedObject = objChecks[i].gameObject;
                    if (this.transform.position == selectedObject.transform.position)
                    {
                        yield return new WaitForSeconds(1);
                    }
                    
                    transform.position = Vector3.SmoothDamp(transform.position, selectedObject.transform.position, ref velocity, smoothTime);
                    yield return new WaitForSeconds(0.001f);
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
    IEnumerator objCheck()
    {
        while (true)
        {
            objChecks = Physics.OverlapSphere(transform.position, radius, targetMask);   
            
            yield return new WaitForSeconds(0.01f);
        }

    }
    // Update is called once per frame
    void Update()
    {
        ControllingPet();
    }
    void FollowPlayer()
    {
        if (!inObj)
            transform.position = Vector3.SmoothDamp(transform.position, player.transform.position+offset, ref velocity, smoothTime);
        
    }

    
    void ControllingPet()
    {
        if (Input.GetKeyDown(KeyCode.R) && objChecks.Length != 0)
        {
            inObj = !inObj;
        }
        if (inObj && Input.GetKeyDown(KeyCode.T))
        {
            selectedObject = objChecks[i].gameObject;
            selectedObject.GetComponent<Light>().enabled = !objChecks[i].gameObject.GetComponent<Light>().enabled;
        }
        if (!inObj) 
        {
            if (i >= objChecks.Length)
            {
                i = 0;
            }
            if (i + 1 < objChecks.Length)
            {
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    i += 1;
                }
            }
            if (i - 1 >= 0 )
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    i -= 1;
                }
            }
        }
    }

    
}
