using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerAnimationManager : MonoBehaviour
{
    public Animator animator;
    PlayerLocomotionManager playerLocomotionManager;
    PlayerManager playerManager;
    RigBuilder rigBuilder;

    [Header("Hand IK Contstrants")]
    public TwoBoneIKConstraint rightHandIK;
    public TwoBoneIKConstraint leftHandIK;

    [Header("Aiming Constrants")]
    public Rig aimRig;

    private int xMovHash;
    private int zMovHash;
    private int isPerformingActionsHash;
    private int disableRootmotionHash;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerManager = GetComponent<PlayerManager>();
        playerLocomotionManager = GetComponent<PlayerLocomotionManager>();

    }

    private void Start()
    {
        xMovHash = Animator.StringToHash("xMov");
        zMovHash = Animator.StringToHash("zMov");
        isPerformingActionsHash = Animator.StringToHash("isPerformingActions");
        disableRootmotionHash = Animator.StringToHash("disableRootmotion");
    }

    public void HandleAnimatorValues(float xMov, float zMov, bool isRunning)
    {
        if (xMov > 0) { xMov = 1; }
        else if (xMov < 0) { xMov = -1; }
        else { xMov = 0; }

        if (zMov > 0) { zMov = 1; }
        else if (zMov < 0) { zMov = -1; }
        else { zMov = 0; }

        if (isRunning && zMov > 0)
        {
            zMov = 2;
            xMov = 0;
        }

        animator.SetFloat(xMovHash, xMov, 0.1f, Time.deltaTime);
        animator.SetFloat(zMovHash, zMov, 0.1f, Time.deltaTime);
    }

    public void PlayAnimationWithoutRootMotion(string targetAnimation, bool isPerformingActions)
    {
        animator.SetBool(disableRootmotionHash, true);
        animator.SetBool(isPerformingActionsHash, isPerformingActions);
        animator.applyRootMotion = false;
        animator.CrossFade(targetAnimation, 0.2f);
    }

    public void AssignHandIK(RightHandTargetIK rightIK, LeftHandTargetIK leftIK)
    {
        rightHandIK.data.target = rightIK.transform;
        leftHandIK.data.target = leftIK.transform;
        rigBuilder.Build();
    }

    //当且仅当角色瞄准时，才会开启瞄准ik
    public void UpdateAimConstraints()
    {
        if (playerManager.isAiming)
        {
            aimRig.weight = 1;
        }
        else
        {
            aimRig.weight = 0;
        }
    }

    private void OnAnimatorMove()
    {
        if (playerManager.disableRootmotion) { return; }
        Vector3 animatorDeltaPosition = animator.deltaPosition;
        animatorDeltaPosition.y = 0;

        Vector3 velocity = animatorDeltaPosition / Time.deltaTime;
        playerLocomotionManager.playerRigidBody.drag = 0;
        playerLocomotionManager.playerRigidBody.velocity = velocity;
        transform.rotation *= animator.deltaRotation;
    }
}
