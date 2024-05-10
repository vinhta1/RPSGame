using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance is null)
            {
                Debug.LogError("Game Manager is NULL");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
    public int rps;
    public int rps2;
    public int winner;
    public bool challengeCheck;
    public bool winCheck;

}
