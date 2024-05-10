using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerLogic : MonoBehaviour
{
    public int timer = 3;
    //bool challenger = false;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.challengeCheck = false;
    }

    // Update is called once per frame
    void Update()
    {
        //increase time every second
        if (GameManager.Instance.challengeCheck == true)
        {
            //timer = 20;
            StartCoroutine(TakeoutCoroutine(1));
        }
        //if()

    }
    IEnumerator TakeoutCoroutine(float time)
    {
        GameManager.Instance.challengeCheck = false;
        for (timer = 3; timer > 0; timer--)
        {
            Debug.Log(timer);
            if (GameManager.Instance.rps != 0 && GameManager.Instance.rps2 != 0)
            {
                //break; //stop when both players move
            }


            yield return new WaitForSeconds(time);
            //timer -= 1;


        }

        //timer = 0;
        GameManager.Instance.challengeFlag = true;
        GameManager.Instance.winCheck = true;
        //GameManager.Instance.rps = 0;
        //GameManager.Instance.rps2 = 0;

    }
  }
