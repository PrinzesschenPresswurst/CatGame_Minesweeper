using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
    
    
    private int _setFlagCounter;
   
    private void Start()
    {
        GameLogic.GameIsOver += OnGameOver;
        GameLogic.FlagModeActivated += OnFlagModeActivated;
        GameLogic.DigModeActivated += OnDigModeActivated;
        Tile.FlagWasToggled += OnFlagWasToggled;
        GameEndSceneHandler.EndFeedbackPlayed += OnEndFeedbackPlayed;
        
        gameCanvas.gameObject.SetActive(true);
        endCanvas.gameObject.SetActive(false);
        OnDigModeActivated();
        SetBombCounter();
    }
    
    private void Update()
    {
        timerText.text = DisplayMinutes((int)GameLogic.GameTimer);
    }
    
    public static string DisplayMinutes(int whatToDisplay)
    {
        int minutes = whatToDisplay / 60;
        int seconds = whatToDisplay % 60; 
        
        if (seconds < 10)
            return  "" + minutes + ":0" + seconds;
        
        return  "" + minutes + ":" + seconds;
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
    
    private void OnGameOver(bool result)
    {
        GameLogic.GameIsOver -= OnGameOver;
        GameLogic.FlagModeActivated -= OnFlagModeActivated;
        GameLogic.DigModeActivated -= OnDigModeActivated;
        Tile.FlagWasToggled -= OnFlagWasToggled; 
        
        timerText.text = DisplayMinutes((int)GameLogic.GameTimer);
    }
}
