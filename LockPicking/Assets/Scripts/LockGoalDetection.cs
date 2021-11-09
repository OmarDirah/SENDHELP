using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockGoalDetection : MonoBehaviour
{
    GameObject _currGoal;

    public LockGameEvent GoalMissedEvent;
    public LockGameEvent GoalHitEvent;
    public LockGameEvent ResetLevelEvent;

    public LockGameData GameData;

    bool waiting = false;
    private float waitTime = 2.0f;
    private float timer = 0.0f;

    void Update()
    {
        if (waiting)
        {
            timer += Time.deltaTime;

            if (timer > waitTime)
            {
                timer = timer - waitTime;
                
                ResetLevelEvent.Raise();
                waiting = false;
            }
        }

        if (Input.GetMouseButtonDown(0) && !waiting)
        {
            if (!GameData.isRunning)
            {
                GameData.isRunning = true;
            }

            else if (_currGoal != null)
            {
                Destroy(_currGoal);
                GameData.GoalsLeft--;
                GoalHitEvent.Raise();

            }
            else
            {
                GoalMissedEvent.Raise();
                waiting = true;
            }
        }


    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("IN GOAL");
        _currGoal = other.gameObject;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("EXIT GOAL");
        _currGoal = null;
    }
}
