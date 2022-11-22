using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void StartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("level0-1");
    }

    public void SelectLevel()
    {
        Time.timeScale = 1;
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

    public void StartLevelDemo()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("level1_1 1");
    }
    public void StartLevel0_2()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("level0-2");
    }
    public void StartLevel0_3()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("level0-3");
    }
    public void PassTutorial()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("TutorialPassScene");
    }
	public void StartLevel1_0()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("level1-0");
    }

	public void StartLevel1_1()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("level1_1");
    }

    public void StartLevel1_2()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("level1_2");
    }
    
    public void StartLevel1_3()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("level1_3");
    }
    
    public void StartLevel2_0()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("level2-0");
    }

	public void StartLevel2_1()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("level2-1");
    }
    
    public void StartLevel2_2()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("level2-2");
    }
    
    public void StartLevel2_3()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("level2-3");
    }
    
    public void StartLevel3_0()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("level3-0");
    }

	public void StartLevel3_1()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("level3-1");
    }
    
    public void StartLevel3_2()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("level3-2");
    }
    
    public void StartLevel3_3()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("level3-3");
    }
    
    public void PassLevel3()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level3PassScene");
    }
    
    public void StartLevel4_0()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("level4-0");
    }

	public void StartLevel4_1()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("level4-1");
    }
    
    public void StartLevel4_2()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("level4-2");
    }
    
    public void StartLevel4_3()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("level4-3");
    }
    
    public void PassAllLevels()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level4PassScene");
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("StartScene");
    }
}