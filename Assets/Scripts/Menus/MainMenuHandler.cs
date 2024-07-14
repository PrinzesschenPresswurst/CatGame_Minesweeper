using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    public void SelectEasy()
    {
        GameParams.Rows = 7;
        GameParams.Columns = 7;
        GameParams.BombAmount = 5;
        GameParams.SelectedGameSize = GameParams.GameSize.Small;
        SceneManager.LoadScene(sceneBuildIndex:1);
    }
    
    public void SelectMedium()
    {
        GameParams.Rows = 12;
        GameParams.Columns = 8;
        GameParams.BombAmount = 15;
        GameParams.SelectedGameSize = GameParams.GameSize.Medium;
        SceneManager.LoadScene(sceneBuildIndex:1);
    }
    
    public void SelectHard()
    {
        GameParams.Rows = 16;
        GameParams.Columns = 9;
        GameParams.BombAmount = 30;
        GameParams.SelectedGameSize = GameParams.GameSize.Big;
        SceneManager.LoadScene(sceneBuildIndex:1);
    }
}
