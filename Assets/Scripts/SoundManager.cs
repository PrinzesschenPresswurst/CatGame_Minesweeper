using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SoundManager : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip tileSound;
    [SerializeField] private AudioClip bombSound;
    [SerializeField] private AudioClip greenFlagSound;
    [SerializeField] private AudioClip setFlagSound;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip loseSound;
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        Tile.TileWasClicked += OnTileWasClicked; // dont unsubscribe cause dontDestroyOnLoad
        Tile.BombUncovered += OnBombUncovered;
        Tile.GreenFlagSet += OnGreenFlagSet;
        Tile.FlagSet += OnFlagSet;
        GameEndSceneHandler.EndFeedbackPlayed += OnEndFeedbackPlayed;
    }

    private void OnTileWasClicked(Tile tile)
    {
        if (GameLogic.DigModeActive && !tile.WasUncovered) 
            _audioSource.PlayOneShot(tileSound);
    }

    private void OnBombUncovered(Tile tile)
    {
        _audioSource.PlayOneShot(bombSound);
    }

    private void OnGreenFlagSet(Tile tile)
    {
        _audioSource.PlayOneShot(greenFlagSound);
    }

    private void OnFlagSet(Tile tile)
    {
        _audioSource.PlayOneShot(setFlagSound);
    }

    private void OnEndFeedbackPlayed()
    {
        if (GameLogic.GameWasWon)
            _audioSource.PlayOneShot(winSound);
        if (!GameLogic.GameWasWon)
            _audioSource.PlayOneShot(loseSound);
    }
}
