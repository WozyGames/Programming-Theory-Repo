using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class UIMenu : MonoBehaviour
{

    public void StartGame()
    {
        Debug.Log("Start Game Pressed");
    }

    public void LoadLevelSelector()
    {
        Debug.Log("Level Selector Pressed");
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
