using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Button = UnityEngine.UI.Button;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class EndPopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI endMessage;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI newHighScoreText;
    [SerializeField] private TextMeshProUGUI gameTypeText;
    
    void Start()
    {
        gameTypeText.text = GameParams.SelectedGameSize.ToString();
        GameLogic.GameIsOver += OnGameOver;
    }
    
    private void OnGameOver(bool result)
    {
        GameLogic.GameIsOver -= OnGameOver;
        
        int score = ScoreKeeper.FetchHighScore(GameParams.SelectedGameSize);
        
        if (result)
        {
            ScoreKeeper.SetHighScore(GameLogic.GameTimer);
            score = ScoreKeeper.FetchHighScore(GameParams.SelectedGameSize);
            scoreText.text = "" + GameHUD.DisplayMinutes(score);;
            endMessage.text = "You win, you are cool.";

            if (ScoreKeeper.HighScoreWasBroken)
                newHighScoreText.text = "New HighScore!";
        }
            
        else
        {
            newHighScoreText.text = "";
            scoreText.text =  "You lost";
            endMessage.text ="old highscore: " + GameHUD.DisplayMinutes(score);
        }
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
