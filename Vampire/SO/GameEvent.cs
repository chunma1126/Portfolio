using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/GameEvent")]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> _listener = new List<GameEventListener>();

    public void RegisterListener(GameEventListener listener)
    {
        _listener.Add(listener);
    }

    public void UnRegisterListener(GameEventListener listener)
    {
        _listener.Remove(listener);
    }

    public void Raise()
    {
        for (int i = _listener.Count - 1; i >= 0; i--)
        {
            _listener[i].OnEventRaised();
        }
    }
}