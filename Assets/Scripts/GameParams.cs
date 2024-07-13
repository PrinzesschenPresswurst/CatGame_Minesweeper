using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameParams : MonoBehaviour
{
    public static int Rows { get;  set; }
    public static int Columns { get;  set; }
    public static int BombAmount { get;  set; }
    private GameParams _instance;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else Destroy(this);
    }
    
    public void SelectEasy()
    {
        Rows = 7;
        Columns = 7;
        BombAmount = 5;
        SceneManager.LoadScene(sceneBuildIndex:1);
    }
    
    public void SelectMedium()
    {
        Rows = 12;
        Columns = 8;
        BombAmount = 15;
        SceneManager.LoadScene(sceneBuildIndex:1);
    }
    
    public void SelectHard()
    {
        Rows = 16;
        Columns = 9;
        BombAmount = 30;
        SceneManager.LoadScene(sceneBuildIndex:1);
    }
}
