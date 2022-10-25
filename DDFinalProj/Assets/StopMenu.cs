using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;

        AudioSource[] audios =FindObjectsOfType<AudioSource>();

        foreach (AudioSource a in audios)
        {if( a.ToString() != "songObject (UnityEngine.AudioSource)")
        a.Play();
        }
        
        GameIsPaused = false;
    }

    
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;


        AudioSource[] audios =FindObjectsOfType<AudioSource>();

        foreach (AudioSource a in audios)
        {if( a.ToString() != "songObject (UnityEngine.AudioSource)")
        a.Pause();
        }
        GameIsPaused = true;

    }

}

