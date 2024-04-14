using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAI : EnemyAI
{
    public override void attackTarget()
    {
         rb.velocity = Vector3.zero;
         target.GetComponent<Damageable>().takeDamage(attackPower, stunDuration, this.gameObject);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (System.Array.IndexOf(new string[] { "Platform", this.tag }, collision.collider.tag) !> -1)
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), collision.collider, ignore: true);
    }
}
