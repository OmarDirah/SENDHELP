// [NAME] Erin Alvarico
// [UI Behavior] List of methods/functiosn to use involving UI elements and how it interacts with the game world

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIBehavior : MonoBehaviour
{

    // VARIABLES
    public int delayTime;
    public string ScreenName;

    public void OnClickSwitchScene()
    {
        Invoke("DelayedAction", delayTime);
    }

    void DelayedAction()
    {
        Debug.Log("Waiting for " + delayTime + " Seconds till next task.");
        SceneManager.LoadScene(ScreenName);
    }

    public void OnClickExitGame()
    {
        Application.Quit();
    }

}
