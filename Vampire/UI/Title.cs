using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Title : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    private VisualElement _root;
    private VisualElement _sceneTransition;
    
    private Button _startBtn, _exitBtn;
    
    void Awake()
    {
        _root = uiDocument.rootVisualElement;
    }

    private void OnEnable()
    {
        _startBtn = _root.Q<Button>("Start-btn");
        _exitBtn = _root.Q<Button>("Exit-btn");
        _sceneTransition = _root.Q<VisualElement>("SceneTransition");
        
        
        _startBtn.clicked += HandleStart;
        _exitBtn.clicked += HandleExit;
    }

    private void HandleExit()
    {
        Application.Quit();
    }

    private void HandleStart()
    {
        _sceneTransition.AddToClassList("on");
        _sceneTransition.schedule.Execute(() =>
        {
            SceneManager.LoadScene("Menu");
        }).StartingIn(1200);
    }
}
