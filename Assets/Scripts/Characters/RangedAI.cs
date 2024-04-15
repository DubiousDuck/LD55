using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAI : EnemyAI
{
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (attacking && (this.targetPos - this.transform.position).magnitude < attackRange)
            move(false);
    }

    public virtual void move(bool towards)
    {
    }
}
