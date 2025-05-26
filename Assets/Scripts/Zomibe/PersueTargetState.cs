using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersueTargetState : State
{
    AttackState attackState;

    private void Awake()
    {
        attackState = GetComponent<AttackState>();
    }

    public override State StateTick(ZombieManager zombieManager)
    {
        Debug.Log("PersueTargetState");
        if (zombieManager.distanceFromCurrentTarget <= zombieManager.minAttackDistance)
        {
            return attackState;
        }
        else
        {
            MoveTowardsCurrentTarget(zombieManager);
            RotateTowardsTarget(zombieManager);
            return this;
        }
    }

    private void MoveTowardsCurrentTarget(ZombieManager zombieManager)
    {
        zombieManager.animator.SetFloat("vertical", 1, 0.2f, Time.deltaTime);
    }

    private void RotateTowardsTarget(ZombieManager zombieManager)
    {
        // zombieManager.zombieNevmeshAgent.enabled = true;
        // zombieManager.zombieNevmeshAgent.SetDestination(zombieManager.currentTarget.transform.position);
        // zombieManager.transform.rotation = Quaternion.Slerp(zombieManager.transform.rotation,
        //     zombieManager.zombieNevmeshAgent.transform.rotation,
        //     zombieManager.zombieRotationSpeed / Time.deltaTime);
        
        Vector3 targetDirection = zombieManager.currentTarget.transform.position - transform.position;
        zombieManager.transform.rotation = Quaternion.LookRotation(targetDirection);

    }
}
