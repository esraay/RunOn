using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class GameScene : MonoBehaviour
{
    public GameObject GyroscopeControl;
    public GameObject Background;
    public GameObject InfoScreen;

    void Start()
    {
        if (!(Input.isGyroAvailable))
        {
            GyroscopeControl.SetActive(true);

        }
        else
        {
            Background.SetActive(true);
            
        }

    }
    public void Home()
    {
        InfoScreen.SetActive(false);
    }
    public void BolumAc(string BolumIsmi)
    {
        Application.LoadLevel(BolumIsmi);
        Time.timeScale = 1;
    }

    public void OpenInfo()
    {
        InfoScreen.SetActive(true);
    }
    public void Cikis()
    {
        Application.Quit();
    }
}
