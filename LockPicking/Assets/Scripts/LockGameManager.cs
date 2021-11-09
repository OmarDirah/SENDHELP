using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockGameManager : MonoBehaviour
{

    public LockGameData GameData;
    public LockGameEvent OnWinEvent;

    bool isFirstTap = true;

    void Start()
    {
        GameData.ResetLevel();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !GameData.isRunning && isFirstTap)
        {
            //GameData.isRunning = true;
            isFirstTap = false;
        }
        CheckGoalsLeft();
    }

    void CheckGoalsLeft()
    {
        if (GameData.GoalsLeft <= 0)
        {
            OnWinEvent.Raise();
            StopLevel();
        }
    }

    public void LoadLevel()
    {
        GameData.ResetLevel();
    }

    public void StopLevel()
    {
        GameData.isRunning = false;
    }
}
