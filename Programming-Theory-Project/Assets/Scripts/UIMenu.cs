using System;
using System.Collections;
using System.Collections.Generic;
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
        }
        else
        {
            SceneManager.LoadScene(2);
        }
    }

    public void LoadLevelSelector()
    {
        PlayerPrefs.SetInt("lastPlayedLevel", SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        SceneManager.LoadScene("LevelSelector");
    }

    public void LoadLevel(string level)
    {
        PlayerPrefs.SetInt("lastPlayedLevel", SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        SceneManager.LoadScene(level);
    }

    public void ExitGame()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            PlayerPrefs.SetInt("lastPlayedLevel", SceneManager.GetActiveScene().buildIndex);
        }
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

}
