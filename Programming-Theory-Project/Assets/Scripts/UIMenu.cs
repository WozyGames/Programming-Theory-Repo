using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenu : MonoBehaviour
{

    public void StartGame()
    {
        Time.timeScale = 1;
        if (PlayerPrefs.GetInt("lastPlayedLevel") > 2)
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("lastPlayedLevel"));
            AudioManager.instance.PlayLevelMusic(PlayerPrefs.GetString("lastPlayedLevelMusic"));
        }
        else
        {
            SceneManager.LoadScene(2);
            AudioManager.instance.PlayLevelMusic("Level1");
        }
    }

    public void LoadMainMenu()
    {
        GameManager.instance.SaveProgress();
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadLevelSelector()
    {
        GameManager.instance.SaveProgress();
        Time.timeScale = 1;
        SceneManager.LoadScene("LevelSelector");
    }

    public void LoadLevel(string level)
    {
        GameManager.instance.SaveProgress();
        Time.timeScale = 1;
        SceneManager.LoadScene(level);
        AudioManager.instance.PlayLevelMusic(level);
    }

    public void ExitGame()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            GameManager.instance.SaveProgress();
        }
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

}
