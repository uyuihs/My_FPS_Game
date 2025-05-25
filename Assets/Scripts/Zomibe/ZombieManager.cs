using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{

    public IdleState startingState;
    [SerializeField] private State currentState;

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
            nextState = currentState.StateTick();
            currentState = currentState.StateTick() == null ? currentState : nextState;
        }
    }

    private void FixedUpdate()
    {
        HandleStateMachine();
    }
}
