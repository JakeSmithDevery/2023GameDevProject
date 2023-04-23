using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : ObjectHealth
{
    public GameObject ZombiePrefab;
    public GameObject SpawnerExplosion;
    public float SpawnTime = 5;
    public float SpawnArea = 2;

    public int MaxZombiesToSpawn = 10;
    int numberOfZombiesSpawned;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnZombie", 2, 5);
    }

    void SpawnZombie()
    {
        Vector3 randomPosition = Random.insideUnitCircle * SpawnArea;

        Instantiate(ZombiePrefab, transform.position + randomPosition, Quaternion.identity);
        numberOfZombiesSpawned++;

        if (numberOfZombiesSpawned >= MaxZombiesToSpawn)
        {
            CancelInvoke("SpawnZombie");
        }
    }

    public override void HandleCollision(GameObject otherObject)
    {

    }
    public override void OnDeath()
    {
        Instantiate(SpawnerExplosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
        base.OnDeath();
    }
}
