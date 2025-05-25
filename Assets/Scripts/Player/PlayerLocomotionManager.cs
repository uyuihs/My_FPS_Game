using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLocomotionManager : MonoBehaviour
{
    InputManager inputManager;

    public Rigidbody playerRigidBody;

    PlayerManager playerManager;

    [Header("Camera Transform")]
    public Transform cameraHolderTransform;

    [Header("Movement Speed")]
    public float rotationSpeed = 3.5f;

    [Header("Rotation Varaibles")]
    Quaternion targetRotation;
    Quaternion playerRotation;


    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerRigidBody = GetComponent<Rigidbody>();
        playerManager = GetComponent<PlayerManager>();
    }

    public void HandleAllLocomotions()
    {
        HandleRotation();
    }

    private void HandleRotation()
    {
        if (playerManager.isAiming)
        {
            targetRotation = Quaternion.Euler(0, cameraHolderTransform.eulerAngles.y, 0);
            playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = playerRotation;
        }
        else
        {
            targetRotation = Quaternion.Euler(0, cameraHolderTransform.eulerAngles.y, 0);
            playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            if (inputManager.horizontalCameraInput != 0 || inputManager.verticalCameraInput != 0)
            {
                transform.rotation = playerRotation;
            }

            if (playerManager.isPerformingQuickTurn)
            {
                Debug.Log("QuickTurn");
                playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                transform.rotation = playerRotation;
            }
        }
    }
}
