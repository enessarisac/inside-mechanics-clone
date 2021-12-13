using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShowFps : MonoBehaviour
{
       public Text fpsText;
     public float deltaTime;
  private void Awake() {
      DontDestroyOnLoad(this);
  }
     void Update () {
         deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
         float fps = 1.0f / deltaTime;
         fpsText.text = Mathf.Ceil (fps).ToString ();
     }
}
