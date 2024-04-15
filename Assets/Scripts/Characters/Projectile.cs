using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 2f;
    public float damage = 1;
    public int numTimes = 1;
    public float timeBetween = 1;
    public float stunTime = 0;
    private bool readyToDestroy = true;
    private Quaternion direction;
    private WaitForSeconds wait;
    protected GameObject shooter;
    public GameObject spawnOnDestroy;

    private void Start()
    {
        shooter = this.transform.parent.gameObject;
        this.transform.parent = null;
        wait = new WaitForSeconds(timeBetween);
        this.GetComponent<Rigidbody2D>().velocity = this.transform.rotation * Vector2.up * speed;
    }

    private void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0 && readyToDestroy)
            Destroy(this.gameObject);
    }

    public virtual IEnumerator damageTarget(Damageable target)
    {
        readyToDestroy = false;
        this.GetComponent<SpriteRenderer>().enabled = false;
        target.takeDamage(damage, stunTime, shooter);
        for (int i = 1; i < numTimes; i++)
        {
            target.takeDamage(damage, stunTime, null);
            yield return wait;
        }
        readyToDestroy = true;
        lifetime = 0;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        int layer = other.gameObject.layer;
        if (layer != LayerMask.NameToLayer("Platform") && layer != shooter.gameObject.layer)
        {
            Damageable target = other.GetComponent<Damageable>();
            if (target != null)
                StartCoroutine(damageTarget(target));
            collisionLogic(other);
        }
    }

    public void collisionLogic(Collider2D collider)
    {
        this.GetComponent<Collider2D>().enabled = false;
    }

    public void OnDestroy()
    {
        if (spawnOnDestroy)
            GameObject.Instantiate(spawnOnDestroy, this.transform.position, this.transform.rotation);
    }
}