using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 2f;
    public int damage = 10;
    public GameObject shooter;

    private void Start()
    {
        // Destroy the projectile after its lifetime expires
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // Move the projectile forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the projectile collided with an object that can take damage
        Damageable target = other.GetComponent<Damageable>();

        if (target != null)
        {
            damageTarget(target);
            Destroy(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void damageTarget(Damageable target)
    {

    }
}