using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieManager : MonoBehaviour
{

    public IdleState startingState;

    [Header("Current State")]
    [SerializeField] private State currentState;

    [Header("Current Target")]
    public PlayerManager currentTarget;
    public float distanceFromCurrentTarget;


    [Header("Animator")]
    public Animator animator;

    [Header("Nevmesh Agent")]
    public NavMeshAgent zombieNevmeshAgent;


    [Header("Rigidbody")]
    public Rigidbody zombieRigidbody;

    [Header("RotationSpeed")]
    public float zombieRotationSpeed = 5;

    [Header("AttackDist")]
    public float minAttackDistance = 1;

    private void Awake()
    {
        currentState = startingState;

        zombieNevmeshAgent = GetComponentInChildren<NavMeshAgent>();
        animator = GetComponent<Animator>();
        zombieRigidbody = GetComponent<Rigidbody>();
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

    private void Update()
    {
        zombieNevmeshAgent.transform.localPosition = Vector3.zero;

        if (currentTarget != null)
        {
            distanceFromCurrentTarget = Vector3.Distance(currentTarget.transform.position, transform.position);
        }
    }
}
