using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderEnemy : Enemy
{
    public GameObject projectilePrefab;

    public float webCooldown = 2f;
    float webCooldownTimer;

    public override void EnemyUpdate()
    {
        base.EnemyUpdate();

        if (foundPlayer == null)
        {
            webCooldownTimer = 0f;
            return;
        }

        webCooldownTimer -= Time.deltaTime;

        if (webCooldownTimer <= 0f)
        {
            webCooldownTimer = webCooldown;

            GameObject webGO = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            webGO.GetComponent<SpiderWebProjectile>().SetDirection(Player.Instance.GetDirection());
            webGO.GetComponent<SpiderWebProjectile>().SetTarget(Player.Instance.transform);
        }
    }
}
