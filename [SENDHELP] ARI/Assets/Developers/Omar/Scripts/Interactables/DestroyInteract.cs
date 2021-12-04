using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInteract : Interactable
{
    //references
    public AudioSource source;
    public AudioClip clip;
    public string objectTag;
    // Start is called before the first frame update
    void Start()
    {
        //Disable object at start
        this.enabled = false;

    }

    // Destroy object by overriding Interact method
    public override void Interact()
    {
        // Play sound on interact
        source.PlayOneShot(clip, 7f);
        // Place tag of object
        Destroy(GameObject.FindWithTag(objectTag));
        //Destroy(this.gameObject);
        Debug.Log("Destroy " + objectTag);
    }
}
