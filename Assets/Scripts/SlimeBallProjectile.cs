using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlimeBallProjectile : MonoBehaviour
{
    public float speed = 10f;
    public float liftPower = 3f;
    public float throwPower = 10f;

    Rigidbody2D rb;

    int direction = 1;

    public AudioClip splatSound;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Throw()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        rb.AddForce(new Vector2(throwPower * direction, liftPower), ForceMode2D.Impulse);
    }

    public void SetDirection(int direction)
    {
        this.direction = direction;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        int index = Inventory.Instance.abilities.FindIndex(a => a.GetType() == typeof(SlimeBall));
        if (index >= 0)
        {
            SlimeBall ball = (SlimeBall)Inventory.Instance.abilities[index];
            ball.RemoveBall();
        }

        Vector2 spawnPos = transform.position;
        spawnPos.y = Mathf.Ceil(spawnPos.y);

        WorldManager.Instance.SpawnPrefab(PrefabDatabase.Instance.SLIME_TRAP, spawnPos);
        AudioManager.Instance.PlaySound(splatSound);

        Destroy(gameObject);
    }
}
