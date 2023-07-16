using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    private static GameManger _instance;
    public static GameManger Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("GameManager is Null");
            }
            return _instance;
        }
    }

    public bool hasKeyToCastle { get; set; }

    private void Awake()
    {
        _instance = this;
    }
}
