using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class VultureAI : EnemyAI
{
    public Projectile bones;
    public override void attackTarget()
    {
        throwProj(bones);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (System.Array.IndexOf(new string[] { "Platform", this.tag }, collision.collider.tag)! > -1)
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), collision.collider, ignore: true);
    }
}
