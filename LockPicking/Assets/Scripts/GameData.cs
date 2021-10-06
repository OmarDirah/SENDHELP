using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameData : ScriptableObject
{

    public int GoalsLeft;
    
    public void ResetLevel()
    {
        GoalsLeft = 3;
    }
}
