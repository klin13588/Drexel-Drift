using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skidding : MonoBehaviour
{
    //followed a tutorial by negleft/Flat Tutorials, which is written in Javascript; I converted it to C#. 
    // https://www.youtube.com/watch?v=A0Xv6tsMJSs
    private WheelHit hit;
    public float currentFrictionValue;
    private float skidAt = 0.7f;
    public GameObject skidSound; 
    public GameObject skidSoundClone;
    public float soundEmission;
    private float delay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetComponent<WheelCollider>().GetGroundHit(out WheelHit hit);
        currentFrictionValue = Mathf.Abs(hit.sidewaysSlip);
        if (skidAt <= currentFrictionValue && delay <= 0){ 
            skidSoundClone = Instantiate(skidSound, hit.point, Quaternion.identity); 
            delay = 1;
            Destroy(skidSoundClone, 1.0f);
        } 
        delay -= Time.deltaTime*soundEmission;
    }
}
