using UnityEngine;
using UnityEngine.SceneManagement;

public class SkeletonBoss : EnemyBoss
{
    float spawnRate = 2f;
    float spawnTimer = 0f;

    public SkeletonBoss()
    {
        bossName = "Mr Nobody";
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            GameObject slimeGO = WorldManager.Instance.SpawnPrefab(PrefabDatabase.Instance.SKELETON_ENEMY, transform.position + new Vector3(0, -2f, 0));
            slimeGO.GetComponent<Enemy>().aiType = EnemyAIType.PLAYER_TARGET;

            spawnTimer = spawnRate;
        }
    }

    public override void GivePlayerAbility()
    {
        base.GivePlayerAbility();

        SceneManager.LoadScene("Win Screen");
    }
}
