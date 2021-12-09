using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessGranted : Interactable
{
    // VARIABLES
    public AudioSource source;
    public AudioClip clip;
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
        source.PlayOneShot(clip, 7f);
        Debug.Log("Sound Played");

        anim.SetBool("hasAccessKey", true);
    }
}
