using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour, Damageable
{
    public float health = 1;
    protected bool stunned = false;
    protected Web web = null;
    protected bool attacking = false;
    public float moveSpeed = 5f;
    public float runSpeed = 0f;
    public float jumpForce = 5f;
    public float attackTime = 1f;
    public float detectionRange = 10f;
    public float attackRange = 1f;
    public float  attackPower = 5f;
    public Pickup soulDrop;

    protected GameObject player;
    public float allyFollowRange = 2f;
    public GameObject target;
    protected Vector3 targetPos;
    protected Rigidbody2D rb;
    protected SpriteRenderer sr;
    protected Vector2 size;
    protected float sizeBuffer = 0.01f;
    protected LayerMask visionLayerMask;
    protected LayerMask wallMask;
    public bool grounded = false;
    protected GameObject spawner;
    protected WaitForSeconds attackWait;

    public Sprite[] sprites = { null, null };
    private WaitForSeconds spriteWait;

    public virtual void Start()
    {
        targetPos = this.transform.position;
        rb = this.GetComponent<Rigidbody2D>();
        sr = this.GetComponent<SpriteRenderer>();
        size = this.GetComponent<Collider2D>().bounds.size * (1 + 2 * sizeBuffer);
        attackRange += size.x / 2;
        detectionRange += size.x / 2;
        visionLayerMask = ~(1 << LayerMask.NameToLayer("Platform") | 1 << LayerMask.NameToLayer(this.tag) | 1<< 2);
        wallMask = 1 << LayerMask.NameToLayer("Platform") | 1 << LayerMask.NameToLayer("Terrain");
        attackWait = new WaitForSeconds(attackTime);
        player = GameObject.Find("Player");
        updateTarget(player);
        if (this.tag == "Allies")
        {
            //this.GetComponent<Collider2D>().isTrigger = true;
            detectionRange = Mathf.Infinity;
            visionLayerMask = 1 << LayerMask.NameToLayer("Enemies");
        }
        this.spriteWait = new WaitForSeconds(Random.Range(0.5f, 1f));
        StartCoroutine(spriteChanger());
    }

    private IEnumerator spriteChanger()
    {
        sr.color = Color.white;
        while (true)
            foreach (Sprite sprite in sprites)
            {
                this.sr.sprite = sprite;
                yield return spriteWait;
            }
    }

    public virtual void FixedUpdate()
    {
        if (this.tag == "Allies" && target == player || !target)
            updateTarget(player.GetComponent<PlayerDamageable>().getAgro());
        
        DrawDebugLines();
        lookForTarget();

        if (this.tag == "Allies")
        {
            Vector3 playerPos = player.transform.position;
            if (!inRange(playerPos, player.GetComponent<PlayerDamageable>().maxFollowRange))
            {
                if (player.GetComponent<PlayerController>().grounded)
                {
                    updateTarget(player);
                    this.transform.position = player.transform.position;
                    this.rb.velocity = Vector2.zero;
                }
            }
            else if (!inRange(playerPos, allyFollowRange))
            {
                if (target == player || !inRange(playerPos, player.GetComponent<PlayerDamageable>().detectionRange))
                {
                    updateTarget(player);
                    targetPos = player.transform.position;
                }
            }
            else if (target == player)
            {
                //rb.velocity = new Vector2(rb.velocity.x * 0.2f, rb.gravityScale == 0 ? rb.velocity.y * 0.05f : rb.velocity.y);
                targetPos = this.transform.position;
            }
        }
        if (targetPos != this.transform.position && !stunned)
        {
            if (inRange(target.transform.position) && !attacking && target.tag != this.tag)
                StartCoroutine(attackEnumerator());
            else if (!attacking)
                moveToTarget();
        }

        if (attacking)
        {
            Vector2 diff = targetPos - this.transform.position;
            sr.flipX = diff.x < 0 ? true : diff.x > 0 ? false : sr.flipX;
            if (diff.magnitude < attackRange)
                moveToTarget(false);
            else
                moveToTarget(true);
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
        if (hitData)
        {
            if (hitData.collider.gameObject.layer == this.target.layer)   //no wall between and same team = update target position
            {
                updateTarget(hitData.collider.gameObject);
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

    public virtual void updateTarget(GameObject target)
    {
        this.target = target;
        //Debug.Log("my target is: " + target.name);
    }

    public virtual void moveToTarget(bool towards = true, bool flying = true)
    {
        Vector2 diff = targetPos - this.transform.position;
        if (flying)
            rb.velocity = diff.normalized * (towards ? moveSpeed : -runSpeed);
        else
        {
            if (towards)
                rb.velocity = new Vector3(diff.x < 0 ? -moveSpeed : diff.x > 0 ? moveSpeed : 0, rb.velocity.y, 0);
            else
                rb.velocity = new Vector3(diff.x < 0 ? runSpeed : diff.x > 0 ? -runSpeed : 0, rb.velocity.y, 0);

            Vector2 downLeft = (Vector2)this.transform.position + Vector2.left * (this.size.x / 2 - sizeBuffer) + Vector2.down * (this.size.y / 2 - sizeBuffer);
            grounded = Physics2D.OverlapArea(downLeft, downLeft + Vector2.down * sizeBuffer + Vector2.right * (this.size.x - 2 * sizeBuffer), wallMask);
            RaycastHit2D wallFront = Physics2D.Raycast((Vector2)this.transform.position + Vector2.down * (this.size.y/2 - 2 * sizeBuffer),
                Vector2.right * rb.velocity.x, this.size.x, wallMask);

            if (grounded && wallFront)
            {
                rb.velocity += Vector2.up * jumpForce;
                grounded = false;
            }
            if (this.web != null)
                rb.velocity *= web.slowModifier;
        }
    }

    public virtual IEnumerator attackEnumerator()
    {
        attacking = true;
        this.attackTarget();
        yield return attackWait;
        attacking = false;
    }

    public virtual void attackTarget()
    {
        //rb.velocity = Vector2.zero;
    }

    public void takeDamage(float damage, float stunTime = 0, GameObject damager = null)
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color32(200, 60, 60, 255);
        health -= damage;
        StartCoroutine(stun(stunTime));
        if (damager != null)
            updateTarget(damager);
        takeDamageCallback();
    }

    public void takeDamageCallback(){
        StartCoroutine(changeColorBack());
        if (health <= 0){
            if (this.tag != "Allies")
            {
                Instantiate(soulDrop, this.transform.position, this.transform.rotation);
            }
            else if ((target+"") != "null"){
                Debug.Log(target +"");
                EnemyAI targetAI = target.GetComponent<EnemyAI>();
                if (targetAI != null){
                    target.GetComponent<EnemyAI>().updateTarget(player);
                }
            }
            Destroy(this.gameObject);
        }
    }
    private IEnumerator changeColorBack(){
        yield return new WaitForSeconds(0.5f);
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
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

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<Web>() && other.gameObject.tag != this.gameObject.tag)
            this.web = other.GetComponent<Web>();
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        this.web = null;
    }

    public void throwProj(Projectile proj)
    {
        Vector2 direction = targetPos - this.transform.position;
        Projectile clone = Instantiate(proj, this.transform.position, Quaternion.FromToRotation(Vector3.up, direction));
        clone.shooter = this.gameObject;
        clone.tag = this.tag;
    }
}
