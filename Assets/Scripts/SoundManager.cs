using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _tileSound;
    
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad (gameObject);
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        Tile.TileWasClicked += OnTileWasClicked;
        GameLogic.GameIsOver += OnGameIsOver;
    }

    private void OnTileWasClicked(Tile tile)
    {
       _audioSource.PlayOneShot(_tileSound);
    }
    
    private void OnGameIsOver(bool result)
    {
        Tile.TileWasClicked -= OnTileWasClicked;
        GameLogic.GameIsOver -= OnGameIsOver;
    }
    
}
