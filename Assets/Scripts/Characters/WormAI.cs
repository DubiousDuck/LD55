using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormAI : EnemyAI
{
    private bool popped = false;
    public float popRange = 1f;
    public float popPower = 5f;
    public Sprite afterPop;
    public float buryFraction = 0.9f;

    public override void Start()
    {
        base.Start();
        this.GetComponent<Collider2D>().enabled = false;
        this.rb.gravityScale = 0;
        this.sr.sortingOrder -= 2;
        RaycastHit2D hitData = Physics2D.Raycast(this.transform.position, Vector2.down, 10, this.wallMask);
        this.transform.position = hitData.point + Vector2.up * (this.size.y / 2 - this.size.y * buryFraction);
    }

    public override void lookForTarget()
    {
        if (popped)
            base.lookForTarget();
        else
        {
            Vector2 currPos = this.transform.position + Vector3.down * (1 - buryFraction);
            Vector2 leftUpper = currPos + new Vector2(-0.5f * (size.x + popRange), 1.5f * size.y);
            Vector2 rightLower = currPos + new Vector2(0.5f * (size.x + popRange), 0.5f * size.y);

            Collider2D col = Physics2D.OverlapArea(leftUpper, rightLower, 1 << this.target.gameObject.layer);
            if (col)
            {
                this.transform.position = new Vector2(col.transform.position.x,
                    this.transform.position.y + size.y * buryFraction);
                this.GetComponent<Collider2D>().enabled = true;
                this.rb.gravityScale = 1;
                this.target = col.gameObject;
                this.sr.sortingOrder += 2;
                this.attackTarget();
            }
        }
    }

    public override void moveToTarget(bool towards = true, bool flying = true)
    {
        base.moveToTarget(towards, false);
    }

    public override void attackTarget()
    {
        if (popped)
            target.GetComponent<Damageable>().takeDamage(attackPower, 0, this.gameObject);
        else
        {
            popped = true;
            this.sr.sprite = afterPop;
            // do pop damage
        }
    }
}
