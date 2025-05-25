using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{

    PersueTargetState persueTargetState;

    [SerializeField] bool hasTargetState;

    private void Awake()
    {
        persueTargetState = GetComponent<PersueTargetState>();   
    }

    public override State StateTick()
    {
        if (hasTargetState)
        {
            Debug.Log("Found Target");
            return persueTargetState;
        }
        else
        {
            return this;
        }

    }
}
