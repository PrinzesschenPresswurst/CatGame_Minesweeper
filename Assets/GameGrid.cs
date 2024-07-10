using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameGrid : MonoBehaviour
{
    [SerializeField] private GameObject tileAsset;
    public static float ScreenHeight { get; private set; }
    public static float ScreenWidth { get; private set; }
    private Camera _cam;

    public static List<GameObject> GameTiles = new List<GameObject>();
    
    private int[,] _gameGrid;

    private void Start()
    {
        _cam = Camera.main;
        GetScreenSize(); 
        SetCameraPos();
        DrawGrid();
        BombPicker.PickBombTiles();
        RunAdjacentFinder();
        SetTileTexts();
    }

    private void GetScreenSize()
    {
        ScreenHeight= _cam.orthographicSize *2;
        ScreenWidth = ScreenHeight * _cam.aspect;
    }

    private void SetCameraPos()
    {
        _cam.transform.position = new Vector3(GameGrid.ScreenWidth/2, GameGrid.ScreenHeight/2, -10);
    }
    
    private void DrawGrid()
    {
        _gameGrid = new int[GameParams.Columns,GameParams.Rows];
        
        float xOffset = (float)GameParams.Columns/ 2 - 0.5f;
        float yOffset = (float)GameParams.Rows / 2 - 0.5f;
        Vector3 middlePos = new Vector3(ScreenWidth / 2, ScreenHeight / 2, 0);
        Vector3 middlePosOffset = new Vector3(ScreenWidth / 2 - xOffset , ScreenHeight / 2 -yOffset, 0);
        
        
        for (int columns = 0; columns < _gameGrid.GetLength(0); columns++)
        {
            for (int rows = 0; rows < _gameGrid.GetLength(1); rows++)
            {
                GameObject currentTile = Instantiate(tileAsset, middlePosOffset, quaternion.identity);
                GameTiles.Add(currentTile);
                currentTile.GetComponent<Tile>().SetPosition(rows, columns);
                
                middlePosOffset.y += 1;
            }
            
            middlePosOffset.y  = ScreenHeight / 2 - yOffset ;
            middlePosOffset.x += 1;
        }
    }

    private void RunAdjacentFinder()
    {
        foreach (var tile in GameTiles)
        {
            tile.GetComponent<Tile>().DetermineAdjacentBombs();
        }
    }

    private void SetTileTexts()
    {
        foreach (var tile in GameTiles)
        {
            tile.GetComponent<Tile>().SetTileText();
        }
    }
    
}
