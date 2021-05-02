using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurretState
{
    protected TurretScript parent;

    public virtual void Enter(TurretScript parent)
    {
        this.parent = parent;
    }

    public virtual void Update()
    {

    }

    public virtual void Exit()
    {

    }

    public virtual void OnTriggerEnter(Collider col)
    {

    }

    public virtual void OnTriggerExit(Collider col)
    {

    }
}
