using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class MusicManager : MonoBehaviour
{
    public AudioSource music;
    [SerializeField] Slider volumeSlider;
    private static MusicManager _instance;

    public static MusicManager Instance
    {
        get
        {
            return _instance;
        }
    }
    public AudioClip[] songs;
    public float volume;

//private void Awake()
//{
//    if (_instance==null && SceneManager.GetActiveScene().buildIndex != 0)
//    {
 //       _instance = this;
//        DontDestroyOnLoad(_instance);
 //   }
 //   else
//        Destroy(this.gameObject); 
//}

    void Start()
    {


        music = GetComponent<AudioSource>();
        if(!music.isPlaying)
            ChangeSong(Random.Range(0,songs.Length));
        

        if(!PlayerPrefs.HasKey("musicVolume"))
        {  
            PlayerPrefs.SetFloat("musicVolume",1);
            Load();
        }

        else
        {
        Load();
        }


    }
void Update()
{
    music.volume = volumeSlider.value;
    if(!music.isPlaying)
        ChangeSong(Random.Range(0,songs.Length));
}

private void Load()
    {
        
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume",volumeSlider.value);
    }


public void ChangeSong(int songPicked)
{
    music.clip = songs[songPicked];
    music.Play();
}

}
