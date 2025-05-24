using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    InputController playerControls;
    PlayerAnimationManager animationManager;

    [Header("Player Movement")]
    public float horizontalMovementInput;
    public float verticalMovementInput;
    public Vector2 movementInput;

    [Header("Camera Rotation")]
    public float verticalCameraInput;
    public float horizontalCameraInput;
    private Vector2 cameraInput;

    private void Awake()
    {
        animationManager = GetComponent<PlayerAnimationManager>();
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new InputController();
            playerControls.PlayerMove.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
            playerControls.PlayerMove.Camera.performed += ctx => cameraInput = ctx.ReadValue<Vector2>();
        }

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void HandleAllInputs()
    {
        HandleCameraInput();
        HandleMovementInput();
    }

    private void HandleMovementInput()
    {
        horizontalMovementInput = movementInput.x;
        verticalMovementInput = movementInput.y;
        animationManager.HandleAnimatorValues(horizontalMovementInput, verticalMovementInput);
    }

    private void HandleCameraInput()
    {
        horizontalCameraInput = cameraInput.x;
        verticalCameraInput = cameraInput.y;
    }

}
