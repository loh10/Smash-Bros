using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPause : MonoBehaviour
{
    public MultiplayerManager mpm;
    public GameObject pauseMenu;
    private void Awake()
    {
        mpm = GameObject.Find("EventManager").GetComponent<MultiplayerManager>();
    }
    private void Update()
    {
        if(mpm.Nbplayer>=1)
        {
            if (Gamepad.current.startButton.wasPressedThisFrame && !pauseMenu.activeSelf)
            {
                OpenPause();
            }
            else if(Gamepad.current.startButton.wasPressedThisFrame && pauseMenu.activeSelf)
            {
                Continue();
            }
        }
    }
    void OpenPause()
    {
        ChangeInput(false);
        pauseMenu.SetActive(true);
        pauseMenu.GetComponentInChildren<Button>().Select();
        Time.timeScale = 0;
    }

    private void ChangeInput(bool activate)
    {
        for (int i = 1;i<mpm.Nbplayer+1; i++)
        {
          GameObject.Find("player " + i).GetComponent<playerController>().enabled = activate;  
        }
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Continue()
    {
        pauseMenu.gameObject.SetActive(false);
        ChangeInput(true);
        Time.timeScale = 1.0f;
    }
    public void Menu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}
