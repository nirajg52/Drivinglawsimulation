using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{

    [SerializeField] WheelCollider frontRightCollider;
    [SerializeField] WheelCollider backRightCollider;
    [SerializeField] WheelCollider frontLeftCollider;
    [SerializeField] WheelCollider backLeftCollider;

    [SerializeField] private Transform frontRightTransform;
    [SerializeField] private Transform frontLeftTransform;
    [SerializeField] private Transform backRightTransform;
    [SerializeField] private Transform backLeftTransform;

    public float acceleration = 500f;
    public float breakingForce = 300f;
    public float maxTurnAngle = 15f;


    private float currentAcceleration = 0f;
    private float currentBrakeForce = 0f;

    private float currentTurnAngle = 0f;
    // Start is called before the first frame update
   

    // Update is called once per frame
    private void FixedUpdate()
    {

        currentAcceleration = acceleration * Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.Space))
        {
            currentBrakeForce = breakingForce;
        }

        else
        {
            currentBrakeForce = 0f;
        }

        frontRightCollider.motorTorque = currentAcceleration;
        backRightCollider.motorTorque = currentAcceleration;
        frontLeftCollider.motorTorque = currentAcceleration;
        backLeftCollider.motorTorque = currentAcceleration;

        frontRightCollider.brakeTorque = currentBrakeForce;
        frontLeftCollider.brakeTorque = currentBrakeForce;
        backLeftCollider.brakeTorque = currentBrakeForce;
        backRightCollider.brakeTorque = currentBrakeForce;

        currentTurnAngle = maxTurnAngle * Input.GetAxis("Horizontal");
        frontLeftCollider.steerAngle = currentTurnAngle;
        frontRightCollider.steerAngle = currentTurnAngle;

        UpdateWheel(frontRightCollider,frontRightTransform);

        UpdateWheel(frontLeftCollider, frontLeftTransform);

        UpdateWheel(backRightCollider, backRightTransform);

        UpdateWheel(backLeftCollider, backLeftTransform);

    }

    void UpdateWheel(WheelCollider col, Transform transform)
    {

        Vector3 position;
        //Quaternion rotation;

        //col.GetWorldPose(out position, out rotation);

        //transform.position = position;
        //transform.rotation = rotation;
    }
}
