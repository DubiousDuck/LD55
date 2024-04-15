using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DwarfAI : EnemyAI
{
    private bool popped = false;
    private float origDetRange;
    public SpriteRenderer prePop;
    public float buryFraction = 0.95f;

    public override void Start()
    {
        base.Start();
        this.GetComponent<Collider2D>().enabled = false;
        this.rb.gravityScale = 0;
        Color color = this.sr.color;
        color.a = 0;
        this.sr.color = color;

        RaycastHit2D hitData = Physics2D.Raycast(this.transform.position, Vector2.down, 10, this.wallMask);
        this.transform.position = hitData.point + Vector2.up * this.size.y / 2;

        prePop.enabled = true;
        prePop.transform.position = this.transform.position + Vector3.down * this.size.y * buryFraction;
        prePop.sortingOrder -= 2;

        origDetRange = this.detectionRange;
        this.detectionRange = this.attackRange;
    }

    public override void FixedUpdate()
    {
        if (popped)
            base.FixedUpdate();
        else
            this.lookForTarget();
    }

    public override void lookForTarget()
    {
        base.lookForTarget();
        if(!popped && targetPos == target.transform.position)
        {
            Color color = this.sr.color;
            color.a = 1;
            this.sr.color = color;
            this.sr.sortingOrder += 2;
            this.prePop.enabled = false;

            this.rb.gravityScale = 1;
            this.transform.position += Vector3.up * size.y * buryFraction;
            this.GetComponent<Collider2D>().enabled = true;
            popped = true;
            this.detectionRange = origDetRange;
        }
    }

    public override void moveToTarget(bool towards = true)
    {
        this.walkToTarget(towards);
    }

    public override void attackTarget()
    {

    }
}
