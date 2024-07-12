using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public static event Action<bool> GameIsOver;
    public static event Action DigModeActivated;
    public static event Action FlagModeActivated; 
    
    public static bool GameHasEnded { get; private set; }
    private static bool _gameWasWon;
    private bool _digModeActive;
    
    
    private void Start()
    {
        GameHasEnded = false;
        Tile.TileWasClicked += OnTileWasClicked;
        _digModeActive = true;
    }

    private void OnTileWasClicked(Tile tile)
    {
        if (tile.WasUncovered)
            return;

        if (_digModeActive)
        {
            tile.UncoverTile();
        
            if (!tile.HasBomb && tile.AdjacentBombAmount == 0)
                UncoverAllEmpty(tile);

            if (tile.HasBomb)
            {
                _gameWasWon = false;
                HandleGameEnd(_gameWasWon);
            }

            if (CheckGameWon())
            {
                _gameWasWon = true;
                HandleGameEnd(_gameWasWon);
            }
        }

        if (!_digModeActive)
        {
            tile.ToggleFlag();
        }
    }
    
    private void UncoverAllEmpty(Tile rootTile) 
    {
        rootTile.UncoverNeighbours();
    }

    private bool CheckGameWon()
    {
        foreach (var tile in GameGrid.GameTiles)
        {
            Tile tileComponent = tile.GetComponent<Tile>();
            if (!tileComponent.HasBomb && !tileComponent.WasUncovered)
                return false;
        }
        return true;
    }

    private void HandleGameEnd(bool result)
    {
        GameHasEnded = true;
        if (GameIsOver != null)
        {
            Tile.TileWasClicked -= OnTileWasClicked;
            GameIsOver.Invoke(result);
        }
    }

    public void DigModeActivate()
    {
        _digModeActive = true;
        if (DigModeActivated != null)
             DigModeActivated.Invoke();
    }

    public void FlagModeActive()
    {
        _digModeActive = false;
        
        if (FlagModeActivated != null) 
            FlagModeActivated.Invoke();
    }
}
