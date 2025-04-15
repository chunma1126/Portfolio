using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "Answer_" , menuName = "SO/Answer")]
public class AnswerSO : ScriptableObject
{
    [TextArea] public List<string> YesAnswer;
    [TextArea] public List<string> WeakYesAnswer;
    [TextArea] public List<string> NoAnswer;
    [TextArea] public List<string> WeakNoAnswer;

    public string GetAnswer(QuestionType type)
    {
        string str = "";
        
        switch (type)
        {
            case QuestionType.Yes:
                str = YesAnswer[Random.Range(0, YesAnswer.Count)];
                break;
            case QuestionType.No:
                str = NoAnswer[Random.Range(0, WeakYesAnswer.Count)];
                break;
            case QuestionType.WeakYes:
                str = WeakYesAnswer[Random.Range(0, NoAnswer.Count)];
                break;
            case QuestionType.WeakNo:
                str = WeakNoAnswer[Random.Range(0, WeakNoAnswer.Count)];
                break;
            case QuestionType.Error:
                str = "에러 발생";
                break;
        }

        return str;
    }
    
    
    
}
