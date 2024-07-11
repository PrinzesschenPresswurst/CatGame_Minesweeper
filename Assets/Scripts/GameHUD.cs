using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameHUD : MonoBehaviour
{
    [SerializeField] private Canvas gameCanvas;
    [SerializeField] private TextMeshProUGUI remainingBombsText;
    [SerializeField] private TextMeshProUGUI timerText;
    
    [SerializeField] private Canvas endCanvas;
    [SerializeField] private TextMeshProUGUI endMessage;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI newHighScoreText;
    
    private int _highScore;

    private float GameTimer { get; set;}
   
    private void Start()
    {
        newHighScoreText.gameObject.SetActive(false);
        SetInitialHighScore();
        GameLogic.GameIsOver += OnGameOver;
        remainingBombsText.text = "Bombs: " + GameParams.BombAmount;
        gameCanvas.gameObject.SetActive(true);
        endCanvas.gameObject.SetActive(false);
    }

    private void SetInitialHighScore()
    {
        if (PlayerPrefs.GetInt("_highScore") == 0)
        {
            PlayerPrefs.SetInt("_highScore", int.MaxValue);
        }
    }

    private void Update()
    {
        if (GameLogic.GameHasEnded)
            return;
        
        GameTimer += Time.deltaTime;
        timerText.text = "Time: " +(int)GameTimer;
    }

    private void OnGameOver(bool result)
    {
        GameLogic.GameIsOver -= OnGameOver;
        endCanvas.gameObject.SetActive(true);
        
        if (result)
        {
            SetHighScore();
            scoreText.text = "Highscore: " + _highScore;
            endMessage.text = "You win, you are cool.";
        }
            
        else
        {
            _highScore = PlayerPrefs.GetInt("_highScore");
            if (_highScore == int.MaxValue)
                _highScore = 0;
            newHighScoreText.gameObject.SetActive(false);
            scoreText.text = "Highscore: " + _highScore;
            endMessage.text = "You lost, everything explodes.";
        }
    }

    private void SetHighScore()
    {
        _highScore = PlayerPrefs.GetInt("_highScore");
        if (GameTimer < _highScore)
        {
            PlayerPrefs.SetInt("_highScore", (int)GameTimer);
            newHighScoreText.gameObject.SetActive(true);
        }
        _highScore = PlayerPrefs.GetInt("_highScore");
    }
    public void PlayAgain()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
