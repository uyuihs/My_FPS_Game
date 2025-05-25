using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersueTargetState : State
{
    public override State StateTick()
    {
        Debug.Log("PersueTargetState");
        return this;
    }
}
