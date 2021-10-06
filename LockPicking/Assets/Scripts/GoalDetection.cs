using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalDetection : MonoBehaviour
{
    GameObject _currGoal;
    bool _isRunning = false;

    public GameEvent GoalMissedEvent;
    public GameEvent GoalHitEvent;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!_isRunning)
            {
                _isRunning = true;
            }

            if (_currGoal != null)
            {
                Destroy(_currGoal);
                GoalHitEvent.Raise();
            }
            else
            {
                GoalMissedEvent.Raise();
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
