using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameData GameData;
    public GameEvent OnWinEvent;

    void Start()
    {
        GameData.ResetLevel();
    }

    void Update()
    {
        CheckGoalsLeft();
    }

    void CheckGoalsLeft()
    {
        if (GameData.GoalsLeft < 0)
        {
            OnWinEvent.Raise();
        }
    }
}
