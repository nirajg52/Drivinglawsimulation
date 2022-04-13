using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    public TextMeshProUGUI SpeedTextMesh;

    public Rigidbody rBody;

    public BoxCollider CarCollider;
    public Vector3 respawnPoint = new Vector3(2.63f, 0f, 79.9f);

    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float currentbreakForce;
    private bool isBreaking;

    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteerAngle;

    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheeTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;


    //public AudioSource engineIdleAudio;

    public AudioSource engine;
    public float topSpeed = 100;
    public float currentSpeed = 0;
    public float pitch;

    public TextMeshProUGUI Hint; 



    private void Start()
    {
        rBody = GetComponent<Rigidbody>();

        engine = GetComponent<AudioSource>();

        CarCollider = GetComponent<BoxCollider>();

    }

    private void Update()
    {
        SpeedTextMesh.text = "Speed:" + (rBody.velocity.magnitude * 2.23).ToString("0") + ("m/h");
    }

    private void FixedUpdate()
    {
        ShowHint();
        GetInput();
        PlayAudio();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void PlayAudio()
    {
        currentSpeed = rBody.velocity.magnitude * 2.23f;
        pitch = Mathf.Lerp(0, 1, currentSpeed / topSpeed);
        engine.pitch = pitch * 0.5f;
    }


    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        currentbreakForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();       
    }

    private void ApplyBreaking()
    {
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheeTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot
;       wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }

    private void ShowHint()
    {
        if (Input.GetKey(KeyCode.H))
        {
            Hint.text = "1 Move as far right as possible, check traffic, and signal a left turn." + " 2.Turn the steering wheel sharply to the left and move forward slowly. ..." +
                        "3.Shift to reverse, turn your wheels sharply to the right, check traffic, and back your vehicle to the right curb, or edge of roadway.";
            
        }

        if (Input.GetKey(KeyCode.Alpha0))
        {
            Hint.text = "";
        }


       
    }
}
