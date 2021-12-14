using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public GameObject playButton,optionsButton,controlsButton,backButton,controlText,loadGameButton;

    
    public void  Start() 
    {
        if(PlayerPrefs.HasKey("X")==false)
       {
           loadGameButton.SetActive(false);
       }
    }
   public void GameStart()
   {
       PlayerPrefs.DeleteAll();
       SceneManager.LoadScene("Level1");
   }
   public void Options()
   {
       loadGameButton.SetActive(false);
       controlsButton.SetActive(true);
       playButton.SetActive(false);
       optionsButton.SetActive(false);
       backButton.SetActive(true);
        //SceneManager.LoadScene("");
   }
   public void BackButton()
   { 
       controlsButton.SetActive(false);
       playButton.SetActive(true);
       optionsButton.SetActive(true);
       backButton.SetActive(false);
       controlText.SetActive(false);

   }
   public void Controls()
   {
       controlsButton.SetActive(false);
       controlText.SetActive(true);

   }
   public void NextLevel()
   {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);

   }
   public void LoadGame()
   {
       SceneManager.LoadScene("Level1");
   }
   public void RestartLevel()
   {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   }
   public void Quit()
   {
       Application.Quit();
   }
   public void MainMenu()
   {
     

       SceneManager.LoadScene("MainMenu");
   }
}
