using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class spriteEditor : MonoBehaviour
{
    [SerializeField]
    public string filepath = "";
    public Sprite mc_smile;
    public Sprite mc_smile_2;
    public Sprite mc_angry;
    public Sprite mc_annoyed;
    public Sprite mc_silhoutte;
    public Sprite mom_normal;
    public Sprite mom_sad;
    public Sprite mom_frown;
    public Sprite grandpa_cry;
    public Sprite grandpa_sad;
    public Sprite grandpa_smile;
    public Sprite mom_silhoutte;
    public Sprite girl_silhoutte;
    public Sprite girl_shocked;
    public Sprite girl_laughing;
    public Sprite girl_default;
    public Sprite girl_smile;
    public Sprite girl_angry;

    public int page = 0;
    
    void Start()
    {
        
    }

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
        
        updateCurrentSprite();
    }

    public void updateCurrentSprite()
    {
        string[] characterOnScreen = File.ReadAllLines(filepath);

        try
        {    
            switch(characterOnScreen[page])
            {
                case "mc_smile":
                    changeIcon(mc_smile);
                    break;
                case "mc_smile_2":
                    changeIcon(mc_smile_2);
                    break;
                case "mc_angry":
                    changeIcon(mc_angry);
                    break;
                case "mc_annoyed":
                    changeIcon(mc_annoyed);
                    break;
                case "mc_silhoutte":
                    changeIcon(mc_silhoutte);
                    break;
                case "mom_normal":
                    changeIcon(mom_normal);
                    break;
                case "mom_sad":
                    changeIcon(mom_sad);
                    break;
                case "mom_frown":
                    changeIcon(mom_frown);
                    break;
                case "mom_silhoutte":
                    changeIcon(mom_silhoutte);
                    break;
                case "grandpa_cry":
                    changeIcon(grandpa_cry);
                    break;
                case "grandpa_sad":
                    changeIcon(grandpa_sad);
                    break;
                case "grandpa_smile":
                    changeIcon(grandpa_smile);
                    break;
                case "girl_silhoutte":
                    changeIcon(girl_silhoutte);
                    break;
                case "girl_shocked":
                    changeIcon(girl_shocked);
                    break; 
                case "girl_laughing":
                    changeIcon(girl_laughing);
                    break;
                case "girl_default":
                    changeIcon(girl_default);
                    break;
                case "girl_smile":
                    changeIcon(girl_smile);
                    break;
                case "girl_angry":
                    changeIcon(girl_angry);
                    break; 
            }
        }
        
        catch
        {
            gameObject.SetActive(false);
        }
    }
    public void changeIcon(Sprite emotion)
    {
        SpriteRenderer SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        SpriteRenderer.sprite = emotion;
        
    }

}