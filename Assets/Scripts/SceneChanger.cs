using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("level1_1");
    }

    public void SelectLevel()
    {
        SceneManager.LoadScene("SelectScene");
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("TutorialScene 1");
    }

    public void JustTry()
    {
        SceneManager.LoadScene("Base Scene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartLevel1_2()
    {
        SceneManager.LoadScene("level1_2");
    }
    
    public void StartLevel1_3()
    {
        SceneManager.LoadScene("level1_3");
    }
    
    public void StartLevel2_1()
    {
        SceneManager.LoadScene("level2-1");
    }
    
    public void StartLevel2_2()
    {
        SceneManager.LoadScene("level2-2");
    }
    
    public void StartLevel2_3()
    {
        SceneManager.LoadScene("level2-3");
    }
    
    public void StartLevel3_1()
    {
        SceneManager.LoadScene("level3-1");
    }
    
    public void StartLevel3_2()
    {
        SceneManager.LoadScene("level3-2");
    }
    
    public void StartLevel3_3()
    {
        SceneManager.LoadScene("level3-3");
    }
    
    public void StartLevel4_1()
    {
        SceneManager.LoadScene("level4-1");
    }
    
    public void StartLevel4_2()
    {
        SceneManager.LoadScene("level4-2");
    }
    
    public void StartLevel4_3()
    {
        SceneManager.LoadScene("level4-3");
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("StartScene");
    }
}