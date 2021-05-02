using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShootState : TurretState
{

    public override void Enter(TurretScript parent)
    {
        base.Enter(parent);
        parent.Animator.SetBool("Shoot", true);
    }

    public override void Update()
    {
        if (parent.Target != null)
        {
            Debug.Log("strelba");
            parent.Rotaror.LookAt(parent.Target.position);
        }
        if (!parent.SeeChecker(parent.GunBarrel.forward, parent.Rotaror.position, "player"))
        {
            parent.ChangeState(new TurretStateIdle());
        }
    }
    public override void OnTriggerExit(Collider col)
    {
        if (col.tag == "player")
        {
            parent.Target = null;
            parent.ChangeState(new TurretStateIdle());
        }
    }
}
