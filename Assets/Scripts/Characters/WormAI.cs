using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static SpiderAI;

public class WormAI : EnemyAI
{
    private bool popped = false;
    public float popRange = 1f;
    public float popPower = 5f;
    private bool initialized;
    public Sprite afterPop;
    public float buryFraction = 0.9f;

    public override void Start()
    {
        base.Start();
        this.GetComponent<Collider2D>().enabled = false;
        this.rb.gravityScale = 0;
        this.sr.sortingOrder -= 1;
        LayerMask wallMask = 1 << LayerMask.NameToLayer("Platform") | 1 << LayerMask.NameToLayer("Terrain");
        RaycastHit2D hitData = Physics2D.Raycast(this.transform.position, Vector2.down, 10, wallMask);
        this.transform.position = hitData.point + Vector2.up * (this.size.y / 2 - this.size.y * buryFraction);
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
        if (popped)
            base.lookForTarget();
        else
            tryPop();
    }

    private void tryPop()
    {
        Vector2 currPos = this.transform.position + Vector3.down * (1-buryFraction);
        Vector2 leftUpper = currPos + new Vector2(-0.5f * (size.x + popRange), 1.5f * size.y);
        Vector2 rightLower = currPos + new Vector2(0.5f * (size.x + popRange), 0.5f * size.y);
        Debug.DrawLine(leftUpper, rightLower, Color.white);

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

    public override void moveToTarget()
    {
        this.walkToTarget(false);
    }

    public override void attackTarget()
    {
        this.rb.velocity = new Vector2(0, rb.velocity.y);
        if (popped)
            return;
        else
        {
            popped = true;
            this.sr.sprite = afterPop;
        }
        //damage target differently in each case
        
    }
}
