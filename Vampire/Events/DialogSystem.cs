using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public enum State
{
    ChatStart,
    Game,
    ChatEnd
}

public class DialogSystem : MonoBehaviour
{
    private ChatSO _chatSo;
    public ChatSO chatSoStart;
    public ChatSO chatSoGoodEnd;
    public ChatSO chatSoEndBad;
    
    public AnswerSO AnswerSo;
    public UXMLHelper UxmlHelper;
    
    [Space]
    public State state;
    
    public float chatSpeed;
    
    private UIDocument _uiDocument;
    private VisualElement _root, _character,_gameBox;
    private Button _dialogBox , _gameBtn1 , _gameBtn2 , _gameBtn3;
    private Label _name, _description , _titleText, _episodeText;
    private TextField _answerField;
    
    private QuizGame _quizGame;
    private QuestionType _questionType;
    
    private int _index = -1;
    private string _answer;
    
    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
        _quizGame = GetComponent<QuizGame>();

        _chatSo = chatSoStart;
        state = State.ChatStart;
    }
    private void OnEnable()
    {
        _root = _uiDocument.rootVisualElement;
        
        _character = _root.Q<VisualElement>("Character");
        _gameBox = _root.Q<VisualElement>("Game-box");
        
        _name = _root.Q<Label>("Name");
        _description = _root.Q<Label>("Description");
        
        _dialogBox = _root.Q<Button>("Dialog-box");
        _gameBtn1 = _root.Q<Button>("Game-btn1");
        _gameBtn2 = _root.Q<Button>("Game-btn2");
        _gameBtn3 = _root.Q<Button>("Game-btn3");
            
        _answerField = _root.Q<TextField>("AnswerFiled");
        
        _titleText = _root.Q<Label>("title-text");
        _episodeText = _root.Q<Label>("episode-text");
        
        _dialogBox.clicked += HandleDialog;
        
        _gameBtn1.clicked += () =>
        {
            HandleGameBtn(_gameBtn1.text);
        };
        _gameBtn2.clicked += () =>
        {
            HandleGameBtn(_gameBtn2.text);
        };
        _gameBtn3.clicked += () =>
        {
            HandleGameBtn(_gameBtn3.text);
        };

        _answerField.RegisterCallback<KeyDownEvent>(OnAnswerSubmit);

    }

    private void Start()
    {
        _titleText.text = chatSoStart.title;
        _episodeText.text = $"Episode.{chatSoStart.episode.ToString()}";
        
        InitializeDialog();
    }

    private void OnAnswerSubmit(KeyDownEvent evt)
    {
        if (evt.keyCode == KeyCode.Return && _answerField.value != string.Empty)
        {
            _quizGame.curQuestionCount--;
            if (_quizGame.GetAnswer() == _answerField.text)
            {
                ToggleGamePanel(false);
                
                var answer = UxmlHelper.GetTree(UXML.Answer).Instantiate();
                answer.style.position = Position.Absolute;
                answer.style.flexGrow = 0;
                Label answerLabel = answer.Q<Label>("Answer");
                
                _root.Add(answer);
                
                answer.RegisterCallback<GeometryChangedEvent>((evt) =>
                {
                    _chatSo = chatSoGoodEnd;
                    state = State.ChatEnd;
                    InitializeDialog();
                    
                    answerLabel.AddToClassList("on");

                    answerLabel.schedule.Execute(() =>
                    {
                        answerLabel.RemoveFromClassList("on");
                    }).StartingIn(1500);
                    
                });
            }
            else
            {
                ToggleGamePanel(false);
                StartCoroutine(TypeDescription("오답"));
                _answerField.value = String.Empty;
            }
            
            evt.StopPropagation();
            evt.PreventDefault();
        }
    }
    private void HandleGameBtn(string buttonText)
    {
        _questionType = _quizGame.GetQuestionType(buttonText);
        
        _quizGame.curQuestionCount--;
        _quizGame.RemoveDescription(buttonText);
        
        ToggleGamePanel(false);
        
        _answer = AnswerSo.GetAnswer(_questionType);
        StartCoroutine(TypeDescription(_answer));
        
    }
    private void HandleDialog()
    {
        switch (state)
        {
            case State.ChatStart:
                ContinueDialog();
                break;
            case State.Game:

                if (_quizGame.curQuestionCount <= 0)
                {
                    _chatSo = chatSoEndBad;
                    state = State.ChatEnd;
                    InitializeDialog();
                    break;
                }
                
                DisplayQuestion();
                ToggleGamePanel(true);
                break;
            case State.ChatEnd:
                ContinueDialog();
                break;
        }
    }
    private void ContinueDialog()
    {
        if (_description.text == _chatSo.list[_index].description)
        {
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            _description.text = _chatSo.list[_index].description;
        }
    }
    private void NextLine()
    {
        if (_index < _chatSo.list.Count - 1)
        {
            _index++;
            DisplayText();
        }
        else if(state == State.ChatStart)
        {
            StartGame();
        }
        else if (state == State.ChatEnd)
        {
            var sceneTransition = _root.Q<VisualElement>("SceneTransition");
            sceneTransition.AddToClassList("on");

            sceneTransition.schedule.Execute(() =>
            {
                SceneManager.LoadScene("Menu");
            }).StartingIn(1200);
        }
    }
    private void StartGame()
    {
        state = State.Game;
        
        _quizGame.GameStart();
        _name.text = Character.Heroine.ToString();
        
        ToggleGamePanel(true);
        DisplayQuestion();
    }
    private void DisplayQuestion()
    {
        List<string> questionList = _quizGame.GetDescription();
        
        _gameBtn1.text = questionList[0];
        _gameBtn2.text = questionList[1];
        _gameBtn3.text = questionList[2];
    }
    private void DisplayText()
    {
        //이름 넣어주기
        if (_chatSo.list[_index].character != Character.Null)
        {
            _name.text = _chatSo.list[_index].character.ToString();
        }
        else
        {
            _name.text = string.Empty;
        }
        
        //할일 있으면 하고
        _chatSo.list[_index].onInteractions?.Invoke();
                
        UpdateCharacterImage();
                
        if (_chatSo.list[_index].character == Character.Heroine)
        {
            _dialogBox.AddToClassList("heroin");
        }
        else
        {
            _dialogBox.RemoveFromClassList("heroin");
        }
                
        //대사 바꾸기
        _description.text = String.Empty;
        StartCoroutine(TypeDescription(_chatSo.list[_index].description));
    }
    private IEnumerator TypeDescription(string str)
    {
        foreach (char c in str)
        {
            _description.text += c;
            yield return new WaitForSeconds(chatSpeed);
        }
    }
    private void UpdateCharacterImage()
    {
        if (_chatSo.list[_index].cg != null)
        {
            var texture = Utility.SpriteToTexture(_chatSo.list[_index].cg);
            if (texture != null)
            {
                _character.style.backgroundImage = new StyleBackground(texture);
                _character.style.display = DisplayStyle.Flex;
            }
        }
    }
    private void InitializeDialog()
    {
        _index = 0;
        DisplayText();
    }
    private void ToggleGamePanel(bool active)
    {
        if (active)
        {
            _dialogBox.AddToClassList("off");
            _character.AddToClassList("game");
            _gameBox.RemoveFromClassList("off");
        }
        else
        {

            _description.text = string.Empty;
            _dialogBox.RemoveFromClassList("off");
            _character.RemoveFromClassList("game");
            _gameBox.AddToClassList("off");
        }
        
        
    }
}