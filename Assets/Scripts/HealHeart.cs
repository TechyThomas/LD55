using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealHeart : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        Health health = col.GetComponent<Health>();

        if (health == null)
        {
            return;
        }

        health.Heal(20);

        Destroy(gameObject);
    }
}
