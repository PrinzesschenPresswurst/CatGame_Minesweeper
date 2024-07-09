using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameGrid : MonoBehaviour
{
    [SerializeField] private int _columns = 3;
    [SerializeField] private int _rows = 2;
    [SerializeField] private GameObject tileAsset;
    public static float ScreenHeight { get; private set; }
    public static float ScreenWidth { get; private set; }
    public static int CurrentColumn { get; set; }
    public static int CurrentRow { get; set; }

    public static List<GameObject> gameTiles = new List<GameObject>();

    private int randomColumn;
    private int randomRow;

    
    private int[,] _gameGrid;

    private void Start()
    {
        GetScreenSize(); 
        DrawGrid();
        BombPicker.PickBombTiles();
    }

    private void GetScreenSize()
    {
        ScreenHeight= Camera.main.orthographicSize *2;
        ScreenWidth = ScreenHeight * Camera.main.aspect;
    }
    
    private void DrawGrid()
    {
        _gameGrid = new int[_columns,_rows];
        
        float xOffset = (float)_columns/ 2 - 0.5f;
        float yOffset = (float)_rows / 2 - 0.5f;
        Vector3 middlePos = new Vector3(ScreenWidth / 2, ScreenHeight / 2, 0);
        Vector3 middlePosOffset = new Vector3(ScreenWidth / 2 - xOffset , ScreenHeight / 2 -yOffset, 0);
        
        
        for (int columns = 0; columns < _gameGrid.GetLength(0); columns++)
        {
            for (int rows = 0; rows < _gameGrid.GetLength(1); rows++)
            {
                CurrentColumn = columns;
                CurrentRow = rows;
                GameObject currentTile = Instantiate(tileAsset, middlePosOffset, quaternion.identity);
                gameTiles.Add(currentTile);
                
                middlePosOffset.y += 1;
                
            }
            middlePosOffset.y  = ScreenHeight / 2 - yOffset ;
            middlePosOffset.x += 1;
        }
    }
    
}
