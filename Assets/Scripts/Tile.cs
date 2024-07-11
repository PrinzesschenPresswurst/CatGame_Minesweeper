using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Sprite tileFaceCovered;
    [SerializeField] private Sprite tileFaceBare;
    [SerializeField] private Sprite bombSprite;
    
    private SpriteRenderer _spriteRenderer;
    private int _tileRow;
    private int _tileColumn;
    public bool HasBomb{ get; private set; }
    public int AdjacentBombAmount { get; private set; }
    private TextMeshPro _tileText;
    public bool WasUncovered { get; private set; }
    public static event Action<Tile> TileWasClicked;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = tileFaceCovered;
    }

    private void OnMouseUpAsButton()
    {
        if (GameLogic.GameHasEnded)
            return;
        
        if (TileWasClicked != null)
            TileWasClicked.Invoke(this);
    }

    public void UncoverTile()
    {
        _spriteRenderer.sprite = tileFaceBare;
        WasUncovered = true;
        
        _tileText = GetComponentInChildren<TextMeshPro>();
        if (HasBomb)
        {
            _tileText.text = " ";
            _spriteRenderer.sprite = bombSprite;
        }
            
        else if (AdjacentBombAmount == 0)
            _tileText.text = " ";
        
        else
            _tileText.text = AdjacentBombAmount.ToString();
    }
    
    public void SetPosition(int row, int column)
    {
        _tileRow =row;
        _tileColumn = column;
    }
    
    public void SetBomb()
    {
        HasBomb = true;
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
            
            if (tileComponent._tileRow == _tileRow && tileComponent._tileColumn == _tileColumn - 1)
                AdjacentBombAmount++;
            if (tileComponent._tileRow == _tileRow && tileComponent._tileColumn == _tileColumn+1)
                AdjacentBombAmount++;
            if (tileComponent._tileRow == _tileRow-1 && tileComponent._tileColumn == _tileColumn)
                AdjacentBombAmount++;
            if (tileComponent._tileRow == _tileRow -1 && tileComponent._tileColumn == _tileColumn-1)
                AdjacentBombAmount++;
            if (tileComponent._tileRow == _tileRow -1 && tileComponent._tileColumn == _tileColumn+1)
                AdjacentBombAmount++;
            if (tileComponent._tileRow == _tileRow +1 && tileComponent._tileColumn == _tileColumn)
                AdjacentBombAmount++;
            if (tileComponent._tileRow == _tileRow +1 && tileComponent._tileColumn == _tileColumn-1) 
                AdjacentBombAmount++;
            if (tileComponent._tileRow == _tileRow +1 && tileComponent._tileColumn == _tileColumn+1)
                AdjacentBombAmount++;
        }
    }
}
