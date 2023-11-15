using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class MenuManager : MonoBehaviour
{
    public GameObject menuOption;
    public List<Slider> reglage;
    public Button option;

    void Awake()
    {
        menuOption.SetActive(false);
    }
    private void Update()
    {
        if(Gamepad.current.buttonEast.wasPressedThisFrame)
        {
            CloseOption();
        }
    }
    public void OpenOption()
    {
        reglage[0].Select();
        menuOption.SetActive(true);
    }
    private void SaveOption()
    {
        dataTransfer.maxJumpHeight = (int)reglage[0].value;
        dataTransfer.maxJumpTime = (int)reglage[1].value;
        int volume = (int)reglage[2].value;
        dataTransfer.ejectionDistance = (int)reglage[3].value;
        dataTransfer.stuntTime = (int)reglage[4].value;
    }
    public void CloseOption()
    {
        SaveOption();
        option.Select();
        menuOption.SetActive(false);
    }
    public void Jouer()
    {
        SceneManager.LoadScene("Game");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
