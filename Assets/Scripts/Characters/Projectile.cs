using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 2f;
    public int damage = 1;
    public int numTimes = 1;
    public int timeBetween = 1;
    public int stunTime = 0;
    private bool readyToDestroy = true;
    private Quaternion direction;
    private WaitForSeconds wait;
    public GameObject shooter;
    public GameObject spawnOnDestroy;

    private void Start()
    {
        shooter = this.transform.parent.gameObject;
        wait = new WaitForSeconds(timeBetween);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
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
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        int layer = other.gameObject.layer;
        if (layer != LayerMask.NameToLayer("Platform") && layer != shooter.gameObject.layer)
        {
            Damageable target = other.GetComponent<Damageable>();
            if (target != null) damageTarget(target);
        }
    }

    public IEnumerator DestroyMe(float time)
    {
        float t = 0;
        for (; t < time || !readyToDestroy; t += Time.deltaTime)
            yield return null;
        Destroy(this.gameObject);
    }

    public void OnDestroy()
    {
        if (spawnOnDestroy)
            GameObject.Instantiate(spawnOnDestroy, this.transform.position, this.transform.rotation);
    }
}