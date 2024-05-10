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
        GameManager.Instance.challengeCheck = true;
    }

    // Update is called once per frame
    void Update()
    {
        //increase time every second
        if (GameManager.Instance.challengeCheck == true)
        {
            //timer = 20;
            Debug.Log("got here");
            StartCoroutine(TakeoutCoroutine(1));
        }
        //if()

    }
    IEnumerator TakeoutCoroutine(float time)
    {
        Debug.Log(timer);
        GameManager.Instance.challengeCheck = false;
        Debug.Log(timer);
        for (timer = 20; timer > 0; timer--)
        {
            Debug.Log(timer);
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
