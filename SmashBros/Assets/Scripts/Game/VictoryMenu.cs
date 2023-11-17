using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryMenu : MonoBehaviour
{
    public List<Text> playerName;
    public List<Text> playerNbVie;
    public Button replay;
    MultiplayerManager mpm;
    Timer timer;
    public int actPlayer;
    public int maxPlayer;
    private void Awake()
    {
        mpm = GameObject.Find("EventManager").GetComponent<MultiplayerManager>();
        timer = GameObject.Find("timer").GetComponent<Timer>();
        replay.Select();
    }

    
    void Update()
    {
        print(playerNbVie.Count);    
        actPlayer = mpm.Nbplayer;
        if(actPlayer>maxPlayer)
        {
            maxPlayer = actPlayer;
            addPlayerText();
        }
        if(timer.victory)
        {
            DisplayScore();
        }
    }

    private void DisplayScore()
    {
        for(int i = 0; i < maxPlayer;i++)
        {
            print("player " + i + 1);
            playerNbVie[i].text = GameObject.Find("player " + (i+1).ToString()).GetComponent<ViePlayer>().currentLife.ToString();
        }
    }

    private void addPlayerText()
    {
        for (int i = 1; i < maxPlayer+1; i++)
        {
            playerName.Add(GameObject.Find("joueur" + i + "Txt").GetComponent<Text>());
            playerNbVie.Add(GameObject.Find("nbMort" + i.ToString()).GetComponent<Text>());
        }
    }
}
