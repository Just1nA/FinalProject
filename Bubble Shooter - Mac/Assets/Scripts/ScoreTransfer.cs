using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTransfer : MonoBehaviour
{
    public static int SCORE;
    public static int HighScore;
    public Text scoreText;   

    void Awake()
    {   
        HighScore = 0;

        SCORE = PlayerPrefs.GetInt("YourScore");
        HighScore = PlayerPrefs.GetInt("HighScore");
        if(SCORE > HighScore) PlayerPrefs.SetInt("HighScore", SCORE);
        scoreText.text = SCORE.ToString();
    }
}
