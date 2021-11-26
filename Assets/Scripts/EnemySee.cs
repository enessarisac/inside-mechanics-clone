using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySee : MonoBehaviour
{
    public bool LeftRightZ;
    public float EyeScanZ, ViewDistance;
    void Update()
    {
        if (LeftRightZ)
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
            if (EyeScanZ > -40)
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
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

        }
    }
}
