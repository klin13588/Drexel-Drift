using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControllerFinalUpdate : MonoBehaviour
{
    // follows the tutorial from https://www.youtube.com/watch?v=Z4HA8zJhGEk. Credit to 
    // GameDevChef

    // getting variables set up for speed 

    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizontalInput;
    private float verticalInput;
    private float currentsteerAngle;
    public float currentbrakeForce;
    public bool isBraking;

    public float currentSpeed;
    [SerializeField] private float decelerationConstant;
    [SerializeField] private float motorforce;
    [SerializeField] private float brakeForce;
    [SerializeField] private float maxSteerAngle;

    [SerializeField] private float maxSpeed;
    
    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    [SerializeField] private Rigidbody car;

    [SerializeField] private AudioSource audioSource;
    

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform  frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;
    [SerializeField] private float audioCoefficient;

    [SerializeField] public int[] gearing;
    [SerializeField] public float backfireThreshold;
    [SerializeField] public GameObject backfireObject;
    [SerializeField] public GameObject tip1;
    [SerializeField] public GameObject tip2;
    
    
    public GameObject backfireObjectClone;
    private float minGearSpeed;
    private float maxGearSpeed;
    public int currentGear;
    public int prevGear;


    private void Start()
    {   
    }

    private void Update()
    {
        Backfire();
    }

    
    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        CarSpeed();
        PlayAudio();
        EngineBraking();
    }

    private void HandleMotor()
    {
        car = GetComponent<Rigidbody>();
        
        // limit the top speed 
        // 5/11/2022 -- Added AWD bit
        currentSpeed = car.velocity.magnitude;
        if(currentSpeed < maxSpeed){
            frontLeftWheelCollider.motorTorque = verticalInput * motorforce;
            frontRightWheelCollider.motorTorque = verticalInput * motorforce;
            rearLeftWheelCollider.motorTorque = verticalInput * motorforce;
            rearRightWheelCollider.motorTorque = verticalInput * motorforce;
        }
        else{
            frontLeftWheelCollider.motorTorque = 0;
            frontRightWheelCollider.motorTorque = 0;
            rearLeftWheelCollider.motorTorque = 0;
            rearRightWheelCollider.motorTorque = 0;
        }
        
        // detect braking
        // activates the brake force and calls ApplyBraking() 
        // while the spacebar is pressed down 
        if (Input.GetKey("space")){
            isBraking = true; 
        }
        else{
            isBraking = false;
        }
        currentbrakeForce = isBraking ? brakeForce : 0f;
        if(isBraking){
            ApplyBraking();
        }
        else{
            frontRightWheelCollider.brakeTorque = currentbrakeForce;
            frontLeftWheelCollider.brakeTorque = currentbrakeForce;
            rearRightWheelCollider.brakeTorque = currentbrakeForce;
            rearLeftWheelCollider.brakeTorque = currentbrakeForce;
        }
        
    }
 

    private void HandleSteering()
    {
        currentsteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentsteerAngle;
        frontRightWheelCollider.steerAngle = currentsteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;

    }

    private void ApplyBraking()
    {
        
        frontRightWheelCollider.brakeTorque = currentbrakeForce;
        frontLeftWheelCollider.brakeTorque = currentbrakeForce;
        rearRightWheelCollider.brakeTorque = currentbrakeForce;
        rearLeftWheelCollider.brakeTorque = currentbrakeForce;
        Debug.Log("braking");
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
    }

    private void CarSpeed()
    {
        car = GetComponent<Rigidbody>();
        currentSpeed = car.velocity.magnitude;
    }
    private void PlayAudio() //FIXME: Making shifting sounds
    {
        minGearSpeed = 0.0f; 
        maxGearSpeed = gearing[0];
        audioSource = GetComponent<AudioSource>();
        for(int i = 0; i <gearing.Length; i++){
            if (gearing[i] > currentSpeed){
                break;
            }
            else if (i == 0){
                minGearSpeed = 0.0f;
            }
            else{
                minGearSpeed = gearing[i-1];
            }
            maxGearSpeed = gearing[i];
            currentGear = i+1;
        }
        //Debug.Log("upper limit" + maxGearSpeed);
        //Debug.Log("lower limit" + minGearSpeed);
        float enginePitch = (((currentSpeed - minGearSpeed))/(maxGearSpeed - minGearSpeed))*audioCoefficient;
        audioSource.pitch = enginePitch;
    }

    private void Backfire()
    {
        var light1 = tip1.GetComponent<Light>();
        var light2 = tip2.GetComponent<Light>();
        var emission1 = tip1.GetComponent<ParticleSystem>().emission;
        var emission2 = tip2.GetComponent<ParticleSystem>().emission;
        
        if((Input.GetKeyUp("w") || Input.GetKeyUp("up")) && currentSpeed > 10f){
            Debug.Log("Backfire!");
            backfireObjectClone = Instantiate(backfireObject, car.position, Quaternion.identity);
            Destroy(backfireObjectClone, 0.3f);
        }
        if(backfireObjectClone){
            emission1.enabled = true;
            emission2.enabled = true;
            light1.enabled = true;
            light2.enabled = true;
        }else{
            emission1.enabled = false;
            emission2.enabled = false;
            light1.enabled = false;
            light2.enabled = false;
        }
    }
    private void EngineBraking()
    {
        if(Input.GetButton(VERTICAL) == false){
            frontRightWheelCollider.brakeTorque = decelerationConstant;
            frontLeftWheelCollider.brakeTorque = decelerationConstant;
            rearRightWheelCollider.brakeTorque = decelerationConstant;
            rearLeftWheelCollider.brakeTorque = decelerationConstant;
        }
        if(Input.GetKey("space") == true && Input.GetButton(VERTICAL) == false){
            frontRightWheelCollider.brakeTorque = currentbrakeForce;
            frontLeftWheelCollider.brakeTorque = currentbrakeForce;
            rearRightWheelCollider.brakeTorque = currentbrakeForce;
            rearLeftWheelCollider.brakeTorque = currentbrakeForce;
        }
    }
}

