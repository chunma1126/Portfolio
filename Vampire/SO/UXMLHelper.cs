using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum UXML
{
    Chat,
    Loading,
    NextBtn,
    DateInfo,
    Answer
}

[CreateAssetMenu(menuName = "SO/UXMLHelper")]
public class UXMLHelper : ScriptableObject
{
    public VisualTreeAsset Chat;
    public VisualTreeAsset Loading;
    public VisualTreeAsset NextBtn;
    public VisualTreeAsset DateInfo;
    public VisualTreeAsset Answer;
    
    private Dictionary<UXML, VisualTreeAsset> _treeDictionary;

    private void OnEnable()
    {
        _treeDictionary = new Dictionary<UXML, VisualTreeAsset>();
        
        _treeDictionary.Add(UXML.Chat ,Chat);
        _treeDictionary.Add(UXML.Loading ,Loading);
        _treeDictionary.Add(UXML.NextBtn , NextBtn);
        _treeDictionary.Add(UXML.DateInfo , DateInfo);
        _treeDictionary.Add(UXML.Answer , Answer);
    }

    public VisualTreeAsset GetTree(UXML uxml)
    {
        if(_treeDictionary.TryGetValue(uxml, out var tree))
        {
            return tree;
        }
        return null;
    }
}