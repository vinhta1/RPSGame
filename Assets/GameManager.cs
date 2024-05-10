using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JoyconUnity;

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
    public GameObject player1; //empty containers for Joycon Demo
    public GameObject player2;
    public JoyconDemo p1Joycon; //joyconDemo class for player 1
    public JoyconDemo p2Joycon;
    private List<Joycon> joycons; //list of connected joycons

    public int rps;
    public int rps2;
    public int winner;
    public bool challengeCheck;
    public bool challengeFlag = true;
    public bool winCheck;

    private void Start()
    {
        p1Joycon = player1.GetComponent<JoyconDemo>();
        p2Joycon = player2.GetComponent<JoyconDemo>();
        joycons = JoyconManager.Instance._connectedJoycons; //initialize list
    }

    private void Update()
    {
        Joycon j1 = joycons[p1Joycon.jc_ind];
        Joycon j2 = joycons[p2Joycon.jc_ind];

        if (p1Joycon.ready && p2Joycon.ready && challengeFlag)
        {
            challengeFlag = false;
            challengeCheck = true;
        }
        if (!challengeFlag)
        {
            j1.SetRumble(160, 320, 0.6f);
            j2.SetRumble(160, 320, 0.6f);

            if (Mathf.Abs(p1Joycon.gyro.x) >= 10)
            {
                rps = 1;
            } else if (Mathf.Abs(p1Joycon.gyro.y) >= 10)
            {
                rps = 2;
            }
            else if (Mathf.Abs(p1Joycon.gyro.z) >= 10)
            {
                rps = 3;
            }

            if (Mathf.Abs(p2Joycon.gyro.x) >= 10)
            {
                rps2 = 1;
            }
            else if (Mathf.Abs(p2Joycon.gyro.y) >= 10)
            {
                rps2 = 2;
            }
            else if (Mathf.Abs(p2Joycon.gyro.z) >= 10)
            {
                rps2 = 3;
            }
        }
        if (challengeFlag)
        {
            j1.SetRumble(0, 0, 0);
            j2.SetRumble(0, 0, 0);
        }
    }

}
