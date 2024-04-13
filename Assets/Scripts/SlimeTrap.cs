using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeTrap : MonoBehaviour
{
    float lifespan = 5f;
    float lifeTimer = 0f;

    Enemy enemy;
    bool isTrapped = false;

    void Start()
    {
        lifeTimer = lifespan;
    }

    void Update()
    {
        lifeTimer -= Time.deltaTime;

        if (lifeTimer <= 0f)
        {
            if (enemy != null)
            {
                enemy.SetMove(true);
            }

            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        enemy = col.GetComponent<Enemy>();

        if (isTrapped || enemy == null)
        {
            return;
        }

        isTrapped = true;
        enemy.SetMove(false);
    }
}
