using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LockEventListener : MonoBehaviour
{

    public LockGameEvent Event;
    public UnityEvent Response;

    void OnEnable()
    {
        Event.Register(this);
    }

    void OnDisable()
    {
        Event.Deregister(this);
    }

    public void OnEventRaised()
    {
        Response.Invoke();
    }
}
