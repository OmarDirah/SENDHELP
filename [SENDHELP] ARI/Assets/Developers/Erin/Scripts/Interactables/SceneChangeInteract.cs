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
    public int levelDone;

    // Start is called before the first frame update
    void Start()
    {
        //Disable object at start
        this.enabled = false;
    }

    // Destroy object by overriding Interact method
    public override void Interact()
    {
        // Immediate Change to Scene Specified by Name - Will change to accommodate for loading screens
        LevelSelection.levelListDone.Add(levelDone);
        SceneManager.LoadScene(SceneToLoad);
    }
}
