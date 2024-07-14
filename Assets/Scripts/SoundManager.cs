using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip tileSound;
    
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
        Tile.TileWasClicked += OnTileWasClicked; // dont unsubscribe cause dont destroyonload
    }

    private void OnTileWasClicked(Tile tile)
    {
       _audioSource.PlayOneShot(tileSound);
    }
    
    
    
}
