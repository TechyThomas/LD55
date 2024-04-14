using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonEnemy : Enemy
{
    public GameObject bonePrefab;

    public float boneCooldown = 2f;
    float boneCooldownTimer;

    public override void EnemyUpdate()
    {
        base.EnemyUpdate();

        if (foundPlayer == null)
        {
            boneCooldownTimer = 0f;
            return;
        }

        boneCooldownTimer -= Time.deltaTime;

        if (boneCooldownTimer <= 0f)
        {
            boneCooldownTimer = boneCooldown;

            GameObject boneGO = Instantiate(bonePrefab, transform.position, Quaternion.identity);
            boneGO.GetComponent<BoneProjectile>().SetTarget(Player.Instance.transform);
            boneGO.GetComponent<BoneProjectile>().Throw();
        }

        Vector3 targetPos = transform.position - foundPlayer.transform.position;

        if (targetPos.x < 0f)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponentInChildren<SpriteRenderer>().flipX = false;
        }
    }
}
