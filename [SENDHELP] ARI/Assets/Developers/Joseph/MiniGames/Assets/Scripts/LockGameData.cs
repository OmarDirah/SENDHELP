using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LockGameData : ScriptableObject
{

    public int GoalsLeft;
    public bool isRunning = false;

    public void ResetLevel()
    {
        GoalsLeft = 3;
        isRunning = false;
    }
}
