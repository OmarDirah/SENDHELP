using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyControler : Interactable
{
    public GameObject terminal;
    //bool keyGrabbed;
    bool key;
    // Start is called before the first frame update
    void Awake()
    {
        terminal.GetComponent<SceneChangeInteract>().enabled = false;
        key = false;


    }

    private void Update()

    {
        if (key) 
        {
            terminal.GetComponent<SceneChangeInteract>().enabled = true;
        }
        
    }

    public override void Interact()
    {
        key = true;
        terminal.GetComponent<SceneChangeInteract>().enabled = true;

    }
}
