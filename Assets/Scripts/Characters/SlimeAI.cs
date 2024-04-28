using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAI : EnemyAI
{
    public float[] jumpModRange = new float[] { 1f, 1.5f };

    public override void Start()
    {
        base.Start();
    }

    public override void moveToTarget(bool towards = true, bool flying = false)
    {
        if (!attacking)
            StartCoroutine(attackEnumerator());
    }

    public override IEnumerator attackEnumerator()
    {
        this.attacking = true;
        
        float randForce = this.jumpForce * Random.Range(jumpModRange[0], jumpModRange[1]);
        Vector3 diff = this.targetPos - this.transform.position;
        Vector3 jumpVel = new Vector3(diff.x < 0 ? -1 : diff.x > 0 ? 1 : 0, Random.Range(0.5f, 2), 0);
        jumpVel = jumpVel.normalized * randForce;

        this.GetComponent<Collider2D>().enabled = false;
        while (grounded)
        {
            rb.velocity = jumpVel;
            Vector2 downLeft = (Vector2)this.GetComponent<Collider2D>().bounds.min + Vector2.down * sizeBuffer + Vector2.right * sizeBuffer;
            grounded = Physics2D.OverlapArea(downLeft, downLeft + Vector2.down * sizeBuffer + Vector2.right * (this.size.x - 2 * sizeBuffer), wallMask);
            Debug.DrawLine(downLeft, downLeft + Vector2.down * sizeBuffer + Vector2.right * (this.size.x - 2 * sizeBuffer), grounded ? Color.red : Color.white);
            yield return null;
        }

        this.GetComponent<Collider2D>().enabled = true;
        while (!grounded)
        {
            Vector2 downLeft = (Vector2)this.GetComponent<Collider2D>().bounds.min + Vector2.down * sizeBuffer + Vector2.right * sizeBuffer;
            grounded = Physics2D.OverlapArea(downLeft, downLeft + Vector2.down * sizeBuffer + Vector2.right * (this.size.x - 2 * sizeBuffer), wallMask);
            Debug.DrawLine(downLeft, downLeft + Vector2.down * sizeBuffer + Vector2.right * (this.size.x - 2 * sizeBuffer), grounded ? Color.red : Color.white);
            yield return null;
        }
        yield return new WaitForSeconds(this.attackTime);
        this.attacking = false;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (grounded) Debug.Log("Grounded");
        else if (this.tag == "Enemies" && collision.collider.tag == "Allies" ||
            collision.collider.tag == "Enemies" && this.tag == "Allies")
            collision.gameObject.GetComponent<Damageable>().takeDamage(this.attackPower, 0, this.gameObject);
    }
}