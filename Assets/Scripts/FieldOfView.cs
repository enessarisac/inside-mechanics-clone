using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0,360)]
    public float angle;

    public Transform player;
    public GameObject cube;
    public GameObject gameManager;
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    public bool canSee;
    
    public float turnSpeed;
    
    LevelController levelController;

    // Start is called before the first frame update
    void Start()
    {
        levelController = gameManager.GetComponent<LevelController>();
        StartCoroutine(FOVroutine());

    }
    private IEnumerator FOVroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while (true)
        {
            FieldOfViewCheck();
            yield return wait;
            
        }
    }
    
    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);
        if (!canSee)
        {
            if (rangeChecks.Length != 0)
            {
                Transform target = rangeChecks[0].transform;
                Vector3 directionToTarget = (target.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
                {
                    float distanceToTarget = Vector3.Distance(transform.position, target.position);
                    if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    {
                        canSee = true;
                    }
                    else
                    {
                        canSee = false;
                    }
                }
                else
                {
                    canSee = false;
                }
            }
            else if (canSee)
            {
                canSee = false;
            }
        }
    }
    public float rotationSpeed;
    public float rotationAngle;
    public bool leftRightZ=false;
    // Update is called once per frame
    void Update()
    {

        if (canSee)
        {
            Catch();
        }
        else
        {
            lookAround(140, 220);    
        }
        

    }    
    public void Busted()
    {
        levelController.RestartLevel();
    }
    public float minDist;
    public float speed;


    public void Catch()
    {

        transform.LookAt(player);
        if (Vector3.Distance(player.position, transform.position) > minDist)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        if(Vector3.Distance(player.position, transform.position) <= minDist)
        {
            Busted();
        }
    }
    public void lookAround(float x,float y)
    {
        transform.localEulerAngles = new Vector3(0, rotationAngle, 0);
        if (leftRightZ)
        {
            if (rotationAngle > x)
            {
                rotationAngle -= 30 * Time.deltaTime;
            }
            else
            {
                leftRightZ = false;
            }
        }
        else
        {
            if (rotationAngle < y)
            {
                rotationAngle += 30 * Time.deltaTime;
            }
            else
            {
                leftRightZ = true;
            }
        }
    }
}
