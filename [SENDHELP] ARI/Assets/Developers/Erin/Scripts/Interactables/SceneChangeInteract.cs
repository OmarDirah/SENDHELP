// [NAME] Erin Alvarico
// [Scene Change Interact] Override Method to Interact with a Prop that will Transition to a Unity Scene

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeInteract : Interactable
{
    // VARIABLES
    public string SceneToLoad;
    public int delayTime;
    public int levelDone;
    public Animator anim;
    public AudioSource source;
    public AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        //Disable object at start
        this.enabled = false;
      
    }

    // Bool will initiate fade sequence and call delayed scene change
    public override void Interact()
    {
        ;
        // Immediate Change to Scene Specified by Name - Will change to accommodate for loading screens
        LevelSelection.levelListDone.Add(levelDone);
        source.PlayOneShot(clip, 7f);
        anim.SetBool("MinigameWon", true);
        Invoke("DelayedAction", delayTime);
    }

    void DelayedAction()
    {
        Debug.Log("Waiting for " + delayTime + " Seconds till next task.");
        SceneManager.LoadScene(SceneToLoad);
    }
}
