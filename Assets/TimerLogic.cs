using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerLogic : MonoBehaviour
{
    public int timer = 20;
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
            TakeoutCoroutine(1);
        }
        //if()

    }
    IEnumerator TakeoutCoroutine(float time)
    {
        GameManager.Instance.challengeCheck = false;
        for (timer = 20; timer > 0; timer--)
        {
            if (GameManager.Instance.rps != 0 && GameManager.Instance.rps2 != 0)
            {
                break;
            }


            yield return new WaitForSeconds(time);
            //timer -= 1;


        }

        //timer = 0;
        GameManager.Instance.winCheck = true;

    }
  }
