using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessGranted : Interactable
{
    // VARIABLES
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        //Disable object at start
        this.enabled = false;
    }

    // trigger bool to open door animation
    public override void Interact()
    {
        anim.SetBool("hasAccessKey", true);
    }
}
