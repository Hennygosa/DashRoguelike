using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretStateIdle : TurretState
{
    public override void Enter(TurretScript parent)
    {
        base.Enter(parent);
        parent.Animator.SetBool("Shoot", false);
    }

    public override void Update()
    {
        if (parent.DefaultRotation != parent.Rotaror.rotation)
        {
            parent.Rotaror.rotation = Quaternion.RotateTowards(parent.Rotaror.rotation, parent.DefaultRotation,
                Time.deltaTime * parent.RotationSpeed);
        }
        if (parent.Target != null)
        {
            if (parent.SeeChecker(((parent.Target.position) - parent.GunBarrel.position), parent.GunBarrel.position, "player"))
            {
                parent.ChangeState(new TurretStateFindTarget());
            }
        }
    }

    public override void OnTriggerEnter(Collider col)
    {
        {
            if (col.tag == "player")
            {
                parent.Target = col.transform;
                parent.ChangeState(new TurretStateFindTarget());
            }
        }
    }
}
