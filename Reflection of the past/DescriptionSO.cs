using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Description")]
public class DescriptionSO : ScriptableObject
{
    [TextArea]public string[] descriptions;
    
    public string GetDescriptionRandom()
    {
        return descriptions[Random.Range(0, descriptions.Length)];
    }

    public string GetDescriptionIdx(int idx)
    {
        if(idx >= 0 && idx < descriptions.Length)
            return descriptions[idx];
        
        Debug.Log($"{idx} 존재하지 않는 텍스트를 가지고 오려 합니다.");
        return string.Empty;
    }
}
