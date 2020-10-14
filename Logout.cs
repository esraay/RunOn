using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KNNEnvirement;
using System.Linq;
using System.IO;
using System;

public class Logout : MonoBehaviour
{
    public Text speed;
    private float timeRemaining;
    public float minutes;
    public float seconds;
    void Start()
    {
        timeRemaining = 0;
    }
    void Update()
    {
        timeRemaining += Time.deltaTime;
        if (timeRemaining > 0)
        {
            minutes = Mathf.Floor(timeRemaining / 60);
            seconds = Mathf.Floor(timeRemaining % 60);
            speed.text = " " + minutes.ToString("00") + ":" + seconds.ToString("00");
        }
        
    }

    public int GameScore()
    {
        return Convert.ToInt32(minutes * 60 + seconds);
    }

}
