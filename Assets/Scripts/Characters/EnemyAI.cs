using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float health = 1;
    protected bool stunned = false;
    protected bool attacking = false;
    public float moveSpeed = 5f;
    public float attackTime = 1f;
    public float detectionRange = 10f;
    public float attackRange = 5f;

    protected GameObject target;
    protected Vector3 targetPos;
    protected Rigidbody rb;

    public virtual void Start()
    {
        targetPos = this.transform.position;
        rb = this.GetComponent<Rigidbody>();
        updateTarget(GameObject.Find("Player"));
    }

    public virtual void FixedUpdate()
    {
        DrawDebugLines();
        if (target != null)
        {
            rb.velocity = Vector3.zero;
            lookForTarget();
            if (targetPos != this.transform.position && !stunned)
                if (inRange(target.transform.position) && !attacking)
                    StartCoroutine(attackTarget());
                else if (!attacking)
                    moveToTarget(targetPos);
        }
    }

    public void DrawDebugLines()
    {
        Vector3 dir = Vector3.Normalize(target.transform.position - this.transform.position);
        Debug.DrawRay(this.transform.position, dir * detectionRange);
        Debug.DrawRay(this.transform.position, dir * attackRange, Color.red);
    }

    public virtual void lookForTarget()
    {
        Vector3 dir = Vector3.Normalize(target.transform.position - this.transform.position);
        Ray ray = new Ray(this.transform.position, dir);
        if (Physics.Raycast(ray, out RaycastHit hitData, maxDistance: detectionRange))
        {
            if (hitData.collider.tag == "Player")   //no wall between = update target position
                targetPos = target.transform.position;
            else if (inRange(targetPos, 0.1f))      // if wall between and already at target, wait in place
                targetPos = this.transform.position;
        }
    }

    public bool inRange(Vector3 position, float range = -1)
    {
        if (range < 0) range = this.attackRange;
        return Vector3.Distance(this.transform.position, position) < range;
    }

    public void updateTarget(GameObject target)
    {
        this.target = target;
    }

    public virtual void moveToTarget(Vector3 pos)
    {
        Vector3 diff = targetPos - this.transform.position;
        if (diff.magnitude > moveSpeed)
            diff = Vector3.Normalize(diff) * moveSpeed;
        rb.velocity = diff;
    }

    public virtual IEnumerator attackTarget()
    {
        attacking = true;
        //do stuff
        yield return new WaitForSeconds(attackTime);
        attacking = false;
    }

    public virtual void takeDamage(float damage, float stunTime = 0, GameObject damager = null)
    {
        health -= damage;
        StartCoroutine(stun(stunTime));
        if (damager != null)
            updateTarget(damager);
    }

    public IEnumerator stun(float sec)
    {
        stunned = true;
        yield return new WaitForSeconds(sec);
        stunned = false;
    }
}
