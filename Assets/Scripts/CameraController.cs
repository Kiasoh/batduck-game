using UnityEngine;

/// <summary>
/// camera controller script for switching between main and batman cameras.
/// </summary>
public class CameraController : MonoBehaviour
{
    [Header("Cameras")]
    public Camera[] cameras;
    private int currentCameraIndex = 0;
    void Start()
    {
        // SwitchToMainCamera();
        currentCameraIndex = 0;
        cameras[currentCameraIndex].enabled = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            SwitchToNextCamera();
        }
    }

    void SwitchToNextCamera()
    {
        cameras[currentCameraIndex].enabled = false;
        currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;
        cameras[currentCameraIndex].enabled = true;
    }
}
