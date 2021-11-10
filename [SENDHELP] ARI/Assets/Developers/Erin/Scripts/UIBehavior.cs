// [NAME] Erin Alvarico
// [UI Behavior] List of methods/functiosn to use involving UI elements and how it interacts with the game world

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIBehavior : MonoBehaviour
{

    // VARIABLES
    //bool buttonSelect = false;


    public void OnClickSwitchScene(string ScreenName)
    {
        SceneManager.LoadScene(ScreenName);
    }

    
    /*public void OnClickChangeView(GameObject camera)
    {
        // Run CameraController Update Method to change to appropriate view
        CameraController.Update();
    }*/
    

    public void OnClickExitGame()
    {
        Application.Quit();
    }

}
