using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// fail safe to keep script off anything but props
//[RequireComponent (typeof(Prop))]
public class Interactable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        this.enabled = false;
    }

    public virtual void Interact() 
    {
        Debug.Log("Interacting with " + name);
    }

   
}
