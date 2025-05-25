using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{

    public IdleState startingState;

    [Header("Current State")]
    [SerializeField] private State currentState;

    [Header("Current Target")]
    public PlayerManager currentTarget;
    
    private void Awake()
    {
        currentState = startingState;
    }

    //僵尸状态机
    private void HandleStateMachine()
    {
        State nextState;
        if (currentState != null)
        {
            nextState = currentState.StateTick(this);
            currentState = currentState.StateTick(this) == null ? currentState : nextState;
        }
    }

    private void FixedUpdate()
    {
        HandleStateMachine();
    }
}
