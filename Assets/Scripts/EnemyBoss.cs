using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : Enemy
{
    protected string bossName;

    void Start()
    {
        GetComponent<Health>().onDeath += GivePlayerAbility;

        UI_Boss.Instance.SetBoss(this);
        UI_Boss.Instance.ShowUI();
    }

    public virtual void GivePlayerAbility()
    {
        UI_Boss.Instance.HideUI();
        UI_Boss.Instance.SetBoss(null);
    }

    public string GetName()
    {
        return bossName;
    }
}
