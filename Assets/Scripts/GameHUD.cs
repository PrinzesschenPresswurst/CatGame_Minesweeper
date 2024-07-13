using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class GameHUD : MonoBehaviour
{
    [SerializeField] private Button digButton;
    [SerializeField] private Button flagButton;
    
    [SerializeField] private Canvas gameCanvas;
    [SerializeField] private TextMeshProUGUI remainingBombsText;
    [SerializeField] private TextMeshProUGUI timerText;
    
    [SerializeField] private Canvas endCanvas;
    [SerializeField] private TextMeshProUGUI endMessage;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI newHighScoreText;
    [SerializeField] private TextMeshProUGUI gameTypeText;

    private float _gameTimer;
    private int _setFlagCounter;
   
    private void Start()
    {
        GameLogic.GameIsOver += OnGameOver;
        GameLogic.FlagModeActivated += OnFlagModeActivated;
        GameLogic.DigModeActivated += OnDigModeActivated;
        Tile.FlagWasToggled += OnFlagWasToggled;
        GameEndSceneHandler.EndFeedbackPlayed += OnEndFeedbackPlayed;
        gameTypeText.text = GameParams.SelectedGameSize.ToString();
        gameCanvas.gameObject.SetActive(true);
        endCanvas.gameObject.SetActive(false);
        OnDigModeActivated();
        SetBombCounter();
    }
    
    private void Update()
    {
        RunTimer();
    }

    private void RunTimer()
    {
        if (GameLogic.GameHasEnded)
            return;
        
        _gameTimer += Time.deltaTime;
        timerText.text = " " +(int)_gameTimer;
    }

    private void OnFlagWasToggled(int count)
    {
        _setFlagCounter += count;
        SetBombCounter();
    }

    private void SetBombCounter()
    {
        remainingBombsText.text = " " + (GameParams.BombAmount - _setFlagCounter);

        if (_setFlagCounter > GameParams.BombAmount)
            remainingBombsText.color = Color.red;
        else remainingBombsText.color = Color.black;
    }
    
    
    private void OnGameOver(bool result)
    {
        GameLogic.GameIsOver -= OnGameOver;
        GameLogic.FlagModeActivated -= OnFlagModeActivated;
        GameLogic.DigModeActivated -= OnDigModeActivated;
        Tile.FlagWasToggled -= OnFlagWasToggled; 
        
        int score = ScoreKeeper.FetchHighScore();
        
        if (result)
        {
            ScoreKeeper.SetHighScore(_gameTimer);
            score = ScoreKeeper.FetchHighScore();
            scoreText.text = "" + score;
            endMessage.text = "You win, you are cool.";

            if (ScoreKeeper.HighScoreWasBroken)
                newHighScoreText.text = "New HighScore!";
        }
            
        else
        {
            newHighScoreText.text = "";
            scoreText.text =  "You lost";
            endMessage.text ="old highscore: " + score;
        }
    }

    private void OnEndFeedbackPlayed()
    {
        endCanvas.gameObject.SetActive(true);
        GameEndSceneHandler.EndFeedbackPlayed -= OnEndFeedbackPlayed;
    }

    private void OnFlagModeActivated()
    {
        flagButton.GetComponent<Button>().image.color = Color.blue;
        digButton.GetComponent<Button>().image.color = Color.white;
    }

    private void OnDigModeActivated()
    {
        digButton.GetComponent<Button>().image.color = Color.blue;
        flagButton.GetComponent<Button>().image.color = Color.white;
    }
    
    public void PlayAgain()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
    public void LoadMenu()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(0);
    }
}
