using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    public List<Slider> slider;

    private void Start()
    {
        Load();
    }
    public void Load()
    {
        dataTransfer.maxJumpHeight = PlayerPrefs.GetFloat("HdS", 1);
        dataTransfer.speed = PlayerPrefs.GetFloat("Speed", 1);
        dataTransfer.Volume = PlayerPrefs.GetFloat("Volume", 0.5f);
        dataTransfer.ejectionDistance = PlayerPrefs.GetFloat("DistEjec", 1);
    }
    public void Save()
    {
        PlayerPrefs.SetFloat("HdS", slider[0].value);
        PlayerPrefs.SetFloat("Speed", slider[1].value);
        PlayerPrefs.SetFloat("Volume", slider[2].value);
        PlayerPrefs.SetFloat("DistEjec", slider[3].value);
    }
}
