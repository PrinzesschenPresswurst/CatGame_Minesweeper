using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SoundManager : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip tileSound;
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        Tile.TileWasClicked += OnTileWasClicked; // dont unsubscribe cause dontDestroyOnLoad
    }

    private void OnTileWasClicked(Tile tile)
    {
       _audioSource.PlayOneShot(tileSound);
    }
    
    
    
}
