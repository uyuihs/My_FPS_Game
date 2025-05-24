using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    PlayerCamera playerCamera;
    InputManager inputManager;
    PlayerLocomotionManager playerLocomotionManager;
    private void Awake()
    {
        playerCamera = FindAnyObjectByType<PlayerCamera>();
        inputManager = GetComponent<InputManager>();
        playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
    }

    private void Update()
    {
        inputManager.HandleAllInputs();
        playerLocomotionManager.HandleAllLocomotions();
    }

    private void LateUpdate()
    {
        playerCamera.HandleAllCameraMovement();

    }

 
}
