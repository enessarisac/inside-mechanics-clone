using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public bool isPaused=false;
    public GameObject canvas;
    public void Update()
    {
        if(!isPaused)
        StopGame();
        else
        StartGame();
    }
    public void StartGame()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale=1;
            isPaused=false;
            canvas.SetActive(false);
        }
    }
    public void StopGame()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale=0;
            isPaused=true;
             canvas.SetActive(true);
        }
    }
    public void StartGameWithButton()
    {
        Time.timeScale=1;
        isPaused=false;
         canvas.SetActive(false);
    }
    
    
}
