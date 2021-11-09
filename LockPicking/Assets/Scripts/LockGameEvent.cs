using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LockGameEvent : ScriptableObject
{

    List<LockEventListener> _eventListeners = new List<LockEventListener>();

    public void Raise()
    {
        for (int i = 0; i < _eventListeners.Count; i++)
        {
            _eventListeners[i].OnEventRaised();
        }
    }

    public void Register(LockEventListener listener)
    {
        if (!_eventListeners.Contains(listener))
        {
            _eventListeners.Add(listener);
        }
    }

    public void Deregister(LockEventListener listener)
    {
        if (_eventListeners.Contains(listener))
        {
            _eventListeners.Remove(listener);
        }   
    }

}
