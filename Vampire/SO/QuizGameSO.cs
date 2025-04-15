using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine;
using System;

public enum QuestionType
{
    Yes,
    No,
    WeakYes,
    WeakNo,
    Error
}

[Serializable]
public struct QuestionInfo
{
    public QuestionType type;
    public string descriptions;
}

[CreateAssetMenu(fileName = "Quiz_" , menuName = "SO/QuizGame")]
public class QuizGameSO : ScriptableObject
{
    public string answer;

    public List<QuestionInfo> questionInfos;
    private List<string> newDescription;

    private void OnEnable()
    {
        newDescription = new List<string>();
    }

    public List<string> GetQuestions()
    {
        newDescription.Clear();
        List<QuestionInfo> questionInfosCopy = new List<QuestionInfo>(questionInfos);
        
        for (int i = 0; i < 3; i++)
        {
            int questionCount = questionInfosCopy.Count;
            int randomIndex = Random.Range(0,questionCount);
            newDescription.Add(questionInfosCopy[randomIndex].descriptions);
            questionInfosCopy.RemoveAt(randomIndex);
        }
        
        return newDescription;
    }

    public void RemoveDescription(string str)
    {
        questionInfos.RemoveAll(x => x.descriptions == str);
    }

    public QuestionType GetQuestionType(string str)
    {
        foreach (var item in questionInfos)
        {
            if(item.descriptions == str)
            {
                return item.type;
            }
        }
        
        return QuestionType.Error;
    }
    
    
}
