using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KNNEnvirement;
using System.Linq;
using System.IO;
using System;

public class score_save : MonoBehaviour
{
    public Text user_score;
    public int score;
    public int amazing;

    public void GameScore()
    {
        Logout gameend = user_score.GetComponent<Logout>();
        score = gameend.GameScore();
        PlayerPrefs.SetInt("Score", score);
        user_score.text = score.ToString();
    }
}
