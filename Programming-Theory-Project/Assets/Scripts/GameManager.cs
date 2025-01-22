using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    private GameObject[] terminals;

    // Start is called before the first frame update
    void Start()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        CountTerminals();
    }

    private void CountTerminals()
    {
        terminals = GameObject.FindGameObjectsWithTag("Terminal");
    }

    public void CheckActiveTerminals()
    {
        int activeTerminals = 0;

        //Gets the number of active terminals in the scene
        foreach (GameObject terminal in terminals)
        {
            if (terminal.transform.childCount > 3)
            {
                activeTerminals++;
            }
        }

        if (activeTerminals == terminals.Length)
        {
            LevelCompleted();
        }
    }

    public void LevelCompleted()
    {
        Debug.Log("Level Completed");
    }

    public void GameOver()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

}
