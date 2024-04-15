using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBoss : EnemyBoss
{
    float spawnRate = 1f;
    float spawnTimer = 0f;

    public SpiderBoss()
    {
        bossName = "Hairy Legs";
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            GameObject slimeGO = WorldManager.Instance.SpawnPrefab(PrefabDatabase.Instance.SPIDER_ENEMY, transform.position + new Vector3(0, -2f, 0));
            slimeGO.GetComponent<Enemy>().aiType = EnemyAIType.PLAYER_TARGET;

            spawnTimer = spawnRate;
        }
    }

    public override void GivePlayerAbility()
    {
        base.GivePlayerAbility();

        Inventory.Instance.AddAbility(new WebAbility());
        LevelManager.Instance.SwitchLevel(1);
    }
}
