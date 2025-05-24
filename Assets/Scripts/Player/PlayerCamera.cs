using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    InputManager inputManager;

    public Transform cameraPivot;
    public Camera cameraObject;
    public GameObject player;

    Vector3 targetPosition;
    Vector3 cameraFollowVelocity = Vector3.zero;
    Vector3 cameraRotation;
    Quaternion targetRotation;
    float smoothTime = 0.2f;

    float lookAmountVertical;
    float lookAmounHorizontal;
    float maximumPivoyAngle = 15f;
    float minimumPivotAngle = -15f;

    private void Awake()
    {
        inputManager = player.GetComponent<InputManager>();
    }

    public void HandleAllCameraMovement()
    {
        FollowPlayer();
        RotateCamera();
    }

    private void FollowPlayer()
    {
        targetPosition = Vector3.SmoothDamp(transform.position, player.transform.position, ref cameraFollowVelocity, smoothTime * Time.deltaTime);
        transform.position = targetPosition;

    }

    private void RotateCamera()
    {
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
