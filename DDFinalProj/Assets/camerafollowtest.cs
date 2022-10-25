using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerafollowtest : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform target;
    [SerializeField] private float translateSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Rigidbody car;


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
