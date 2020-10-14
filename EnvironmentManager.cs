using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Linq;

public class EnvironmentManager : MonoBehaviour
{
    public GameObject newStreet;
    public GameObject currentStreet;
    private GameObject _oldStreet;
    public GameObject GameOver;
    private int counter;
    public int score;
    public Text user_score;

    private float timeRemaining;
    public float minutes;
    public float seconds;

    void Start()
    {
        timeRemaining = 0;
    }

    void Update() //silinebilir
    {
        timeRemaining += Time.deltaTime;
        if (timeRemaining > 0)
        {
            minutes = Mathf.Floor(timeRemaining / 60);
            seconds = Mathf.Floor(timeRemaining % 60);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Start")
        {
            if(_oldStreet != null)
            {
                Destroy(_oldStreet);

            } else {

                Destroy(GameObject.Find("MainScene"));
            }
        }
        else if(other.tag == "Middle")
        {
            SpawnLevel();
        }
    }

    void SpawnLevel()
    {
        _oldStreet = currentStreet;
        currentStreet = (GameObject) Instantiate(newStreet,currentStreet.transform.GetChild(0).position, Quaternion.identity);
        counter++;
        if(counter == 1)
        {
            Time.timeScale = 0;
            GameOver.SetActive(true);
            score = GameScore();
            PlayerPrefs.SetInt("Score", score);
            user_score.text = score.ToString();
        }
    }
    public int GameScore()
    {
        
        return 6000 / System.Convert.ToInt32(minutes * 60 + seconds);
    }

    

}
