using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameParams : MonoBehaviour
{
    public static int Rows { get;  set; }
    public static int Columns { get;  set; }
    public static int BombAmount { get;  set; }
    public static GameSize SelectedGameSize { get; set; }

    public enum GameSize
    {
        Small,
        Medium,
        Big
    };
}
