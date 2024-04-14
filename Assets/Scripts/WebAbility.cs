using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebAbility : Ability
{
    public WebAbility()
    {
        name = "Web Rope";
        description = "Shoot a web and swing like no super hero has ever done.";
        attack = 0;
        cooldown = 0;
    }

    public override void Attack()
    {
        base.Attack();
        WebRope.Instance.CreateRope();
    }
}
