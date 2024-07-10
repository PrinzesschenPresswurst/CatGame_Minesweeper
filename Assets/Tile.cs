using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    public int TileRow;
    public int TileColumn;
    public bool HasBomb;
    public int AdjacentBombAmount;
    public TextMeshPro tileText;

    private void Start()
    {
       
        
    }

    public void SetTileText()
    {
        tileText = GetComponentInChildren<TextMeshPro>();
        
        if (HasBomb)
            tileText.text = "!";
        else if (AdjacentBombAmount == 0)
            tileText.text = " ";
        else
            tileText.text = AdjacentBombAmount.ToString();
    }

    public void SetPosition(int row, int column)
    {
        TileRow =row;
        TileColumn = column;
    }
    
    private void OnMouseUpAsButton()
    {
        Debug.Log("clicked tile ");
    }

    public void UpdateColor(Color color)
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = color;
    }

    public void DetermineAdjacentBombs()
    {
        if (HasBomb)
            return;

        foreach (var tile in GameGrid.GameTiles)
        {
            Tile tileComponent = tile.GetComponent<Tile>();
            if (!tileComponent.HasBomb)
                continue;
            
            if (tileComponent.TileRow == TileRow && tileComponent.TileColumn == TileColumn - 1)
                AdjacentBombAmount++;
            if (tileComponent.TileRow == TileRow && tileComponent.TileColumn == TileColumn+1)
                AdjacentBombAmount++;
            if (tileComponent.TileRow == TileRow-1 && tileComponent.TileColumn == TileColumn)
                AdjacentBombAmount++;
            if (tileComponent.TileRow == TileRow -1 && tileComponent.TileColumn == TileColumn-1)
                AdjacentBombAmount++;
            if (tileComponent.TileRow == TileRow -1 && tileComponent.TileColumn == TileColumn+1)
                AdjacentBombAmount++;
            if (tileComponent.TileRow == TileRow +1 && tileComponent.TileColumn == TileColumn)
                AdjacentBombAmount++;
            if (tileComponent.TileRow == TileRow +1 && tileComponent.TileColumn == TileColumn-1) 
                AdjacentBombAmount++;
            if (tileComponent.TileRow == TileRow +1 && tileComponent.TileColumn == TileColumn+1)
                AdjacentBombAmount++;
        }
    }
}
