using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    private void Update()
    {
        if(this.gameObject.activeSelf)
        {
            Time.timeScale = 0;
        }
    }
    public void Continue()
    {
        Time.timeScale = 1.0f;
        this.gameObject.SetActive(false);
    }
    public void Menu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}
