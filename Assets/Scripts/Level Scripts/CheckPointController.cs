using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    public GameObject player;
  public GameObject[] checkPointLocation;
  public int checkIndex;

  

public void Start() 
{
    if(PlayerPrefs.HasKey("X"))
    {
        LoadGame();
    }
    
    
}
public void SaveGame()
{
    PlayerPrefs.DeleteAll();
    PlayerPrefs.SetFloat("X",player.transform.position.x);
    PlayerPrefs.SetFloat("Y",player.transform.position.y);
    PlayerPrefs.SetFloat("Z",player.transform.position.z);
}
public void LoadGame()
{
    float X=PlayerPrefs.GetFloat("X");
    float Y=PlayerPrefs.GetFloat("Y");
    float Z=PlayerPrefs.GetFloat("Z");
    player.transform.position=new Vector3 (X,Y,Z);
}
public void Delete()
{

}
 private void Update() {
     for(int i = 0 ; i<checkPointLocation.Length; i++)
     {
        
     }
 }
}
