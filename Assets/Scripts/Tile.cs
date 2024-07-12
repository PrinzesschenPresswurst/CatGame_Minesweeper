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
    [SerializeField] private Sprite flagSprite;
    [SerializeField] private Sprite flagSpriteWon;
    private ParticleSystem _bombParticle;
    
    private SpriteRenderer _spriteRenderer;
    private TextMeshPro _tileText;
    private int _tileRow;
    private int _tileColumn;
    private int[,] _neighbors;
    public bool HasBomb { get; private set; }
    public int AdjacentBombAmount { get; private set; }
    public bool WasUncovered { get; private set; }

    private bool _wasFlagged;
    public static event Action<Tile> TileWasClicked;
    public static event Action<int> FlagWasToggled;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = tileFaceCovered;
        _tileText = GetComponentInChildren<TextMeshPro>();
        _bombParticle = GetComponentInChildren<ParticleSystem>();
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
        
        if (HasBomb)
        {
            _tileText.text = " ";
            _spriteRenderer.sprite = bombSprite;
            _bombParticle.Play();
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

    public void DetermineNeighbors()
    {
        _neighbors = new int [8, 2];
        int[,] adjacentTiles =
        {
            { -1, 1 }, { 0, 1 }, { 1, 1 },
            { -1, 0 }, { 1, 0 },
            { -1, -1 }, { 0, -1 }, { 1, -1 },
        };
        
        for (int i = 0; i < adjacentTiles.GetLength(0); i++)
        {
            _neighbors[i,0] =  _tileColumn + adjacentTiles [i,0];
            _neighbors[i,1] =  _tileRow + adjacentTiles [i,1];
        }
    }
    
    public void CountBombsInNeighbours()
    {
        foreach (var tile in GameGrid.GameTiles)
        {
            Tile tileComponent = tile.GetComponent<Tile>();
            for (int i = 0; i < _neighbors.GetLength(0); i++)
            {
                if (!tileComponent.HasBomb)
                    continue;
                if (tileComponent._tileColumn == _neighbors[i,0] && tileComponent._tileRow == _neighbors[i,1] ) 
                    AdjacentBombAmount++;
            }
        }
    }
    
    public void ToggleFlag()
    {
        if (!_wasFlagged)
        {
            _spriteRenderer.sprite = flagSprite;
            
            if (FlagWasToggled != null)
                FlagWasToggled(1);
        }
        else
        {
            _spriteRenderer.sprite = tileFaceCovered;
            if (FlagWasToggled != null)
                FlagWasToggled(-1);
        }
        
        _wasFlagged = !_wasFlagged;
    }

    public void SetGreenFlag()
    {
        _spriteRenderer.sprite = flagSpriteWon;
    }

    public void UncoverNeighbours()
    {
        foreach (var tile in GameGrid.GameTiles)
        {
            Tile tileComponent = tile.GetComponent<Tile>();
            for (int i = 0; i < _neighbors.GetLength(0); i++)
            {
                if (tileComponent._tileColumn == _neighbors[i, 0] && tileComponent._tileRow == _neighbors[i, 1])
                {
                    if (tileComponent.HasBomb)
                        break;
                    if (tileComponent.WasUncovered)
                        break;
                    tileComponent.UncoverTile();
                    
                    if (tileComponent.AdjacentBombAmount == 0)
                    {
                        tileComponent.UncoverNeighbours(); // call recursion
                    }
                    break;
                }
            }
        }
    }
}
