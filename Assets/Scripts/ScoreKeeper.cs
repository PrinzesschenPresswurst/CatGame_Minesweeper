using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    public static int HighScore { get; set; }
    public static bool HighScoreWasBroken { get; set; }

    private void Start()
    {
        SetInitialHighScore();
    }

    private void Update()
    {
        ListenForResetHighScoreCheat();
    }
    
    private static void ListenForResetHighScoreCheat()
    {
        if (Input.GetKey(KeyCode.R))
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("playerprefs reset");
        }
    }

    private void SetInitialHighScore()
    {
        if (PlayerPrefs.GetInt( GameParams.SelectedGameSize.ToString()) == 0)
        {
            PlayerPrefs.SetInt(GameParams.SelectedGameSize.ToString(), int.MaxValue);
        }
    }
    
    public static int FetchHighScore()
    {
        HighScore = PlayerPrefs.GetInt(GameParams.SelectedGameSize.ToString());
        if (HighScore == int.MaxValue)
            HighScore = 0;
        return HighScore;
    }

    public static void SetHighScore(float gameTimer)
    {
        HighScore = PlayerPrefs.GetInt(GameParams.SelectedGameSize.ToString());
        HighScoreWasBroken = false;
        if (gameTimer < HighScore)
        {
            PlayerPrefs.SetInt(GameParams.SelectedGameSize.ToString(), (int)gameTimer);
            HighScoreWasBroken = true;
        }
        HighScore = PlayerPrefs.GetInt(GameParams.SelectedGameSize.ToString());
    }
}
