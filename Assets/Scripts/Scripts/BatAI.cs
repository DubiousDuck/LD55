using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAI : EnemyAI
{
    public new void lookForTarget()
    {
        Vector3 dir = Vector3.Normalize(target.transform.position - this.transform.position);
        Ray ray = new Ray(this.transform.position, dir);
        if (Physics.Raycast(ray, out RaycastHit hitData, maxDistance: detectionRange))
        {
            if (hitData.collider.tag == "Player")   //no wall between = update target position
                targetPos = target.transform.position;
            else if (inRange(targetPos, 0.1f))      // if wall between and already at target, wait in place
                targetPos = this.transform.position;
        }
    }
}
