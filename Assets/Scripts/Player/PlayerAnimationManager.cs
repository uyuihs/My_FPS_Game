using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    public Animator animator;
    private int xMovHash;
    private int zMovHash;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        xMovHash = Animator.StringToHash("xMov");
        zMovHash = Animator.StringToHash("zMov");
    }

    public void HandleAnimatorValues(float xMov, float zMov)
    {
        animator.SetFloat(xMovHash, xMov, 0.1f, Time.deltaTime);
        animator.SetFloat(zMovHash, zMov, 0.1f, Time.deltaTime);
    }
}
