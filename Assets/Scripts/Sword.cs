using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Ability
{
    public Sword()
    {
        name = "Sword";
        description = "A blade that cuts things";
        attack = 30f;
        cooldown = 0.4f;
    }

    public override void Attack()
    {
        base.Attack();

        Vector2 swordSpawnPos = Player.Instance.GetPosition() + new Vector2(1f, 0f);
        GameObject attackGO = WorldManager.Instance.SpawnPrefab(PrefabDatabase.Instance.SWORD_ATTACK, swordSpawnPos);

        attackGO.GetComponent<SwordAttack>().Attack(attack);
    }
}
