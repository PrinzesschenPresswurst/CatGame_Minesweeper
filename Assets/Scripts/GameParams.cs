using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameParams : MonoBehaviour
{
    public static int Rows { get;  set; }
    public static int Columns { get;  set; }
    public static int BombAmount { get;  set; }
    private static GameParams _instance;
    public static GameSize SelectedGameSize { get; set; }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad (gameObject);
    }
    
    

    public enum GameSize
    {
        Small,
        Medium,
        Big
    };
}
