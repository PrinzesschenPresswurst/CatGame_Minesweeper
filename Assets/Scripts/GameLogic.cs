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
    public static float GameTimer { get; private set; }
    
    public static bool GameWasWon { get; private set; }
    public static bool DigModeActive { get; private set; }
    
    private void Start()
    {
        Tile.TileWasClicked += OnTileWasClicked;
        
        GameTimer = 0;
        GameHasEnded = false;
        DigModeActive = true;
    }

    private void Update()
    {
        RunTimer();
    }
    
    private void RunTimer()
    {
        if (GameHasEnded)
            return;
        
        GameTimer += Time.deltaTime;
    }

    private void OnTileWasClicked(Tile tile)
    {
        if (tile.WasUncovered)
            return;

        if (DigModeActive)
        {
            tile.UncoverTile();
        
            if (!tile.HasBomb && tile.AdjacentBombAmount == 0)
                UncoverAllEmpty(tile);

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

        if (!DigModeActive)
        {
            tile.ToggleFlag();
        }
    }
    
    private void UncoverAllEmpty(Tile rootTile) 
    {
        rootTile.UncoverNeighbours();
    }
    
    public void DigModeActivate()
    {
        DigModeActive = true;
        if (DigModeActivated != null)
             DigModeActivated.Invoke();
    }

    public void FlagModeActive()
    {
        DigModeActive = false;
        if (FlagModeActivated != null) 
            FlagModeActivated.Invoke();
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
}
