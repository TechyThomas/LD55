using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpiderWebProjectile : MonoBehaviour
{
    public float speed = 7f;
    public float damage = 10f;

    int direction = 1;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        Vector3 webPos = transform.right * speed * Time.fixedDeltaTime * direction;
        transform.position += webPos;
    }

    public void SetDirection(int direction)
    {
        this.direction = direction;
    }

    public void SetTarget(Transform target)
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        Vector3 targetPos = target.position - transform.position;
        rb.MoveRotation(Quaternion.LookRotation(targetPos));
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Health health = col.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage);
            Player.Instance.AddKnockback();
        }

        Destroy(gameObject);
    }
}
