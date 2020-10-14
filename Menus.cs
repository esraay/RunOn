using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menus : MonoBehaviour
{
   
    public PlayerControl player;
    public GameObject PauseMenu;
    public void PauseGame()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0;

    }
    public void ResumeGame()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        Application.LoadLevel(Application.loadedLevel);
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void returnMenu(string menu)
    {
        Application.LoadLevel(menu);
        Time.timeScale = 1;

    }
}
