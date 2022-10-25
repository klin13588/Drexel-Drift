using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    // Potentially Obsolete
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;

    }

    public void MariosPark()
    {
        SceneManager.LoadScene("trackWithTImer");
    }

    public void UnclesHome()
    {
        SceneManager.LoadScene("Track2");
    }

    public void Sus()
    {
        SceneManager.LoadScene("track3");
    }

    public void Sunset()
    {
        SceneManager.LoadScene("track4");
    }

    public void CH1()
    {
        SceneManager.LoadScene("VNCH1");
    }

    public void CH2()
    {
        
        SceneManager.LoadScene("VNCH2");
    }

    public void CH3()
    {
        SceneManager.LoadScene("VNCH3");
    }

    public void CH1PLAY()
    {
        PlayerPrefs.SetInt("selectedCar", 0);
        SceneManager.LoadScene("trackWithTImer");
    }

    public void CH2PLAY()
    {
        PlayerPrefs.SetInt("selectedCar", 1);
        SceneManager.LoadScene("track2");
    }

    public void CH3PLAY()
    {
        PlayerPrefs.SetInt("selectedCar", 2);
        SceneManager.LoadScene("track3");
    }

    public void Return()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ResetScene()
     {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Time.timeScale = 1f;


     }


    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
