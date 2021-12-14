using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint: MonoBehaviour
{
   
    CheckPointController checkPointController;
    public GameObject gameManager;
    private void Start() 
    {
        checkPointController=gameManager.GetComponent<CheckPointController>();
    }
    private void OnTriggerEnter(Collider other)
     {
        checkPointController.SaveGame();
    }
}
