using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAxe : Projectile
{
    public float maxDist = 5f;
    public float grabRange = 1f;
    private bool returning = false;
    public Vector3 modVector;

    public override void FixedUpdate()
    {
        this.transform.Rotate(new Vector3(0, 0, rotateSpeed));
        if (!returning)
        {
            maxDist -= this.speed * Time.deltaTime;
            if (maxDist <= 0)
                returning = true;
        }
        else if (this.shooter)
        {
            Vector3 diff = this.shooter.transform.position - this.transform.position;
            if(diff.magnitude < grabRange)
            {
                shooter.GetComponent<DwarfAI>().pickReady = true;
                Destroy(this.gameObject);
            }
            else
                this.rb.velocity = (this.shooter.transform.position - this.transform.position).normalized * this.speed;
        }
        else
            Destroy(this.gameObject);
    }

    public override IEnumerator damageTarget(Damageable target)
    {
        target.takeDamage(damage, stunTime, shooter);
        yield return null;
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject != shooter)
            base.OnTriggerEnter2D(other);
    }

    public override void collisionLogic(Collider2D collider)
    {
        return;
    }
}
