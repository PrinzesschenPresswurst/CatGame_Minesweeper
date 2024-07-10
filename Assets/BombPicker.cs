using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class BombPicker : MonoBehaviour
{
    public static void PickBombTiles()
    {
        List<GameObject> bombTiles = new List<GameObject>();
        Debug.Log(GameGrid.GameTiles.Count);
        
        for (int i = 0; i < GameParams.BombAmount; i++)
        {
            int bomb = Random.Range(0, GameGrid.GameTiles.Count);
            GameObject bombTile = GameGrid.GameTiles[bomb];
            bombTile.gameObject.GetComponent<Tile>().HasBomb = true;
            bombTile.GetComponent<Tile>().UpdateColor(Color.blue);
            GameGrid.GameTiles.Remove(bombTile);
            bombTiles.Add(bombTile);
            Debug.Log(GameGrid.GameTiles.Count);
        }

        foreach (var tile in bombTiles)
        {
            GameGrid.GameTiles.Add(tile);
        }
        Debug.Log(GameGrid.GameTiles.Count);
    }

    public static void PickBombCoordinates()
    {
        int placedBombs = 0;
        
        while (placedBombs < GameParams.BombAmount)
        {
            int row = Random.Range(0, GameParams.Rows);
            int column = Random.Range(0, GameParams.Columns);
            
            foreach (var tile in GameGrid.GameTiles)
            {
                if (tile.GetComponent<Tile>().TileRow == row && tile.GetComponent<Tile>().TileColumn == column)
                {
                    if (!tile.GetComponent<Tile>().HasBomb)
                    {
                        tile.GetComponent<Tile>().HasBomb = true;
                        tile.GetComponent<Tile>().UpdateColor(Color.blue);
                        placedBombs++;
                        break;
                    }
                }
            }
        }
    }
}
