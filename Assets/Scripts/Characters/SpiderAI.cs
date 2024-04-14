using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAI : EnemyAI
{
    public override void moveToTarget(Vector3 pos)
    {
        Vector3 diff = targetPos - this.transform.position;
        Vector3 prlToFloor = Vector3.Project(diff, -transform.up);
        Vector3 perpToFloor = diff - prlToFloor;
        Vector3 floorPos = this.transform.position - transform.up * this.size.y/2;

        //maximum move in perp direction
        //check front if can move
        //else check back if can move
        Vector3 newPos = this.transform.position + perpToFloor.normalized * moveSpeed;
        //if(Physics.OverlapSphere(newPos, sizeBuffer))
        
            newPos = this.transform.position + perpToFloor;


        //if not grounded at that point, check if wall up or down in direction of enemy
        //rotate if so
        //if not then move backwards to make sure still grounded on wall

        if (diff.magnitude > moveSpeed)
            diff = Vector3.Normalize(diff) * moveSpeed;
        rb.velocity = diff;
    }


}