using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBoss : EnemyBoss
{
    float spawnRate = 2f;
    float spawnTimer = 0f;

    void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            WorldManager.instance.SpawnPrefab(PrefabDatabase.instance.SLIME_ENEMY, transform.position + new Vector3(-3f, 0, 0));
            spawnTimer = spawnRate;
        }
    }

    public override void GivePlayerAbility()
    {
        base.GivePlayerAbility();

        Inventory.instance.AddAbility(new SlimeBall());
    }
}
