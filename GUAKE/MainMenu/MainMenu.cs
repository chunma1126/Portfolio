using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartMainMenu(string stage1)
    {
        SceneManager.LoadScene(stage1);
    }

    private void Start()
    {
        Time.timeScale = 1;
    }
    
    public void Quit()
    {
        //ming
        Application.Quit();
    }
}
