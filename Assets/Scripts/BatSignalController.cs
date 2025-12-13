using UnityEngine;

/// <summary>
/// controller for the Bat-Signal light.
/// Allows toggling on/off with the B key and automatic rotation.
/// </summary>
public class BatSignalController : MonoBehaviour
{
    [Header("Settings")]
    public Light signalLight;

    public float rotateSpeed = 5f;
    public float maxRotationAngle = 15f; 

    private bool isSignalOn = false;
    private float initialYRotation;
    private int rotationDirection = 1; // 1 for right, -1 for left

    void Start()
    {
        initialYRotation = transform.localEulerAngles.y;
        signalLight.enabled = isSignalOn;
    }

    void Update()
    {
        HandleInput();
        HandleRotation();
    }

    /// <summary>
    /// gets input to toggle the signal
    /// </summary>
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleSignal();
        }
    }

    /// <summary>
    /// toggles the Bat-Signal light on or off
    /// </summary>
    private void ToggleSignal()
    {
        isSignalOn = !isSignalOn;
        
        if (signalLight != null)
        {
            signalLight.enabled = isSignalOn;
        }
    }

    /// <summary>
    /// handles the rotation of the Bat-Signal when it is on
    /// </summary>
    private void HandleRotation()
    {
        
        // Rotate around the Y-axis (vertical) locally within bounds
        if (isSignalOn)
        {
            float currentY = transform.localEulerAngles.y;
            float delta = Mathf.DeltaAngle(initialYRotation, currentY);

            // Reverse direction if out of bounds
            if (Mathf.Abs(delta) >= maxRotationAngle)
            {
                rotationDirection *= -1;
            }
            // Only rotate in the current direction
            transform.Rotate(Vector3.up * rotateSpeed * rotationDirection * Time.deltaTime);
        }
    }
}