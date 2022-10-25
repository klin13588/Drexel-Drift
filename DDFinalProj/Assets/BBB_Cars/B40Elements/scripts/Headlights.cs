using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headlights : MonoBehaviour
{
    // Start is called before the first frame update
    public Light headLampLeft;
    public Light headLampRight;

    public Light tailLightLeft; 
    public Light tailLightRight;
    
    public Renderer headlights;
    public Material headlightsOn;
    public Material headlightsOff;

    public Renderer taillights;
    public Material tallightsOn;
    public Material taillightsOff;

    public bool isOn;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ActivateHeadlight();
    }

    void FixedUpdate()
    {
        
        ActivateTailLights();
    }

    private void ActivateHeadlight(){
        // if(Input.GetKey("1")){
        //     headlights.material = headlightsOn;
        //     headLampLeft.GetComponent<Light>().enabled = true;
        //     headLampRight.GetComponent<Light>().enabled = true;
        // }
        // else if(Input.GetKey("2")){
        //     headlights.material = headlightsOff;
        //     headLampLeft.GetComponent<Light>().enabled = false;
        //     headLampRight.GetComponent<Light>().enabled = false;
        // }
        if(Input.GetKeyDown("1")){
            isOn = !isOn;
        }
        headLampLeft.GetComponent<Light>().enabled = isOn;
        headLampRight.GetComponent<Light>().enabled = isOn;
        if(isOn){
            headlights.material = headlightsOn;
        }
        else{
            headlights.material = headlightsOff;
        }
    }
    private void ActivateTailLights(){
        if((Input.GetKey("s")) || (Input.GetKey("space")) || (Input.GetKey(KeyCode.DownArrow))) {
            tailLightLeft.intensity = 15f;
            tailLightRight.intensity = 15f;
            taillights.material = tallightsOn;
        }
        else{ 
            tailLightRight.intensity = 0f;
            tailLightLeft.intensity = 0f;
            taillights.material = taillightsOff;
        }
    }
}
