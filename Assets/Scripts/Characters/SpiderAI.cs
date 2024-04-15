using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAI : EnemyAI
{
    public enum StickDir { LEFT = 270, RIGHT = 90, UP = 180, DOWN = 0 };
    public StickDir stickDir = StickDir.DOWN;

    public Projectile webBullet;
    public Projectile poison;
    public float poisonFrac = 0.85f;

    private bool initialized = false;
    private float lockCoord;
    private Vector3 lastPos;
    public override void Start()
    {
        this.transform.Rotate(new Vector3(0, 0, (float)this.stickDir));
        lastPos = this.transform.position;
        base.Start();
    }

    public override void FixedUpdate()
    {
        if (initialized)
        {
            base.FixedUpdate();

            Vector2 newPos = (Vector2)this.transform.position + rb.velocity * Time.deltaTime;
            if (!Physics2D.Raycast(newPos, -transform.up, this.size.y / 2, this.wallMask))
            {
                this.rb.velocity = Vector2.zero;
                this.transform.position = lastPos;
            }
            else
            {
                this.rb.velocity = rb.velocity;
                lastPos = this.transform.position;
            }
        }
        else if (!Physics2D.Raycast(this.transform.position, -this.transform.up, this.size.y / 2, this.wallMask))
            rb.velocity = -this.transform.up * moveSpeed;
        else
        {
            if (stickDir == StickDir.LEFT || stickDir == StickDir.RIGHT)
                lockCoord = this.transform.position.x;
            else
                lockCoord = this.transform.position.y;
            initialized = true;
        }
    }

    public override void moveToTarget(bool towards = true, bool flying = true)
    {
        Vector2 currPos = this.transform.position;
        if (stickDir == StickDir.LEFT || stickDir == StickDir.RIGHT)
        {
            this.transform.position = new Vector2(lockCoord, currPos.y);
            if (currPos.y < this.targetPos.y)
                rb.velocity = Vector2.up;
            else if (currPos.y > this.targetPos.y)
                rb.velocity = Vector2.down;
        }
        else
        {
            this.transform.position = new Vector2(currPos.x, lockCoord);
            if (currPos.x < this.targetPos.x)
                rb.velocity = Vector2.right;
            else if (currPos.x > this.targetPos.x)
                rb.velocity = Vector2.left;
        }
        rb.velocity *= towards ? moveSpeed : -runSpeed;
    }

    public override void attackTarget()
    {
        if (Random.Range(0.0f, 1.0f) < poisonFrac)
            throwProj(poison);
        else
            throwProj(webBullet);
    }
}