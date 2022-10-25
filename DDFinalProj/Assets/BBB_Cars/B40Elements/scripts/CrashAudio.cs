using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashAudio : MonoBehaviour
{
    // written with reference to UnityAPI guide 
    public GameObject collisionSound;
    public GameObject collisionSoundClone; 
    // to avoid an error, you need to destroy the clone of an asset rather than deleting the asset itself 
    private Rigidbody car;
    private Transform carTransform;  
    private Vector3 position;
    
    void Start()
    {
        carTransform = car.transform;
        position = carTransform.position;
    }
    void Update()
    { 

    }
    void OnCollisionEnter(Collision collision)
    { 
        collisionSoundClone = Instantiate(collisionSound, position, Quaternion.identity);
        Debug.Log("We have made collisions");
        Destroy(collisionSoundClone, 1);
    }
}
