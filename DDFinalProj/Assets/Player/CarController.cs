using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    // follows the tutorial from https://www.youtube.com/watch?v=Z4HA8zJhGEk. Credit to 
    // GameDevChef

    // getting variables set up for speed 

    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizontalInput;
    private float verticalInput;
    private float currentsteerAngle;
    private float currentbrakeForce;
    private bool isBraking;

    public float currentSpeed;
    [SerializeField] private float motorforce;
    [SerializeField] private float brakeForce;
    [SerializeField] private float maxSteerAngle;

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
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorforce;
        frontRightWheelCollider.motorTorque = verticalInput * motorforce;
        // what if we made the car AWD
        // rearRightWheelCollider.motorTorque = verticalInput * motorforce;
        // rearLeftWheelCollider.motorTorque = verticalInput * motorforce; 
        currentbrakeForce = isBraking ? brakeForce : 0f;
        ApplyBraking();
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
    private void PlayAudio()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = currentSpeed / 15;

    }
}

