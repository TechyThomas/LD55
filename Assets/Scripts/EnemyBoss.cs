using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : Enemy
{
    void Start()
    {
        GetComponent<Health>().onDeath += GivePlayerAbility;
    }

    public virtual void GivePlayerAbility()
    {

    }
}
