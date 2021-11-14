using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEventSO : ScriptableObject
{
    private List<GameEventListener> listeners = new List<GameEventListener>();

    public void Raise()
    {
        for (int i = listeners.Count-1; i > 0; i--)
        {
            listeners[i].OnEventRaised();
        }
    }

    public void Register(GameEventListener listener)
    {
        listeners.Add(listener);
    }

    public void Unregister(GameEventListener listener)
    {
        listeners.Remove(listener);
    }
}
