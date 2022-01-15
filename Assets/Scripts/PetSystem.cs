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
    public int i=0;
    Collider[] objChecks;
    void Start()
    {

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
            if (objChecks.Length != 0)
            {
                
                if (inObj)
                {
                    if (this.transform.position == objChecks[i].gameObject.transform.position)
                    {
                        yield return new WaitForSeconds(1);
                    }
                    transform.position = Vector3.SmoothDamp(transform.position, objChecks[i].gameObject.transform.position, ref velocity, smoothTime);
                }
                else
                {
                    yield return new WaitForSeconds(0.001f);
                }
            }
            yield return new WaitForSeconds(0.5f);
            
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
            Collider[] objChecks = Physics.OverlapSphere(transform.position, radius, targetMask);   
            if (objChecks.Length != 0)
            {
                
                Debug.Log(objChecks.Length);
            }
            yield return new WaitForSeconds(0.001f);
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
        if (Input.GetKeyDown(KeyCode.R))
        {
            inObj = !inObj;
        }
        if (inObj && Input.GetKeyDown(KeyCode.T))
        {
            objChecks[i].gameObject.GetComponent<Light>().enabled = !objChecks[i].gameObject.GetComponent<Light>().enabled;
        }
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
        if (i - 1 >= 0)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                i -= 1;
            }
        }
    }

    
}
