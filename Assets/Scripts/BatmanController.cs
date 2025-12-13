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
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    public float rotationSpeed = 100f;

    public float boostMultiplier = 2f;
    public float stealthSpeedMultiplier = 0.5f;
    
    [Header("State Info")]
    public BatmanState currentState = BatmanState.Normal;

    private float currentSpeed;

    void Update()
    {
        HandleInputForStates();
        HandleMovement();
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
}