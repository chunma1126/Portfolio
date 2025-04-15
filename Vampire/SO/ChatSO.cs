using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public enum Character
{
    Player,
    Heroine,
    Null,
}

[System.Serializable]
public struct Description
{
    public Character character;
    public Sprite cg;
    [TextArea] public string description;

    public UnityEvent onInteractions;
}

[CreateAssetMenu(menuName = "SO/Chat", fileName = "Chat_")]
public class ChatSO : ScriptableObject
{
    public string title;
    public int episode;
    
    public List<Description> list;
}