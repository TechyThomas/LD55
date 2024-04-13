using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlimeBallProjectile : MonoBehaviour
{
    public float speed = 10f;
    float power = 3f;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(power * Vector2.up, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        int index = Inventory.instance.abilities.FindIndex(a => a.GetType() == typeof(SlimeBall));
        if (index >= 0)
        {
            SlimeBall ball = (SlimeBall)Inventory.instance.abilities[index];
            ball.RemoveBall();
        }

        Vector2 spawnPos = transform.position;
        spawnPos.y = Mathf.Ceil(spawnPos.y);

        WorldManager.instance.SpawnPrefab(PrefabDatabase.instance.SLIME_TRAP, spawnPos);

        Destroy(gameObject);
    }
}
