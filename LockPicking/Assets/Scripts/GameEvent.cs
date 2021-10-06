using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{

    List<EventListener> _eventListeners = new List<EventListener>();

    public void Raise()
    {
        for (int i = 0; i < _eventListeners.Count; i++)
        {
            _eventListeners[i].OnEventRaised();
        }
    }

    public void Register(EventListener listener)
    {
        if (!_eventListeners.Contains(listener))
        {
            _eventListeners.Add(listener);
        }
    }

    public void Deregister(EventListener listener)
    {
        if (_eventListeners.Contains(listener))
        {
            _eventListeners.Remove(listener);
        }   
    }

}
