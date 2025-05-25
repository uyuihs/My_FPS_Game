using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    InputController playerControls;
    PlayerAnimationManager animationManager;
    PlayerManager playerManager;
    Animator animator;
    PlayerUIManager playerUIManager;

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
    public bool aimingInput;
    public bool shootInput;

    private void Awake()
    {
        animationManager = GetComponent<PlayerAnimationManager>();
        playerManager = GetComponent<PlayerManager>();
        animator = GetComponent<Animator>();
        //unique
        playerUIManager = FindObjectOfType<PlayerUIManager>();
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
            playerControls.PlayerActions.Aiming.performed += ctx => aimingInput = true;
            playerControls.PlayerActions.Aiming.canceled += ctx => aimingInput = false;
            playerControls.PlayerActions.Shooting.performed += i => shootInput = true;
            playerControls.PlayerActions.Shooting.canceled += i => shootInput = false;
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
        HandleAimingInput();
        HandleShootingInput();
    }

    private void HandleAimingInput()
    {
        if (verticalMovementInput != 0 || horizontalMovementInput != 0)
        {
            aimingInput = false;
            animator.SetBool("isAiming", false);
            playerUIManager.crossHair.SetActive(false);
            return;
        }
        if (aimingInput)
        {
            animator.SetBool("isAiming", true);
            playerUIManager.crossHair.SetActive(true);
        }
        else
        {
            animator.SetBool("isAiming", false);
            playerUIManager.crossHair.SetActive(false);
        }
        animationManager.UpdateAimConstraints();
    }

    private void HandleMovementInput()
    {
        horizontalMovementInput = movementInput.x;
        verticalMovementInput = movementInput.y;
        animationManager.HandleAnimatorValues(horizontalMovementInput, verticalMovementInput, runInput);

        //TODO 扩展IK使能
        if (horizontalMovementInput == 0 || verticalMovementInput == 0)
        {
            animationManager.rightHandIK.weight = 1;
            animationManager.leftHandIK.weight = 1;
        }
        else
        {
            animationManager.rightHandIK.weight = 0;
            animationManager.leftHandIK.weight = 0;
        }
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

    private void HandleShootingInput() {
        if (shootInput && aimingInput)
        {
            shootInput = false;
            Debug.Log("Shoot");
            playerManager.UseCurrentWeapon();
        }        
    }

}
