using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    InputManager inputManager;
    PlayerManager playerManager;

    public Transform cameraPivot;
    public Camera cameraObject;

    [Header("Camera Follow Target")]
    public GameObject player;
    public Transform aimedCameraPosition;//瞄准的相机跟随位置
    public Transform aimedTarget;//瞄准点

    Vector3 targetPosition;
    Vector3 cameraFollowVelocity = Vector3.zero;
    Vector3 cameraRotation;
    Quaternion targetRotation;
    float smoothTime = 0.2f;
    float aimSmoothTime = 3f;

    float lookAmountVertical;
    float lookAmounHorizontal;
    float maximumPivoyAngle = 15f;
    float minimumPivotAngle = -15f;
    PlayerUIManager playerUIManager;

    private void Awake()
    {
        inputManager = player.GetComponent<InputManager>();
        playerManager = player.GetComponent<PlayerManager>();
        playerUIManager = FindObjectOfType<PlayerUIManager>();
    }

    public void HandleAllCameraMovement()
    {
        FollowPlayer();
        RotateCamera();
    }

    private void FollowPlayer()
    {
        if (playerManager.isAiming)
        {
            targetPosition = Vector3.SmoothDamp(transform.position, aimedCameraPosition.transform.position, ref cameraFollowVelocity, smoothTime * Time.deltaTime);
            transform.position = targetPosition;
        }
        else
        {
            targetPosition = Vector3.SmoothDamp(transform.position, player.transform.position, ref cameraFollowVelocity, smoothTime * Time.deltaTime);
            transform.position = targetPosition;
        }
    }

    private void RotateCamera()
    {
        if (playerManager.isAiming)
        {
            cameraObject.transform.localRotation = Quaternion.Euler(0, 0, 0);

            lookAmountVertical += inputManager.horizontalCameraInput;
            lookAmounHorizontal -= inputManager.verticalCameraInput;
            lookAmounHorizontal = Mathf.Clamp(lookAmounHorizontal, minimumPivotAngle, maximumPivoyAngle);

            cameraRotation = Vector3.zero;
            cameraRotation.y = lookAmountVertical;
            targetRotation = Quaternion.Euler(cameraRotation);
            targetRotation = Quaternion.Slerp(transform.rotation, targetRotation, aimSmoothTime);
            transform.rotation = targetRotation;

            cameraRotation = Vector2.zero;
            cameraRotation.x = lookAmounHorizontal;
            targetRotation = Quaternion.Euler(cameraRotation);
            targetRotation = Quaternion.Slerp(cameraPivot.localRotation, targetRotation, aimSmoothTime);
            cameraObject.transform.localRotation = targetRotation;  
        }
        else
        {
            cameraObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            lookAmountVertical += inputManager.horizontalCameraInput;
            lookAmounHorizontal -= inputManager.verticalCameraInput;
            lookAmounHorizontal = Mathf.Clamp(lookAmounHorizontal, minimumPivotAngle, maximumPivoyAngle);

            cameraRotation = Vector3.zero;
            cameraRotation.y = lookAmountVertical;
            targetRotation = Quaternion.Euler(cameraRotation);
            targetRotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothTime);
            transform.rotation = targetRotation;

            if (inputManager.quickTurnInput)
            {
                inputManager.quickTurnInput = false;
                lookAmountVertical = lookAmountVertical + 180;
                cameraRotation.y = cameraRotation.y + 180;
                transform.rotation = targetRotation;
            }

            cameraRotation = Vector2.zero;
            cameraRotation.x = lookAmounHorizontal;
            targetRotation = Quaternion.Euler(cameraRotation);
            targetRotation = Quaternion.Slerp(cameraPivot.localRotation, targetRotation, smoothTime);
            cameraPivot.localRotation = targetRotation;    
        }
        
    }
}
