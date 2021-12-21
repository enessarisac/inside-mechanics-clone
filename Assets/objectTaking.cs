using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectTaking : MonoBehaviour
{
    public GameObject rightHand;
    public GameObject character;
    Vector3 handPosition;
    PlayerController playerController;
    public bool canPick;
    public bool holding;

    // Start is called before the first frame update
    void Start()
    {
        handPosition = rightHand.GetComponent<Transform>().position;
        playerController = character.GetComponent<PlayerController>();
        canPick = playerController.canPick;
    }

    // Update is called once per frame
    void Update()
    {
        //Eðer canPick doðruysa ve oyuncu E'ye basarsa 
        canPick = playerController.canPick;
        if (Input.GetKeyDown(KeyCode.E) && canPick == true)
        {
            holding = true;
            this.GetComponent<Rigidbody>().isKinematic = true;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            holding = false;
        }
        if (holding == true)
        {
            this.GetComponent<Transform>().position = handPosition;
        }
        if (holding == false)
        {
            this.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
