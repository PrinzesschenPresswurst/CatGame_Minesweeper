using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameParams : MonoBehaviour
{
    [SerializeField] private int rows;
    [SerializeField] private int columns;
    [SerializeField] private int bombAmount;

    public static int Rows { get; private set; }
    public static int Columns { get; private set; }
    public static int BombAmount { get; private set; }

    private void Awake()
    {
        Rows = rows;
        Columns = columns;
        BombAmount = bombAmount;
    }
}
