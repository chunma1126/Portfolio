using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Message
{
    public Character chatType;
    [TextArea] public string description;
}

[CreateAssetMenu(fileName = "DM_", menuName = "SO/DM")]
public class DirectMessageSO : ScriptableObject
{
    public List<Message> chats;
    
    public int ChatCount
    {
        get => chats.Count;
    }
    
    
}