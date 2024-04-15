using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBoss : EnemyBoss
{
    float spawnRate = 2f;
    float spawnTimer = 0f;

    public SlimeBoss()
    {
        bossName = "Sticky Thing";
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            GameObject slimeGO = WorldManager.Instance.SpawnPrefab(PrefabDatabase.Instance.SLIME_ENEMY, transform.position + new Vector3(0, -2f, 0));
            slimeGO.GetComponent<Enemy>().aiType = EnemyAIType.PLAYER_TARGET;

            spawnTimer = spawnRate;
        }
    }

    public override void GivePlayerAbility()
    {
        base.GivePlayerAbility();

        Inventory.Instance.AddAbility(new SlimeBall());
        LevelManager.Instance.SwitchLevel(1);
    }
}
