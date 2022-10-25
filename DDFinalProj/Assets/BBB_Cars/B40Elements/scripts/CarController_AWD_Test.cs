    using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController_AWD_Test : MonoBehaviour
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
    private float minGearSpeed;
    private float maxGearSpeed;
    public GameObject backfire; 
    public GameObject backfireClone;

    private int prevGear;
    public int currentGear;
    private void Start()
    {   
    }

    private void Update()
    {
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
        // Backfire();
    }

    private void HandleMotor()
    {
        car = GetComponent<Rigidbody>();
        
        // limit the top speed 
        currentSpeed = car.velocity.magnitude;
        if(currentSpeed < maxSpeed){
            frontLeftWheelCollider.motorTorque = verticalInput * motorforce;
            frontRightWheelCollider.motorTorque = verticalInput * motorforce;
            //awd bit
            rearLeftWheelCollider.motorTorque = verticalInput * motorforce;
            rearRightWheelCollider.motorTorque = verticalInput * motorforce;

        }
        else{
            frontLeftWheelCollider.motorTorque = 0;
            frontRightWheelCollider.motorTorque = 0;
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
        currentSpeed = car.velocity.sqrMagnitude;
    }
    private void PlayAudio() //FIXME: Making shifting sounds
    {
        minGearSpeed = 0.0f; 
        maxGearSpeed = gearing[0];
        audioSource = GetComponent<AudioSource>();
        for(int i = 0; i <gearing.Length; i++){
            if (gearing[i] > currentSpeed){
//                Backfire();
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
        Debug.Log("upper limit" + maxGearSpeed);
        Debug.Log("lower limit" + minGearSpeed);
        float enginePitch = (((currentSpeed - minGearSpeed))/(maxGearSpeed - minGearSpeed))*audioCoefficient;
        audioSource.pitch = enginePitch;
    }

    // private void Backfire()
    // {
    //     if (prevGear < currentGear){
    //         backfireClone = Instantiate(backfire, car.position, Quaternion.identity);
    //         Debug.Log("Backfire!");
    //         Destroy(backfireClone, 1);
    //     }
    // }
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

