using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DwarfAI : EnemyAI
{
    private bool popped = false;
    public bool pickReady = true;
    public PickAxe pick;
    private float origDetRange;
    public SpriteRenderer prePop;
    public float buryFraction = 0.95f;

    public override void Start()
    {
        base.Start();
        this.GetComponent<Collider2D>().enabled = false;
        this.rb.gravityScale = 0;
        this.sr.enabled = false;

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
            popped = true;
            this.sr.enabled = true;
            this.sr.sortingOrder += 2;
            this.prePop.enabled = false;

            this.rb.gravityScale = 1;
            this.transform.position += Vector3.up * size.y * buryFraction;
            this.GetComponent<Collider2D>().enabled = true;
            this.detectionRange = origDetRange;
        }
    }

    public override void moveToTarget(bool towards = true)
    {
        this.walkToTarget(towards);
    }

    public override IEnumerator attackEnumerator()
    {
        attacking = true;
        pickReady = false;
        this.attackTarget();
        while (!pickReady)
            yield return null;
        yield return attackWait;
        attacking = false;
    }

    public override void attackTarget()
    {
        throwProj(pick);
    }
}
