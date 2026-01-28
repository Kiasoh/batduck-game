using UnityEngine;

/// <summary>
/// Controller script for mouse look (vertical rotation) in POV camera.
/// </summary>
public class MouseLookController : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    public float minPitch = -90f;
    public float maxPitch = 90f;

    private float xRotation = 0f;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (GetComponent<Camera>().enabled)
        {
            HandleMouseYInput();
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    /// <summary>
    /// Handles mouse input for vertical rotation (pitch).
    /// </summary>
    private void HandleMouseYInput()
    {
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minPitch, maxPitch);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}