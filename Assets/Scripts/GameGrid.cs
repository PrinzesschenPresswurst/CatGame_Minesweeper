using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameGrid : MonoBehaviour
{
    [SerializeField] private GameObject tileAsset;
    private static float ScreenHeight { get; set; }
    private static float ScreenWidth { get; set; }
    private Camera _cam;

    public static List<GameObject> GameTiles;
    
    private int[,] _gameGrid;

    private void Start()
    {
        GameTiles = new List<GameObject>();
        _cam = Camera.main;
        GetScreenSize(); 
        SetCameraPos();
        DrawGrid();
        PickBombTiles();
        RunAdjacentFinder();
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
    
    private static void PickBombTiles()
    {
        List<GameObject> bombTiles = new List<GameObject>();

        for (int i = 0; i < GameParams.BombAmount; i++)
        {
            int bomb = Random.Range(0, GameTiles.Count);
            GameObject bombTile = GameTiles[bomb];
            bombTile.GetComponent<Tile>().SetBomb();
            GameTiles.Remove(bombTile);
            bombTiles.Add(bombTile);
        }

        foreach (var tile in bombTiles)
        {
            GameTiles.Add(tile);
        }
    }
    
    private static void RunAdjacentFinder()
    {
        foreach (var tile in GameTiles)
        {
            Tile tileComponent = tile.GetComponent<Tile>();
            tileComponent.DetermineNeighbors();
            tileComponent.CountBombsInNeighbours();
        }
    }
}
