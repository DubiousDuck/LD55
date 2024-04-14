using UnityEngine;

public interface Damageable
{
    public void takeDamage(float damage, float stunTime = 0, GameObject damager = null);
    public void regenHealth(float healthRegained);
}
