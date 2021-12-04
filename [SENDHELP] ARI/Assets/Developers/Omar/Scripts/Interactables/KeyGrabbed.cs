using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyGrabbed : Interactable
{
    public GameObject terminal;
    //bool keyGrabbed;
    
    // Start is called before the first frame update
    void Awake()
    {
        terminal.GetComponent<SceneChangeInteract>().enabled = false;
        


    }

    // Update is called once per frame
   
    public override void Interact()
    {
        terminal.GetComponent<SceneChangeInteract>().enabled = true;

    }

}
    

