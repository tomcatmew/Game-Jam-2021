using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstance : MonoBehaviour
{
    //Singleton Class used for storing and passing data/reference between levels

    public static GameInstance Instance { get; private set; }

    public GameMode MyGameMode;

    public PlayerController MyPlayerController;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            if(MyGameMode == null)
            {
                MyGameMode = gameObject.GetComponent<GameMode>();
            }
            if(MyPlayerController == null)
            {
                MyPlayerController = gameObject.GetComponent<PlayerController>();
            }

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Hello()
    {
        Debug.Log("Hello");
    }
}
