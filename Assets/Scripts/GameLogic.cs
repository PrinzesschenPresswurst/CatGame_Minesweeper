using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public static event Action<bool> GameIsOver;
    public static event Action digModeActivated;
    public static event Action flagModeActivated; 
    
    public static bool GameHasEnded { get; set; }
    private static bool GameWasWon { get; set; }

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
                UncoverAllEmpty();

            if (tile.HasBomb)
            {
                GameWasWon = false;
                HandleGameEnd(GameWasWon);
            }

            if (CheckGameWon())
            {
                GameWasWon = true;
                HandleGameEnd(GameWasWon);
            }
        }

        if (!_digModeActive)
        {
            tile.ToggleFlag();
        }
    }
    
    private void UncoverAllEmpty() // TODO limit to adjacent
    {
        foreach (var tile in GameGrid.GameTiles)
        {
            Tile tileComponent = tile.GetComponent<Tile>();
            if (!tileComponent.HasBomb && tileComponent.AdjacentBombAmount == 0)
            {
                tileComponent.UncoverTile();
            }
        }
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
        if (digModeActivated != null)
             digModeActivated.Invoke();
    }

    public void FlagModeActive()
    {
        _digModeActive = false;
        
        if (flagModeActivated != null) 
            flagModeActivated.Invoke();
    }
}
