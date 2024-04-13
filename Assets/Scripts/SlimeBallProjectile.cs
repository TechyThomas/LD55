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
        Ability currentAbility = Player.instance.GetAbility();
        if (currentAbility.GetType() == typeof(SlimeBall))
        {
            SlimeBall ball = (SlimeBall)currentAbility;
            ball.RemoveBall();
        }

        Vector2 spawnPos = transform.position;
        spawnPos.y = Mathf.Ceil(spawnPos.y);

        WorldManager.instance.SpawnPrefab(PrefabDatabase.instance.SLIME_TRAP, spawnPos);

        Destroy(gameObject);
    }
}
