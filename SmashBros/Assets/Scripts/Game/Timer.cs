using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    public float timer = 179;
    public float actTime;
    Text timerText;
    public GameObject victoryMenu;
    MultiplayerManager mpm;
    ViePlayer vp;
    public bool victory;
    private void Awake()
    {
        victory = false;
        mpm = GameObject.Find("EventManager").GetComponent<MultiplayerManager>();
        timerText = GetComponent<Text>();
        victoryMenu.SetActive(false);
        actTime = timer;
    }
    void FixedUpdate()
    {
        CheckTime();
        CheckVictory();
    }
    void CheckTime()
    {
        if(mpm.Nbplayer>1)
        {
            actTime -= Time.deltaTime;
            timerText.text = ((int)actTime).ToString();
            if (actTime <= 0)
            {
                Time.timeScale = 0;
                victoryMenu.SetActive(true);
            }
        }
    }

    void CheckVictory()
    {
        if (ValidPlayer() == 1 && actTime <= timer-1)
        {
            victoryMenu.SetActive(true);
            victory = true;
            Time.timeScale = 0;
        }
    }

    private int ValidPlayer()
    {
        
        int nbplayer = 0;
        if(mpm.Nbplayer >=1)
        {
            for (int i = 1; i < mpm.Nbplayer + 1; i++)
            {
                vp = GameObject.Find("player " + i.ToString()).GetComponent<ViePlayer>();
                if (!vp.dead)
                {
                    nbplayer++;
                }
            }
        }
        return nbplayer;
    }
}
