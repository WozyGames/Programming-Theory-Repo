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
        Debug.Log("Start Game Pressed");
    }

    public void LoadLevelSelector()
    {
        SceneManager.LoadScene("LevelSelector");
    }

    public void LoadLevel(string level)
    {     
        SceneManager.LoadScene(level);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

}
