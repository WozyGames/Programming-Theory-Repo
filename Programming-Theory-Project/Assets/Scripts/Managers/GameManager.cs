using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Scene Loader Settings")]
    [SerializeField, InspectorName("Time Between Scenes")] private float timeBetweenScenes;

    public static GameManager instance;

    private GameObject[] terminals;
    [HideInInspector] public bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        CountTerminals();

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

    }

    private void CountTerminals()
    {
        terminals = GameObject.FindGameObjectsWithTag("Terminal");
    }

    public void CheckActiveTerminals()
    {
        CountTerminals();
        int activeTerminals = 0;

        //Gets the number of active terminals in the scene
        foreach (GameObject terminal in terminals)
        {
            if (terminal.transform.childCount > 3 && terminal != null)
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
        isGameOver = false;
    }

    public void GameOver()
    {
        isGameOver = true;
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().name));
    }

    IEnumerator LoadScene(string name)
    {
        yield return new WaitForSeconds(timeBetweenScenes);
        SceneManager.LoadScene(name);
        isGameOver = false;
    }

}
