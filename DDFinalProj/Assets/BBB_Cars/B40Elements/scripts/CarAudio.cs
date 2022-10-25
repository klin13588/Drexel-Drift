using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAudio : MonoBehaviour

{
    public float minimumPitch = 0.05f;
    public float maximumPitch = 2f;
    public float engineSpeed = 1f; 
    AudioSource audioSource;
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.pitch = minimumPitch;
        }

        void Update()
        {
            if(engineSpeed < minimumPitch){
                audioSource.pitch = minimumPitch;
            }
            else if (engineSpeed > maximumPitch){
                audioSource.pitch = maximumPitch;
            }
            else {
                audioSource.pitch = engineSpeed;
            }
           
        }
}
