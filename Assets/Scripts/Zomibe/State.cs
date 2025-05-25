using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    //所有状态的基状态，所有状态继承于此
    public virtual State StateTick()
    {
        return this;
    }
}
