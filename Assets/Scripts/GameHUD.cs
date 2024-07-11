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
    
    private float GameTimer { get; set;}
   
    private void Start()
    {
        OnDigModeActivated();
        newHighScoreText.gameObject.SetActive(false);
        GameLogic.GameIsOver += OnGameOver;
        GameLogic.flagModeActivated += OnFlagModeActivated;
        GameLogic.digModeActivated += OnDigModeActivated;
        
        remainingBombsText.text = "Bombs: " + GameParams.BombAmount;
        gameCanvas.gameObject.SetActive(true);
        endCanvas.gameObject.SetActive(false);
    }
    
    private void Update()
    {
        if (GameLogic.GameHasEnded)
            return;
        
        GameTimer += Time.deltaTime;
        timerText.text = "Time: " +(int)GameTimer;

        if (Input.GetKey(KeyCode.R))
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("player prefs wiped");
        }
            
    }

    private void OnGameOver(bool result)
    {
        GameLogic.GameIsOver -= OnGameOver;
        GameLogic.flagModeActivated -= OnFlagModeActivated;
        GameLogic.digModeActivated -= OnDigModeActivated;
        
        endCanvas.gameObject.SetActive(true);
        int score = ScoreKeeper.FetchHighScore();
        
        if (result)
        {
            ScoreKeeper.SetHighScore(GameTimer);
            score = ScoreKeeper.FetchHighScore();
            scoreText.text = "HighScore: " + score;
            endMessage.text = "You win, you are cool.";
            
            if (ScoreKeeper.HighScoreWasBroken)
                newHighScoreText.gameObject.SetActive(true);
        }
            
        else
        {
            newHighScoreText.gameObject.SetActive(false);
            scoreText.text = "Highscore: " + score;
            endMessage.text = "You lost, everything explodes.";
        }
    }
    
    public void PlayAgain()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
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
}
