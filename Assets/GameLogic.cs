using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    private void Start()
    {
        Tile.TileWasClicked += OnTileWasClicked;
    }

    private void OnTileWasClicked(Tile tile)
    {
        if (tile.WasUncovered)
            return;
        
        tile.UncoverTile();
        
        if (!tile.HasBomb && tile.AdjacentBombAmount == 0)
            UncoverAllEmpty();
        
        if (tile.HasBomb)
            GameLost();
        
        if (CheckGameWon())
            GameWon();
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

    private void GameLost()
    {
        Debug.Log("game over");
    }

    private void GameWon()
    {
        Debug.Log("YOU WIN!");
    }
}
