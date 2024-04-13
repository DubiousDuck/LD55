using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : EnemyAI
{
    private Vector3 size;
    private float sizeBuffer = 0.01f;   //buffer in all directions

    public override void Start()
    {
        size = this.GetComponent<Collider>().bounds.size * (1 + 2 * this.sizeBuffer);
        base.Start();
    }

    public override void moveToTarget(Vector3 pos)
    {
        Vector3 diff = targetPos - this.transform.position;
        Vector3 prlToFloor = Vector3.Project(diff, -transform.up);
        Vector3 perpToFloor = diff - prlToFloor;
        Vector3 floorPos = this.transform.position - transform.up * this.size.y/2;

        //check move in perp direction
        Vector3 newPos = this.transform.position + perpToFloor.normalized * moveSpeed;
        if(true)
        
            newPos = this.transform.position + perpToFloor;


        //if not grounded at that point, check if wall up or down in direction of enemy
        //rotate if so
        //if not then move backwards to make sure still grounded on wall

        if (diff.magnitude > moveSpeed)
            diff = Vector3.Normalize(diff) * moveSpeed;
        rb.velocity = diff;
    }


}