using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    float timer = 179;
    Text timerText;
    public GameObject victoryMenu;
    private void Awake()
    {
        timerText = GetComponent<Text>();
        victoryMenu.SetActive(false);
    }
    void Update()
    {
        CheckTime();
    }
    void CheckTime()
    {
        timer -= Time.deltaTime;
        float timeminute = Mathf.Floor(timer / 60);
        float timeseconde = timer % 60;
        timerText.text = (timeminute + ":" + Mathf.RoundToInt(timeseconde)).ToString();
        if (timer <= 0)
        {
            Time.timeScale = 0;
            victoryMenu.SetActive(true);
        }
    }
}
