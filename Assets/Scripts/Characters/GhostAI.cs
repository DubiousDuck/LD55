using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAI : EnemyAI
{
    public float fadeSpeed = 0.05f;

    public override void Start()
    {
        base.Start();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Color col = this.sr.color;
        float alpha = col.a + fadeSpeed * (this.attacking || this.rb.velocity.magnitude > 0 ? 1 : -1);
        col.a = Mathf.Min(1, Mathf.Max(0, alpha));
        this.sr.color = col;
    }

    public override void lookForTarget()
    {
        if (this.inRange(this.target.transform.position, this.detectionRange))
            targetPos = target.transform.position;
        else
            targetPos = this.transform.position;
    }

    public override void attackTarget()
    {
        this.moveToTarget();
    }

    public override void moveToTarget(bool towards = true, bool flying = true)
    {
        if(towards)
            base.moveToTarget(true, false);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (System.Array.IndexOf(new string[] { "Platform", "Enemies", "Player", "Terrain" }, collision.collider.tag) !> -1)
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), collision.collider, ignore: true);
    }
}
