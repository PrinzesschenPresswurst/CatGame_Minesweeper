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
        int bombAmount = 3;
        Debug.Log(GameGrid.gameTiles.Count);
        
        for (int i = 0; i < bombAmount; i++)
        {
            int bomb = Random.Range(0, GameGrid.gameTiles.Count);
            GameObject bombTile = GameGrid.gameTiles[bomb];
            bombTile.gameObject.GetComponent<Tile>().hasBomb = true;
            bombTile.GetComponent<Tile>().UpdateColor(Color.blue);
            GameGrid.gameTiles.Remove(bombTile);
            bombTiles.Add(bombTile);
            Debug.Log(GameGrid.gameTiles.Count);
        }

        foreach (var tile in bombTiles)
        {
            GameGrid.gameTiles.Add(tile);
        }
        Debug.Log(GameGrid.gameTiles.Count);
    }
}
