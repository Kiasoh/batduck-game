using UnityEngine;
/// <summary>
/// batman state
/// </summary>
public enum BatmanState
{
    Normal,
    Stealth,
    Alert
}
/// <summary>
/// batman controller script for movement and rotation.
/// </summary>
public class BatmanController : MonoBehaviour
{
    [Header("Stealth/Alert FX")]
    public Light redLight;
    public Light blueLight;
    public AudioSource alarmSource;
    private float flashTimer = 0f;
    private bool isRedActive = false;
    private const float FLASH_RATE = 0.2f;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    public float rotationSpeed = 100f;

    public float boostMultiplier = 2f;
    public float stealthSpeedMultiplier = 0.5f;
    
    [Header("State Info")]
    public BatmanState currentState = BatmanState.Normal;
    private bool isControlActive = true;

    private float currentSpeed;
    void Start()
    {
        if (alarmSource != null)
        {
            alarmSource.Stop();
            alarmSource.loop = true; 
        }
        SetAlertLights(false);
        SetNormalLights(true);
    }

    void Update()
    {
        if (!isControlActive) return;
        HandleInputForStates();
        HandleMovement();
        if (currentState == BatmanState.Alert)
        {
            HandleAlertFlashingLights();
        }
    }
    
    /// <summary>
    /// Handles input for changing states.
    /// </summary>
    private void HandleInputForStates()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeState(BatmanState.Stealth);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeState(BatmanState.Alert);
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            ChangeState(BatmanState.Normal);
        }
    }

    /// <summary>
    /// Changes the current state of Batman.
    /// </summary>
    /// <param name="newState">new state</param>
    private void ChangeState(BatmanState newState)
    {
        currentState = newState;
        Debug.Log($"State Changed to: {currentState}");
        switch (currentState)
        {
            case BatmanState.Normal:
                SetAlertLights(false); 
                StopAlarm();
                break;

            case BatmanState.Stealth:
                SetAlertLights(false); 
                StopAlarm();
                break;

            case BatmanState.Alert:
                SetAlertLights(true);
                PlayAlarm();
                break;
        }

    }
    /// <summary>
    /// reads input and handles character movement and rotation.
    /// </summary>
    private void HandleMovement()
    {
        float moveInput = Input.GetAxis("Vertical");   
        float turnInput = Input.GetAxis("Horizontal"); 

        CalculateSpeed();

        Vector3 movement = Vector3.forward * moveInput * currentSpeed * Time.deltaTime;
        transform.Translate(movement);

        float rotation = turnInput * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, rotation, 0);
    }

    /// <summary>
    /// calculates current speed based on state and input.
    /// </summary>
    private void CalculateSpeed()
    {
        switch (currentState)
        {
            case BatmanState.Normal:
            case BatmanState.Alert:
                if (Input.GetKey(KeyCode.LeftShift))
                    currentSpeed = moveSpeed * boostMultiplier;
                else
                    currentSpeed = moveSpeed;
                break;

            case BatmanState.Stealth:
                currentSpeed = moveSpeed * stealthSpeedMultiplier;
                break;
        }
    }

    /// <summary>
    /// handles the flashing lights during alert state
    /// </summary>
    private void HandleAlertFlashingLights()
    {
        flashTimer += Time.deltaTime;

        if (flashTimer >= FLASH_RATE)
        {
            // Toggle the active light
            isRedActive = !isRedActive;

            // Turn off the previous light and turn on the new one
            redLight.enabled = isRedActive;
            blueLight.enabled = !isRedActive;

            flashTimer = 0f; // Reset timer
        }
    }

    /// <summary>
    /// sets the alert lights
    /// </summary>
    private void SetAlertLights(bool enable)
    {
        if (redLight != null) redLight.enabled = enable && isRedActive;
        if (blueLight != null) blueLight.enabled = enable && !isRedActive;
        // If turned off, make sure to reset the timer as well
        if (!enable)
        {
            flashTimer = 0f;
            isRedActive = false;
        }
    }
    
    /// <summary>
    /// sets the normal lights
    /// </summary>
    private void SetNormalLights(bool enable)
    {
        // foreach (Light light in normalLights)
        // {
        //     if (light != null)
        //     {
        //         light.enabled = enable;
        //     }
        // }
    }

    /// <summary>
    /// plays the alarm sound
    /// </summary>
    private void PlayAlarm()
    {
        if (alarmSource != null && !alarmSource.isPlaying)
        {
            alarmSource.Play();
        }
    }

    /// <summary>
    /// stops the alarm sound
    /// </summary>
    private void StopAlarm()
    {
        if (alarmSource != null && alarmSource.isPlaying)
        {
            alarmSource.Stop();
        }
    }
    /// <summary>
    /// Handles control activation
    /// </summary>
    public void SetControlActive(bool isActive)
    {
        isControlActive = isActive;
    }
}