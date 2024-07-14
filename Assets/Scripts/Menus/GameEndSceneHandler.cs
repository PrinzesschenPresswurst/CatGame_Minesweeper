using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class GameEndSceneHandler : MonoBehaviour
{
    public static event Action EndFeedbackPlayed;
    [SerializeField] private GameObject flagCelebrateParticle;
    
    void Start()
    {
        GameLogic.GameIsOver += OnGameIsOver;
    }

    private void OnGameIsOver(bool result)
    {
        if (!result)
            StartCoroutine(UncoverBombsSequence());
        else 
            StartCoroutine(GameWonSequence());
            
    }

    IEnumerator UncoverBombsSequence()
    {
        foreach ( var tile in GameGrid.GameTiles)
        {
            Tile tileComponent = tile.GetComponent<Tile>();
            if (tileComponent.WasUncovered)
                continue;
            if (tileComponent.HasBomb)
            { 
                yield return StartCoroutine(UncoverBomb(tileComponent));
            }
        }

        StartCoroutine(FireDonezo());
        GameLogic.GameIsOver -= OnGameIsOver;
    }

    IEnumerator FireDonezo()
    {
        yield return new WaitForSeconds(1f);
        if (EndFeedbackPlayed != null)
            EndFeedbackPlayed.Invoke();
    }

    IEnumerator UncoverBomb(Tile tileComponent)
    {
        yield return new WaitForSeconds(0.2f);
        tileComponent.UncoverTile();
    }

    IEnumerator GameWonSequence()
    {
        foreach ( var tile in GameGrid.GameTiles)
        {
            Tile tileComponent = tile.GetComponent<Tile>();
            if (tileComponent.HasBomb)
            {
                GameObject particle =  Instantiate( flagCelebrateParticle, tile.transform.position, quaternion.identity);
                particle.transform.SetParent(tile.transform);
                yield return StartCoroutine(ValidateFlags(tileComponent));
            }
        }

        StartCoroutine(FireDonezo());
        GameLogic.GameIsOver -= OnGameIsOver;
    }
    
    IEnumerator ValidateFlags(Tile tileComponent)
    {
        yield return new WaitForSeconds(0.2f);
        tileComponent.SetGreenFlag();
    }
}
