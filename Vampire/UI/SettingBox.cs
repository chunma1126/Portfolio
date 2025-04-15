using UnityEngine;
using UnityEngine.UIElements;

public class SettingBox : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    private VisualElement _root;
    private VisualElement _settingBox;
    
    [SerializeField] private AudioSource player;
    [SerializeField] private AudioClip clickSoundClip;
    private Slider _bgmSlider;

    private Button _continueBtn;
    private Button _saveBtn;
    private Button _exitBtn;
    
    private bool _onSetting = false;
        
    private void Awake()
    {
        _root = uiDocument.rootVisualElement;
    }

    private void OnEnable()
    {
        _settingBox = _root.Q<VisualElement>("Setting-box");

        _bgmSlider = _settingBox.Q<Slider>("Bgm-Slider");
        
        _bgmSlider.value = 1;
        player.volume = _bgmSlider.value;
        
        _bgmSlider.RegisterValueChangedCallback(HandleChangeBgmVolume);

        _continueBtn = _settingBox.Q<Button>("Continue-btn");
        _continueBtn.clicked += HandleContinue;
        _continueBtn.clicked += ClickSound;
        
        _saveBtn = _settingBox.Q<Button>("Save-btn");
        _saveBtn.clicked += HandleSave;
        _saveBtn.clicked += ClickSound;
        
        _exitBtn = _settingBox.Q<Button>("Exit-btn");
        _exitBtn.clicked += HandleExit;
        _exitBtn.clicked += ClickSound;
                
    }

    private void OnDisable()
    {
        _continueBtn.clicked -= HandleContinue;
        _continueBtn.clicked -= ClickSound;
        
        _saveBtn.clicked -= HandleSave;
        _saveBtn.clicked -= ClickSound;
        
        _exitBtn.clicked -= HandleExit;
        _exitBtn.clicked -= ClickSound;
    }

    private void HandleExit()
    {
        Debug.Log("게임종료");
        Application.Quit();
    }

    private void HandleSave()
    {
        Debug.Log("저장");
    }

    private void HandleContinue()
    {
        _onSetting = !_onSetting;
        
        _settingBox.RemoveFromClassList("on");
    }

    private void HandleChangeBgmVolume(ChangeEvent<float> evt)
    {
        player.volume = evt.newValue;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _onSetting = !_onSetting;
            
            if (_onSetting)
            {
                _settingBox.AddToClassList("on");
            }
            else
            {
                _settingBox.RemoveFromClassList("on");
            }
        }
    }

    private void ClickSound()
    {
        player.PlayOneShot(clickSoundClip);
    }
    
}
