using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowFOVUpdate : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] public Transform target;
    [SerializeField] private float translateSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] public Rigidbody car;


    private void Awake()
    {
        var caller = PlayerPrefs.GetInt("selectedCar");
        if(caller == 0){
            CallBolbo();
        }
        else if(caller == 1){
            CallBerrari();
        }
        else if(caller == 2){
            CallBus();
        }
        else if(caller == 3){
            CallBorsche();
        }
    }

    private void CallBus()
    {
        this.offset = new Vector3(0, 8, -20);
    }

    private void CallBorsche()
    {
        this.offset = new Vector3(0, 1, -3);
    }

    private void CallBolbo()
    {
        this.offset = new Vector3(0, 2, -6);
    }

    private void CallBerrari()
    {
        this.offset = new Vector3(0, 2, -13);
    }


    private void FixedUpdate()
    {
        HandleTranslation(); 
        HandleRotation();
        WarpCamera();
    }
    
    private void HandleTranslation()
    {
        var targetPosition = target.TransformPoint(offset);
        transform.position = Vector3.Lerp(transform.position, targetPosition, translateSpeed * Time.deltaTime);

    }
    private void HandleRotation()
    {
        var direction = target.position - transform.position;
        var rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
    private void WarpCamera()
    {
        var speed = car.velocity.magnitude;
        this.GetComponent<Camera>().fieldOfView = 65f + 0.3f*speed;
        
    }
}
