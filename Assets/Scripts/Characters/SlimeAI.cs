using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class SlimeAI : EnemyAI
{
    private bool jumping = false;
    public float[] jumpForce = new float[] { 2, 5 };
    public override void moveToTarget()
    {
        if (!jumping && Physics.Raycast(transform.position, Vector3.down, this.size.y / 2))
        {
            float randForce = Random.Range(jumpForce[0], jumpForce[1]);
            Vector3 diff = this.targetPos - this.transform.position;
            Vector3 jumpVel = new Vector3(diff.x < 0 ? -1 : diff.x > 0 ? 1 : 0, Random.Range(0.5f, 2), 0);
            StartCoroutine(jump(jumpVel.normalized * Random.Range(jumpForce[0], jumpForce[1])));
        }
    }

    private IEnumerator jump(Vector3 jumpVel)
    {
        this.jumping = true;
        rb.velocity = jumpVel;
        while (Physics.Raycast(transform.position, Vector3.down, this.size.y / 2))
            yield return null;
        while (!Physics.Raycast(transform.position, Vector3.down, this.size.y / 2))
            yield return null;
        yield return new WaitForSeconds(this.attackTime);
        this.jumping = false;
    }

    public override void attackTarget()
    {
        moveToTarget();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (this.tag == "Enemies")
        {
            if (System.Array.IndexOf(new string[] { "Player, Allies " }, collision.collider.tag) > -1)
            {
                return;
                //do damage
            }
        }
        else if (collision.collider.tag == " Enemies")
            return;
    }
}