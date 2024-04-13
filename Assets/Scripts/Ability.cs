using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability
{
    public string name = "";
    public string description = "";
    public float attack = 0f;
    public float cooldown = 0f;

    public virtual void Attack()
    {
    }
}
