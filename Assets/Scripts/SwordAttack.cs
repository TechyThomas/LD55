using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public LayerMask attackMask;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 0.33f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = Player.Instance.GetPosition() + new Vector2(1f, 0f);
        transform.position = pos;
    }

    public void Attack(float damage)
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 2f, Vector2.up, 0f, attackMask);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.GetComponent<Health>() == null)
            {
                continue;
            }

            hit.collider.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
