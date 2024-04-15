using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 2f;
    public float rotateSpeed = 0;
    public float damage = 1;
    public int numTimes = 1;
    public float timeBetween = 1;
    public float stunTime = 0;
    public float stunChance = 0;
    private bool readyToDestroy = true;
    private Quaternion direction;
    private WaitForSeconds wait;
    protected GameObject shooter;
    public GameObject spawnOnDestroy;
    protected Rigidbody2D rb;
    protected Vector3 initVel;

    public void init()
    {
        shooter = this.transform.parent.gameObject;
        this.transform.parent = null;
        this.tag = shooter.tag;
        this.gameObject.layer = 2;
        wait = new WaitForSeconds(timeBetween);
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = this.transform.rotation * Vector2.up * speed;
        initVel = rb.velocity;
    }

    public virtual void FixedUpdate()
    {
        this.transform.Rotate(new Vector3(0, 0, rotateSpeed));
        lifetime -= Time.deltaTime;
        if (lifetime <= 0 && readyToDestroy)
            Destroy(this.gameObject);
    }

    public virtual IEnumerator damageTarget(Damageable target)
    {
        readyToDestroy = false;
        this.GetComponent<SpriteRenderer>().enabled = false;
        if (Random.Range(0.0f, 1.0f) > stunChance)
            stunTime = 0;
        for (int i = 0; i < numTimes; i++)
        {
            target.takeDamage(damage, stunTime, i == 0 ? shooter : null);
            yield return wait;
        }
        readyToDestroy = true;
        lifetime = 0;
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        int layer = other.gameObject.layer;
        if (layer != 2 && layer != LayerMask.NameToLayer("Platform") && layer != this.gameObject.layer)
        {
            Damageable target = other.GetComponent<Damageable>();
            if (target != null)
                StartCoroutine(damageTarget(target));
            collisionLogic(other);
        }
    }

    public virtual void collisionLogic(Collider2D collider)
    {
        this.GetComponent<Collider2D>().enabled = false;
    }

    public void OnDestroy()
    {
        if (spawnOnDestroy)
        {
            GameObject obj = GameObject.Instantiate(spawnOnDestroy, this.transform.position, this.transform.rotation, this.transform);
            obj.GetComponent<Web>().init();
        }
    }
}