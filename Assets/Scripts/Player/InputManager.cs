using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    InputController playerControls;
    PlayerAnimationManager animationManager;
    PlayerManager playerManager;
    Animator animator;


    [Header("Player Movement")]
    public float horizontalMovementInput;
    public float verticalMovementInput;
    public Vector2 movementInput;

    [Header("Camera Rotation")]
    public float verticalCameraInput;
    public float horizontalCameraInput;
    private Vector2 cameraInput;

    [Header("Button Inputs")]
    public bool runInput;
    public bool quickTurnInput;

    private void Awake()
    {
        animationManager = GetComponent<PlayerAnimationManager>();
        playerManager = GetComponent<PlayerManager>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new InputController();
            playerControls.PlayerMove.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
            playerControls.PlayerMove.Camera.performed += ctx => cameraInput = ctx.ReadValue<Vector2>();
            playerControls.PlayerMove.Run.performed += ctx => runInput = true;
            playerControls.PlayerMove.Run.canceled += ctx => runInput = false;
            playerControls.PlayerMove.Quickturn.performed += ctx => quickTurnInput = true;

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
        HandleQuickTurnInput();
    }

    private void HandleMovementInput()
    {
        horizontalMovementInput = movementInput.x;
        verticalMovementInput = movementInput.y;
        animationManager.HandleAnimatorValues(horizontalMovementInput, verticalMovementInput, runInput);
    }

    private void HandleCameraInput()
    {
        horizontalCameraInput = cameraInput.x;
        verticalCameraInput = cameraInput.y;
    }

    private void HandleQuickTurnInput()
    {
        if (playerManager.isPerformingActions) { return; }
        if (quickTurnInput)
        {
            animator.SetBool("isPerformingQuickTurn", true);
            animationManager.PlayAnimationWithoutRootMotion("QuickTurn", true);
        }
    }

}
