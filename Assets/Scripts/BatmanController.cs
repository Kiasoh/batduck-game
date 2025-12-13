using UnityEngine;

/// <summary>
/// batman controller script for movement and rotation.
/// </summary>
public class BatmanController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    public float rotationSpeed = 100f;

    public float boostMultiplier = 2f;

    private float currentSpeed;

    void Update()
    {
        HandleMovement();
    }

    /// <summary>
    /// reads input and handles character movement and rotation.
    /// </summary>
    private void HandleMovement()
    {
        float moveInput = Input.GetAxis("Vertical");   
        float turnInput = Input.GetAxis("Horizontal"); 

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = moveSpeed * boostMultiplier;
        }
        else
        {
            currentSpeed = moveSpeed;
        }

        Vector3 movement = Vector3.forward * moveInput * currentSpeed * Time.deltaTime;
        transform.Translate(movement);

        float rotation = turnInput * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, rotation, 0);
    }
}