using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Menu : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    [SerializeField] private DirectMessageSO dm;

    public UXMLHelper uxmlHelper;
    
    private VisualElement _root;

    private Button _discordApp;
    private Button _exitApp;

    private ScrollView _discordDmPanel;

    private bool create;
    
    private void Awake()
    {
        _root = uiDocument.rootVisualElement;
    }

    private void OnEnable()
    {
        _discordApp = _root.Q<Button>("Discord-app");
        _exitApp = _root.Q<Button>("Exit-app");
        
        _discordDmPanel = _root.Q<ScrollView>("ScrollView");
        
        _discordApp.clicked += HandleOpenDiscord;
        _exitApp.clicked += HandleExit;
        
                    
    }

    private void SceneTransition()
    {
        VisualElement sceneBackground = _root.Q<VisualElement>("SceneTransition");

        PlayerPrefs.SetInt("SceneTransition" , 1);
            
        sceneBackground.AddToClassList("on");
            
        sceneBackground.schedule.Execute(() =>
        {
            SceneManager.LoadScene("InGame");
        }).StartingIn(1200);
    }

    private void OnDisable()
    {
        _discordApp.clicked -= HandleOpenDiscord;
        _exitApp.clicked -= HandleExit;
    }
    private void HandleExit()
    {
        Application.Quit();
    }

    private void HandleOpenDiscord()
    {
        _discordDmPanel.AddToClassList("on");

        StartCoroutine(MakeChatRoutine());
    }
    

    private IEnumerator MakeChatRoutine()
    {
        if(create)yield break;
        create = true;
        
        var DateInfo = uxmlHelper.GetTree(UXML.DateInfo).Instantiate();
        DateInfo.style.flexGrow = 0;

        Label date = DateInfo.Q<Label>();
        date.text = $"{DateTime.Now.Year}.{DateTime.Now.Month}.{DateTime.Now.Day}. {DateTime.Now.Hour}:{DateTime.Now.Minute}";
        
        _discordDmPanel.Add(DateInfo);
                
        for (int i = 0; i < dm.ChatCount; i++)
        {
            //채팅 나오기 전에 나오는 그 로딩그거 있잖아 그거..
            var loadingTree = uxmlHelper.GetTree(UXML.Loading).Instantiate();
            loadingTree.style.flexGrow = 0;
            
            _discordDmPanel.Add(loadingTree);
            
            VisualElement LoadingDM = loadingTree.Q<VisualElement>("DM");
            LoadingDM.RemoveFromClassList("on");

            if (dm.chats[i].chatType == Character.Player)
            {
                loadingTree.style.alignItems = Align.FlexEnd;
                LoadingDM.AddToClassList("player");
            }            
            
            List<VisualElement> loadingCircleList = loadingTree.Query<VisualElement>("loadingCircle").ToList();
            VisualElement[] loadingCircleArray = loadingCircleList.ToArray();
                        
            int randomCount = Random.Range(10,20);
                
            for (int j= 0; j < randomCount; j++)
            {
                loadingCircleArray[j % loadingCircleArray.Length].ToggleInClassList("on");
                yield return new WaitForSeconds(0.15f);
            }
            loadingTree.RemoveFromHierarchy();
            
            //진짜 채팅
            var tree = uxmlHelper.GetTree(UXML.Chat).Instantiate();
            tree.style.flexGrow = 0;
            
            _discordDmPanel.Add(tree);
                        
            VisualElement DM = tree.Q<VisualElement>("DM");
            
            int descriptionLength = dm.chats[i].description.Length;
            float sum = 0;

            for (int j = 0; j < descriptionLength; j++)
            {
                if (dm.chats[i].description[j] == ' ' || dm.chats[i].description[j] == '.' || dm.chats[i].description[j] == '!')
                    sum += 4f;
                else
                    sum += 30;
            }

            sum += 29;
            
            DM.style.width = sum;
                        
            if (dm.chats[i].chatType == Character.Player)
            {
                tree.style.alignItems = Align.FlexEnd;
                DM.AddToClassList("player");
            }
            
            
            tree.Q<Label>("Description").text = dm.chats[i].description;

        }
        //버튼 추가해야됨 마지막에..
        var btnTree = uxmlHelper.GetTree(UXML.NextBtn).Instantiate();
        btnTree.style.flexGrow = 0;
        
        _discordDmPanel.Add(btnTree);

        Button nextBtn = btnTree.Q<Button>();
        nextBtn.clicked += SceneTransition;

    }

    private void HandleNext()
    {
        SceneManager.LoadScene("InGame");
    }
}
