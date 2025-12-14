using UnityEngine;

public class BatmobileController : MonoBehaviour
{
    [Header("Wheel Colliders")]
    public WheelCollider frontLeftWheel;
    public WheelCollider frontRightWheel;
    public WheelCollider rearLeftWheel;
    public WheelCollider rearRightWheel;

    [Header("Car Stats")]
    public float motorForce = 1000f;
    public float brakeForce = 300f; 
    public float maxSteerAngle = 60f;  

    private float currentMotorForce;
    private float currentSteerAngle;
    private float currentBrakeForce;

    [Header("Drift Settings")]
    public float driftSidewaysStiffness = 0.5f; 
    public float highGripSidewaysStiffness = 1.0f; 

    void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        HandleDrift();
    }

    /// <summary>
    /// تنظیم اصطکاک چرخ‌های عقب برای دریف یا گریپ بالا
    /// </summary>
    private void HandleDrift()
    {
        WheelFrictionCurve frictionL = rearLeftWheel.sidewaysFriction;
        WheelFrictionCurve frictionR = rearRightWheel.sidewaysFriction;

        float targetStiffness;

        if (Input.GetKey(KeyCode.LeftShift) && (Input.GetAxis("Horizontal") != 0))
        {
            targetStiffness = driftSidewaysStiffness;
        }
        else
        {
            targetStiffness = highGripSidewaysStiffness;
        }

        frictionL.stiffness = Mathf.Lerp(frictionL.stiffness, targetStiffness, Time.deltaTime * 5f);
        frictionR.stiffness = Mathf.Lerp(frictionR.stiffness, targetStiffness, Time.deltaTime * 5f);

        rearLeftWheel.sidewaysFriction = frictionL;
        rearRightWheel.sidewaysFriction = frictionR;
    }
    private void GetInput()
    {
        float acceleration = Input.GetAxis("Vertical");
        currentMotorForce = acceleration * motorForce;

        currentSteerAngle = Input.GetAxis("Horizontal") * maxSteerAngle;

        currentBrakeForce = Input.GetKey(KeyCode.Space) ? brakeForce : 0f;
    }

    private void HandleMotor()
    {
        rearLeftWheel.motorTorque = currentMotorForce;
        rearRightWheel.motorTorque = currentMotorForce;

        ApplyBraking();
    }

    // private void ApplyBraking()
    // {
    //     rearLeftWheel.brakeTorque = currentBrakeForce;
    //     rearRightWheel.brakeTorque = currentBrakeForce;
    //     frontLeftWheel.brakeTorque = currentBrakeForce;
    //     frontRightWheel.brakeTorque = currentBrakeForce;

    //     if (Input.GetKey(KeyCode.LeftShift))
    //     {
    //         rearLeftWheel.brakeTorque = brakeForce * 2;
    //         rearRightWheel.brakeTorque = brakeForce * 2;
    //     }
    // }
    private void ApplyBraking()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rearLeftWheel.motorTorque = 0f;
            rearRightWheel.motorTorque = 0f;

            rearLeftWheel.brakeTorque = brakeForce * 2;
            rearRightWheel.brakeTorque = brakeForce * 2;

            frontLeftWheel.brakeTorque = 0f;
            frontRightWheel.brakeTorque = 0f;

            // Reset currentBrakeForce to prevent applying it twice later
            currentBrakeForce = 0f; 

        }
        else
        {
            rearLeftWheel.brakeTorque = currentBrakeForce;
            rearRightWheel.brakeTorque = currentBrakeForce;
            frontLeftWheel.brakeTorque = currentBrakeForce;
            frontRightWheel.brakeTorque = currentBrakeForce;
        }
    }

    private void HandleSteering()
    {
        frontLeftWheel.steerAngle = currentSteerAngle;
        frontRightWheel.steerAngle = currentSteerAngle;
    }
}
