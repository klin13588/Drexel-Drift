using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class textEditor : MonoBehaviour
{
    [SerializeField] public GameObject playButton;
    public string filepath = "";
    int page = 0;
    public TMP_Text textBox;
   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            page++;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (page > 0)
            {
                page--;
            }
            
            else
            {
                page = 0;
            }
        }
        
        updateText();
    }

    public void updateText()
    {
        try
        {
            string[] stringList = File.ReadAllLines(filepath);
            textBox.text = stringList[page];
        }

        catch
        {
            textBox.text = "";
            playButton.SetActive(true);
        }
        
    }

}
