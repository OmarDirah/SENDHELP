using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockGoalUpdate : MonoBehaviour
{

    public int GoalsLeft = 2;

    public void DecrementGoal()
    {
        GoalsLeft--;
        
        if(GoalsLeft < 0)
        {
            GoalsLeft = 0;
        }
    }
}
