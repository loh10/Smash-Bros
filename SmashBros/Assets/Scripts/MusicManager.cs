using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    
    public AudioSource source;
    public List<AudioClip> aJouer;
    int currentMusic= 0;

    // Update is called once per frame
    private void Start()
    {
        Time.timeScale = 1.0f;
        source.volume = dataTransfer.Volume;
        PlayMusic();
    }

    void PlayMusic()
    {
        source.clip = aJouer[currentMusic];
        source.Play();
        if (currentMusic >= aJouer.Count)
        {
            currentMusic = 0;
        }
        else
        {
           currentMusic++;
        }
    }
}
