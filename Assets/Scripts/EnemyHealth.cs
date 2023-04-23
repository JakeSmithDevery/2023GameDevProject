using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : ObjectHealth
{
    public GameObject ZombieRemains;
    public GameObject ZombieExplosion;



    public override void HandleCollision(GameObject otherObject)
    {
        if (otherObject.CompareTag("Sword"))
        {
            Bullet b = otherObject.GetComponent<Bullet>();
            SubtractHealth(b.Damage);
        }
        base.HandleCollision(otherObject);
    }

    public override void OnDeath()
    {
        Instantiate(ZombieRemains, transform.position, Quaternion.identity);
        Destroy(gameObject);
        base.OnDeath();
    }
}
