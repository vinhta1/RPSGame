using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPS_Logic : MonoBehaviour
{
    //Takes global variable that is set to rock, paper, or scissors based on input.
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.rps = 0;
        GameManager.Instance.rps2 = 0;

        //winner 0 means tie, 1 means player 1, and 2 means player 2
        GameManager.Instance.winner = 0;
        GameManager.Instance.winCheck = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.winCheck)
        {
            if (GameManager.Instance.rps != GameManager.Instance.rps2)
            {
                //0 is nothing
                //1 is rock
                //2 is paper
                //3 is scissors

                GameManager.Instance.winner = wining(GameManager.Instance.rps, GameManager.Instance.rps2);
            }
            if(GameManager.Instance.winner == 0)
            {
                Debug.Log("Tie");
            }
            else if(GameManager.Instance.winner == 1)
            {
                Debug.Log("Player 1 wins");
            }
            else if(GameManager.Instance.winner == 2)
            {
                Debug.Log("Player 1 wins");
            }
            GameManager.Instance.winCheck = false;
            GameManager.Instance.winner = 0;
        }
    }
    int wining(int player1, int player2)
    {
        if(player1 == 0)
        {
            return 2;
        }
        else if (player2 == 0)
        {
            return 1;
        }
        else
        {
            if(player1 == 1)
            {
                if(player2 == 2)
                {
                    return 2;
                }
                else if(player2 == 3)
                {
                    return 1;
                }
            }
            else if(player2 == 1)
            {
                if (player1 == 2)
                {
                    return 1;
                }
                else if (player1 == 3)
                {
                    return 2;
                }
            }
            else if(player1 == 2)
            {
                if(player2 == 1)
                {
                    return 1;
                }
                else if(player2 == 3)
                {
                    return 2;
                }
            }
            else if(player2 == 2)
            {
                if (player1 == 1)
                {
                    return 2;
                }
                else if (player1 == 3)
                {
                    return 1;
                }
            }
        }
        return 0;
    }
}
