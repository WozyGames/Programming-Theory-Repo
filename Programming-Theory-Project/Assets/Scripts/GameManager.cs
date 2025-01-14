using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private GameObject[] terminals;

    // Start is called before the first frame update
    void Start()
    {
        terminals = GameObject.FindGameObjectsWithTag("Terminal");
    }

    public void CheckActiveTerminals()
    {
        int activeTerminals = 0;
        foreach (GameObject terminal in terminals)
        {
            if (terminal.transform.childCount > 0)
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

}
