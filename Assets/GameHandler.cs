using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    void Start()
    {
        SetCameraToMiddle();
    }
    
    private void SetCameraToMiddle()
    {
        Camera.main.transform.position = new Vector3(GameGrid.ScreenWidth/2, GameGrid.ScreenHeight/2, -10);
    }   
    
}
