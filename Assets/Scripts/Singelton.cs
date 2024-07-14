using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singelton : MonoBehaviour
{
    private static Singelton _instance;
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
}
