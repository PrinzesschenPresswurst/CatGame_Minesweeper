using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private int _row;
    private int _column;
    private int _bombAmount;

    public void SelectEasy()
    {
        GameParams.Rows = 3;
        GameParams.Columns = 3;
        GameParams.BombAmount = 4;
        SceneManager.LoadScene(sceneBuildIndex:1);
    }
    
    public void SelectMedium()
    {
        GameParams.Rows = 5;
        GameParams.Columns = 5;
        GameParams.BombAmount = 10;
        SceneManager.LoadScene(sceneBuildIndex:1);
    }
    
    public void SelectHard()
    {
        GameParams.Rows = 8;
        GameParams.Columns = 12;
        GameParams.BombAmount = 20;
        SceneManager.LoadScene(sceneBuildIndex:1);
    }
}
