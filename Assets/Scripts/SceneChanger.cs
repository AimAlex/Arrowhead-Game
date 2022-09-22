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
        SceneManager.LoadScene("TutorialScene");
    }

    public void JustTry()
    {
        SceneManager.LoadScene("Base Scene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartLevel2()
    {
        SceneManager.LoadScene("level2-1");
    }

    public void StartLevel3()
    {
        SceneManager.LoadScene("level3-1");
    }

    public void StartLevel4()
    {
        SceneManager.LoadScene("level4-1");
    }

    public void StartLevel5()
    {
        SceneManager.LoadScene("level5-1");
    }

    public void StartLevel6()
    {
        SceneManager.LoadScene("level6-1");
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("StartScene");
    }
}