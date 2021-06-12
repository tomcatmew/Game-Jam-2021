using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{

    public bool IsGameOver = false;

    //Implement the Game Rules here
    public void GameOver()
    {
        if (!IsGameOver)
        {
            //Spawn GameOver UI
            GameInstance.Instance.MyPlayerController.AllowPlayerControl = false;
            IsGameOver = true;
            Debug.Log("GameOver");
        }
    }
}
