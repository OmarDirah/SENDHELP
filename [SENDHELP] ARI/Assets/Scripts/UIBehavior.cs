using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIBehavior : MonoBehaviour
{

    public void OnClickSwitchScene(string ScreenName)
    {
        SceneManager.LoadScene(ScreenName);
    }

    public void OnClickExitGame()
    {
        Application.Quit();
    }

}
