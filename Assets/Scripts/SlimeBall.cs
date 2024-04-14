using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBall : Ability
{
    int maxBalls = 3;
    int currentBalls = 0;

    public SlimeBall()
    {
        name = "Slime Ball";
        description = "Fire stick balls of slime";
        attack = 0;
        cooldown = 0.1f;
    }

    public override void Attack()
    {
        Debug.Log(currentBalls);

        if (currentBalls >= maxBalls)
        {
            return;
        }

        base.Attack();

        int playerDir = Player.Instance.GetDirection();

        GameObject attackGO = WorldManager.Instance.SpawnPrefab(PrefabDatabase.Instance.SLIME_BALL, Player.Instance.GetPosition() + new Vector2(1f * playerDir, 0));
        attackGO.GetComponent<SlimeBallProjectile>().SetDirection(playerDir);

        currentBalls++;
    }

    public void RemoveBall()
    {
        currentBalls--;

        if (currentBalls < 0)
        {
            currentBalls = 0;
        }
    }
}
