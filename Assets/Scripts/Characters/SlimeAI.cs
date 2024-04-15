using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class SlimeAI : EnemyAI
{
    public float[] jumpModRange = new float[] { 0.5f, 1.5f };

    public override void Start()
    {
        base.Start();
        this.grounded = true;
    }

    public override void moveToTarget(bool towards = true)
    {
        if (grounded)
        {
            float randForce = this.jumpForce * Random.Range(jumpModRange[0], jumpModRange[1]);
            Vector3 diff = this.targetPos - this.transform.position;
            Vector3 jumpVel = new Vector3(diff.x < 0 ? -1 : diff.x > 0 ? 1 : 0, Random.Range(0.5f, 2), 0);
            StartCoroutine(jump(jumpVel.normalized * randForce));
        }
    }

    private IEnumerator jump(Vector3 jumpVel)
    {
        this.attacking = true;
        this.grounded = false;
        rb.velocity = jumpVel;
        while (Physics2D.Raycast(transform.position + Vector3.down * this.size.y / 2, Vector2.down, this.sizeBuffer, ~(1 << 2)))
        {
            rb.velocity = jumpVel;
            yield return null;
        }
        while (!Physics2D.Raycast(transform.position + Vector3.down * this.size.y / 2, Vector2.down, this.sizeBuffer, ~(1 << 2)))
            yield return null;

        this.attacking = false;
        yield return new WaitForSeconds(this.attackTime);
        this.grounded = true;
    }

    public override void attackTarget()
    {
        moveToTarget();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.tag == "Enemies")
        {
            if (attacking && System.Array.IndexOf(new string[] { "Player, Allies " }, collision.collider.tag) > -1)
            {
                return;
                //do damage
            }
        }
        else if (collision.collider.tag == " Enemies")
            return;
    }
}