using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    PlayerCamera playerCamera;
    InputManager inputManager;
    Animator animator;
    PlayerLocomotionManager playerLocomotionManager;
    PlayerEquipmentManager playerEquipmentManager;

    [Header("Player Flags")]
    public bool isPerformingActions;
    public bool isPerformingQuickTurn;
    public bool disableRootmotion;
    public bool isAiming;
    private void Awake()
    {
        playerCamera = FindAnyObjectByType<PlayerCamera>();
        inputManager = GetComponent<InputManager>();
        playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
        animator = GetComponent<Animator>();
        playerEquipmentManager = GetComponent<PlayerEquipmentManager>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        inputManager.HandleAllInputs();

        isPerformingActions = animator.GetBool("isPerformingActions");
        disableRootmotion = animator.GetBool("disableRootmotion");
        isPerformingQuickTurn = animator.GetBool("isPerformingQuickTurn");
        isAiming = animator.GetBool("isAiming");
    }

    private void FixedUpdate()
    {
        playerLocomotionManager.HandleAllLocomotions();
    }

    private void LateUpdate()
    {
        playerCamera.HandleAllCameraMovement();

    }

    public void  UseCurrentWeapon()
    {
        playerEquipmentManager.weaponAnimatorManager.ShootWeapon(playerCamera);
    }

 
}
