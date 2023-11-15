using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPause : MonoBehaviour
{
    public GameObject pauseMenu;
    private void Update()
    {
        if(Gamepad.current.startButton.wasPressedThisFrame)
        {
            OpenPause();
        }
    }
    void OpenPause()
    {
        pauseMenu.SetActive(true);
        pauseMenu.GetComponentInChildren<Button>().Select();
        Time.timeScale = 0;
    }
    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Continue()
    {
        Time.timeScale = 1.0f;
        pauseMenu.gameObject.SetActive(false);
    }
    public void Menu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}
