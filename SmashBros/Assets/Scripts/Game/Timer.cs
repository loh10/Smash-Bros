using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    public float timer = 179;
    Text timerText;
    public GameObject victoryMenu;
    MultiplayerManager mpm;
    private void Awake()
    {
        mpm = GameObject.Find("EventManager").GetComponent<MultiplayerManager>();
        timerText = GetComponent<Text>();
        victoryMenu.SetActive(false);
    }
    void Update()
    {
        CheckTime();
    }
    void CheckTime()
    {
        if(mpm.Nbplayer>0)
        {
            timer -= Time.deltaTime;
            timerText.text = ((int)timer).ToString();
            if (timer <= 0)
            {
                Time.timeScale = 0;
                victoryMenu.SetActive(true);
            }
        }
    }
}
