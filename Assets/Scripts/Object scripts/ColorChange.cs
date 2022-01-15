using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    public PetSystem petSystemscript;
    Collider[] objchecksarray;
    int i;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        i = petSystemscript.i;
        objchecksarray = petSystemscript.objChecks;
        if (objchecksarray.Length != 0)
        {
            if (objchecksarray[i].gameObject == this.gameObject)
                this.GetComponent<Renderer>().material.color = Color.red;
            else
                this.GetComponent<Renderer>().material.color = Color.white;
        }
        else
            this.GetComponent<Renderer>().material.color = Color.white;
        //for (int a = 0; a < objchecksarray.Length; a++)
        //{
        //    if (objchecksarray[a].gameObject == this.gameObject)
        //    {
        //        this.GetComponent<Renderer>().material.color = Color.red;
        //        thisObjSelected = true;
        //    }
        //}
        //if (!thisObjSelected)
        //    this.GetComponent<Renderer>().material.color = Color.white;
    }
   
}
