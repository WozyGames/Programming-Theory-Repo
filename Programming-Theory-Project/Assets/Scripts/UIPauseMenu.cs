using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPauseMenu : UIMenu
{
    [SerializeField] private GameObject _menu;

    private bool menuState;

    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && !menuState)
        {
            Time.timeScale = 0;
            _menu.SetActive(true);
            menuState = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && menuState)
        {
            Time.timeScale = 1;
            _menu.SetActive(false);
            menuState = false;
        }

    }
}
