using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretStateFindTarget : TurretState
{
    public override void Update()
    {
        parent.GhostRotator.LookAt(parent.Target.position);

        if (parent.SeeChecker(parent.GunBarrel.forward, parent.Rotaror.position, "player"))
        {
            parent.Rotaror.rotation = Quaternion.RotateTowards(parent.Rotaror.rotation, parent.GhostRotator.rotation,
            Time.deltaTime * parent.RotationSpeed);
        }
        if (parent.GhostRotator.rotation.y == parent.Rotaror.rotation.y)
        {
            parent.ChangeState(new TurretShootState());
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
