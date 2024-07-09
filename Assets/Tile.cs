using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    public int TileRow { get; set; }
    public int TileColumn { get; set; }

    public bool hasBomb { get; set; }

    private void Start()
    { 
        TileRow = GameGrid.CurrentRow;
        TileColumn = GameGrid.CurrentColumn;
    }

    public void UpdateColor(Color color)
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = color;
    }
    
}
