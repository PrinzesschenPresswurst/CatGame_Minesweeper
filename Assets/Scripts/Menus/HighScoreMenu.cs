using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoreMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI easyModeText;
    [SerializeField] private TextMeshProUGUI mediumModeText;
    [SerializeField] private TextMeshProUGUI hardModeText;
    
    void Start()
    {
        easyModeText.text = SetText(GameParams.GameSize.Small);
        mediumModeText.text = SetText(GameParams.GameSize.Medium);
        hardModeText.text = SetText(GameParams.GameSize.Big);
    }

    private string SetText(GameParams.GameSize gameSize)
    {
        return gameSize.ToString() + " --> " + ScoreKeeper.FetchHighScore(gameSize);
    }
}
