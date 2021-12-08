using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : Interactable
{
    // VARIABLES
    public AudioSource source;
    public AudioClip clip;

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
    }
}
