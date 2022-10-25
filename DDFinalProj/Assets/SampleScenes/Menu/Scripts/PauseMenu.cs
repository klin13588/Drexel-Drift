using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private Toggle m_MenuToggle;
	private float m_TimeScaleRef = 1f;
    private float m_VolumeRef = 1f;
    private bool m_Paused;


    void Awake()
    {
        m_MenuToggle = GetComponent <Toggle> ();
	}


    private void MenuOn ()
    {
        m_TimeScaleRef = Time.timeScale;
        Time.timeScale = 0f;

        AudioSource[] audios =FindObjectsOfType<AudioSource>();

        foreach (AudioSource a in audios)
        {if( a.ToString() != "songObject (UnityEngine.AudioSource)")
//        a.Pause();
        print(a);
        }

        m_Paused = true;
    }


    public void MenuOff ()
    {
        Time.timeScale = m_TimeScaleRef;
        AudioSource[] audios =FindObjectsOfType<AudioSource>();

        foreach (AudioSource a in audios)
        {if( a.ToString() != "songObject (UnityEngine.AudioSource)")
//        a.Pause();
        print(a);
        }
        m_Paused = false;
    }


    public void OnMenuStatusChange ()
    {
        if (m_MenuToggle.isOn && !m_Paused)
        {
            MenuOn();
        }
        else if (!m_MenuToggle.isOn && m_Paused)
        {
            MenuOff();
        }
    }


#if !MOBILE_INPUT
	void Update()
	{
		if(Input.GetKeyUp(KeyCode.Escape))
		{
		    m_MenuToggle.isOn = !m_MenuToggle.isOn;
            Cursor.visible = m_MenuToggle.isOn;//force the cursor visible if anythign had hidden it
		}
	}
#endif

}
