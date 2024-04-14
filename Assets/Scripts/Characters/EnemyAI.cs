using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour, Damageable
{
    public float health = 1;
    protected bool stunned = false;
    protected bool attacking = false;
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float attackTime = 1f;
    public float detectionRange = 10f;
    public float attackRange = 1f;
    public float  attackPower = 5f;
    public float stunDuration = 1.0f;

    protected GameObject target;
    protected Vector3 targetPos;
    protected Rigidbody2D rb;
    protected SpriteRenderer sr;
    protected Vector2 size;
    protected float sizeBuffer = 0.01f;
    protected LayerMask visionLayerMask;
    protected bool grounded = true;

    public virtual void Start()
    {
        targetPos = this.transform.position;
        rb = this.GetComponent<Rigidbody2D>();
        sr = this.GetComponent<SpriteRenderer>();
        size = this.GetComponent<Collider2D>().bounds.size * (1 + 2 * sizeBuffer);
        attackRange += size.x / 2;
        detectionRange += size.x / 2;
        visionLayerMask = ~(1 << LayerMask.NameToLayer("Platform") | 1 << LayerMask.NameToLayer(this.tag) | 1<< 2);
        updateTarget(GameObject.Find("Player"));
    }

    public virtual void FixedUpdate()
    {
        DrawDebugLines();
        if (target != null)
        {
            lookForTarget();
            if (targetPos != this.transform.position && !stunned)
                if (inRange(target.transform.position) && !attacking)
                    StartCoroutine(attackEnumerator());
                else if (!attacking)
                    moveToTarget();
        }
        if (attacking)
        {
            Vector2 diff = targetPos - this.transform.position;
            sr.flipX = diff.x < 0 ? true : diff.x > 0 ? false : sr.flipX;
        }
        else
            sr.flipX = rb.velocity.x < 0 ? true : rb.velocity.x > 0 ? false : sr.flipX;
    }

    public void DrawDebugLines()
    {
        Vector2 dir = (target.transform.position - this.transform.position).normalized;
        Debug.DrawRay(this.transform.position, dir * detectionRange);
        Debug.DrawRay(this.transform.position, dir * attackRange, Color.red);
    }

    public virtual void lookForTarget()
    {
        Vector2 dir = (target.transform.position - this.transform.position).normalized;
        RaycastHit2D hitData = Physics2D.Raycast(this.transform.position, dir, detectionRange, visionLayerMask);
        if(hitData)
        {
            if (hitData.collider.gameObject.layer == this.target.layer)   //no wall between and same team = update target position
            {
                this.target = hitData.collider.gameObject;
                targetPos = target.transform.position;
            }
            else if (inRange(targetPos, 0.1f))      // if wall between and already at target, wait in place
                targetPos = this.transform.position;
        }
    }

    public bool inRange(Vector2 position, float range = -1)
    {
        if (range < 0) range = this.attackRange;
        return Vector2.Distance(this.transform.position, position) < range;
    }

    public void updateTarget(GameObject target)
    {
        this.target = target;
    }

    public virtual void moveToTarget()
    {
        Vector2 diff = targetPos - this.transform.position;
        rb.velocity = diff.normalized * moveSpeed;
    }
    public virtual void walkToTarget(bool jumpEnabled = true)
    {
        if (grounded)
        {
            Vector2 diff = targetPos - this.transform.position;
            rb.velocity = new Vector3(diff.x < 0 ? -moveSpeed : diff.x > 0 ? moveSpeed : 0, rb.velocity.y, 0);

            if (jumpEnabled && !Physics2D.Raycast(this.transform.position, rb.velocity, this.size.x / 2, 1 << LayerMask.NameToLayer("Terrain")))
            {
                rb.velocity += Vector2.up * jumpForce;
                grounded = false;
            }
        }
        else
        {
            Debug.DrawRay(this.transform.position, Vector2.down * this.size.y / 2, grounded ? Color.green : Color.red);
            grounded = !Physics2D.Raycast(this.transform.position, Vector2.down, this.size.y / 2, ~(1 << this.gameObject.layer));
        }
    }

    public virtual IEnumerator attackEnumerator()
    {
        attacking = true;
        this.attackTarget();
        yield return new WaitForSeconds(attackTime);
        attacking = false;
    }

    public virtual void attackTarget()
    {
        //rb.velocity = Vector2.zero;
    }

    public void takeDamage(float damage, float stunTime = 0, GameObject damager = null)
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        health -= damage;
        StartCoroutine(stun(stunTime));
        if (damager != null)
            updateTarget(damager);
        takeDamageCallback();
    }

    public void takeDamageCallback(){
        if (health <= 0){
            Destroy(this.gameObject);
        }
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color32(107, 107, 107, 255);
    }

    public IEnumerator stun(float sec)
    {
        stunned = true;
        yield return new WaitForSeconds(sec);
        stunned = false;
    }

    public virtual void regenHealth(float healthRegained){
        //maybe we won't need this
    }
}
