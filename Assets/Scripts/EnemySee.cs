using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySee : MonoBehaviour
{
    public bool LeftRightZ;
    public bool see;
    public float EyeScanZ, ViewDistance;
    public GameObject fLight;
    LevelController levelController;
    public GameObject gameManager;
     private void Start() {
         levelController = gameManager.GetComponent<LevelController>();
     }
    void Update()
    {
        if(see)
        {
            See();
        }
        if (LeftRightZ&&!see)
        {
            if (EyeScanZ < 40)
            {
                EyeScanZ += 30 * Time.deltaTime;
            }
            else
            {
                LeftRightZ = false;
            }
        }
        else
        {
            if (EyeScanZ > -40&&!see)
            {
                EyeScanZ -= 30 * Time.deltaTime;
            }
            else
            {
                LeftRightZ = true;
            }
        }
        transform.Find("MEyes").transform.localEulerAngles = new Vector3(0, EyeScanZ);


        RaycastHit hit;
        Debug.DrawRay(transform.Find("MEyes").position, transform.Find("MEyes").transform.forward * ViewDistance);

        if (Physics.Raycast(transform.Find("MEyes").position, transform.Find("MEyes").transform.forward * ViewDistance, out hit, ViewDistance))
        {
            if (hit.transform.gameObject.tag == "Player")
            {
                Debug.Log(gameObject.name + " CAN see Player");
                EyeScanZ=0;
                see=true;
            }

        }
    }
    public float speed=10;
    public Transform player;
    public float minDist;
     public void Busted()
    {
        levelController.RestartLevel();
    }
    public void See()
    {
        transform.LookAt(player);
        if(Vector3.Distance(transform.position,player.position) >= minDist)
        {
          transform.position += transform.forward*speed*Time.deltaTime;
        if(Vector3.Distance(transform.position,player.position) <= minDist)
              {
                 Busted();
              }     
    }
   
    }
}
