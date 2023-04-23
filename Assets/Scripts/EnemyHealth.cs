using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : ObjectHealth
{
    public GameObject ZombieRemains;
    public GameObject ZombieExplosion;



    public override void HandleCollision(GameObject otherObject)
    {

    }

    public override void OnDeath()
    {
        Instantiate(ZombieRemains, transform.position, Quaternion.identity);
        Destroy(gameObject);
        base.OnDeath();
    }
}
