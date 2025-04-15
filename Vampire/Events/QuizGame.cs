using System.Collections.Generic;
using UnityEngine;

public class QuizGame : MonoBehaviour
{
    [SerializeField] private QuizGameSO quizGameData;

    [SerializeField] public int curQuestionCount;
    private int _questionCount = 20;
    
    public void GameStart()
    {
        curQuestionCount = _questionCount;
        
    }
    
    public bool SubmitAnswer(string str)
    {
        return quizGameData.answer.Equals(str);
    }

    public List<string> GetDescription()
    {
        return quizGameData.GetQuestions();
    }

    public void RemoveDescription(string str)
    {
        quizGameData.RemoveDescription(str);
    }

    public QuestionType GetQuestionType(string str)
    {
        return quizGameData.GetQuestionType(str);
    }

    public string GetAnswer()
    {
        return quizGameData.answer;
    }




}
