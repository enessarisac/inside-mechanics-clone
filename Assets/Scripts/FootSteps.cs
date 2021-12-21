using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    AudioSource audioSource;
    PlayerController playerController;
    public GameObject player;
    public string test;
    //public AudioClip groundVoice;
    private void Start() 
    {
        audioSource=player.GetComponent<AudioSource>();
        playerController=player.GetComponent<PlayerController>();
    }
    private void OnCollisionStay(Collision other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
             if(playerController.horizontalInput!=0 || playerController.verticalInput!=0 && playerController.hang == false)
        {
            Debug.Log(test);
            //if(!audioSource.isPlaying)
            //{
                //audioSource.clip=groundVoice;
                //audioSource.Play();

            //}
        }
        }
    }
}
