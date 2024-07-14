using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameGrid : MonoBehaviour
{
    [SerializeField] private GameObject tileAsset;
    
    private static float _screenHeight;
    private static float _screenWidth;
    private Camera _cam;
    private float _tileAssetScale;
    private int[,] _gameGrid;

    public static List<GameObject> GameTiles { get; private set; }
    
    private void Start()
    {
        _tileAssetScale = 1f;
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
        _screenHeight= _cam.orthographicSize *2;
        _screenWidth = _screenHeight * _cam.aspect;
    }

    private void SetCameraPos()
    {
        _cam.transform.position = new Vector3(GameGrid._screenWidth/2, GameGrid._screenHeight/2, -10);
    }
    
    private void DrawGrid()
    {
        _gameGrid = new int[GameParams.Columns,GameParams.Rows];
        _tileAssetScale = CheckIfFitsScreen() ;
        float xOffset = (GameParams.Columns/ 2f - (_tileAssetScale/2))*_tileAssetScale;
        float yOffset = (GameParams.Rows / 2f - (_tileAssetScale/2)) * _tileAssetScale;
        Vector3 middlePosOffset = new Vector3(_screenWidth / 2f - xOffset , _screenHeight / 2f -yOffset, 0);
        
        
        for (int columns = 0; columns < _gameGrid.GetLength(0); columns++)
        {
            for (int rows = 0; rows < _gameGrid.GetLength(1); rows++)
            {
                GameObject currentTile = Instantiate(tileAsset, middlePosOffset, quaternion.identity);
                currentTile.transform.localScale = new Vector3(_tileAssetScale- 0.1f, _tileAssetScale- 0.1f, 1);
                GameTiles.Add(currentTile);
                currentTile.GetComponent<Tile>().SetPosition(rows, columns);
                
                middlePosOffset.y += _tileAssetScale;
            }
            
            middlePosOffset.y  = _screenHeight / 2 - yOffset ;
            middlePosOffset.x += _tileAssetScale;
        }
    }

    private float CheckIfFitsScreen()
    { 
        float uiBuffer = 2f *2; 
        float actualScreenSize = _screenHeight - uiBuffer;
        float allTiles = GameParams.Rows * _tileAssetScale;
        
        while (actualScreenSize <= allTiles)
        {
            _tileAssetScale = _tileAssetScale - 0.01f;
            allTiles = GameParams.Rows * _tileAssetScale;
        }

        return _tileAssetScale;
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
