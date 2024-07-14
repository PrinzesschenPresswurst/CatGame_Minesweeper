using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    private static int _highScore;
    public static bool HighScoreWasBroken { get; private set; }

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

    private static void SetInitialHighScore()
    {
        if (PlayerPrefs.GetInt( GameParams.SelectedGameSize.ToString()) == 0)
        {
            PlayerPrefs.SetInt(GameParams.SelectedGameSize.ToString(), int.MaxValue);
        }
    }
    
    public static int FetchHighScore()
    {
        _highScore = PlayerPrefs.GetInt(GameParams.SelectedGameSize.ToString());
        if (_highScore == int.MaxValue)
            _highScore = 0;
        return _highScore;
    }

    public static void SetHighScore(float gameTimer)
    {
        _highScore = PlayerPrefs.GetInt(GameParams.SelectedGameSize.ToString());
        HighScoreWasBroken = false;
        if (gameTimer < _highScore)
        {
            PlayerPrefs.SetInt(GameParams.SelectedGameSize.ToString(), (int)gameTimer);
            HighScoreWasBroken = true;
        }
        _highScore = PlayerPrefs.GetInt(GameParams.SelectedGameSize.ToString());
    }
}
