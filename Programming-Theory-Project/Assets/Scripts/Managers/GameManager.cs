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
        AudioManager.instance.PlaySFX("victory");
        if (SceneManager.sceneCountInBuildSettings != SceneManager.GetActiveScene().buildIndex + 1)
        {
            StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
        }
        else
        {
            Debug.Log("Fin de los niveles");
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        StartCoroutine(ReLoadScene());
    }

    public void SaveProgress()
    {
        if (SceneManager.GetActiveScene().buildIndex != 8)
        {
            PlayerPrefs.SetInt("lastPlayedLevel", SceneManager.GetActiveScene().buildIndex);
            PlayerPrefs.SetString("lastPlayedLevelMusic", SceneManager.GetActiveScene().name);
        }
    }

    IEnumerator LoadScene(int index)
    {
        yield return new WaitForSeconds(timeBetweenScenes);
        SceneManager.LoadScene(index);
        isGameOver = false;
        AudioManager.instance.PlayLevelMusic(SceneManager.GetSceneByBuildIndex(index).name);
    }

    IEnumerator ReLoadScene()
    {
        yield return new WaitForSeconds(timeBetweenScenes);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        isGameOver = false;
    }

}
