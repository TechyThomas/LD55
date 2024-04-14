using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneProjectile : MonoBehaviour
{
    public float speed = 5f;
    public float rotSpeed = 3f;

    public float throwPower = 3f;
    public float liftPower = 3f;

    public float damage = 15f;

    Rigidbody2D rb;

    public Transform boneGraphic;

    int direction = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        boneGraphic.Rotate(new Vector3(0f, 0f, transform.rotation.eulerAngles.y - (rotSpeed * Time.deltaTime * direction)));
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

    public void SetTarget(Transform target)
    {
        Vector3 targetPos = target.position - transform.position;

        if (targetPos.x < 0f)
        {
            direction = -1;
        }
    }

    public void Throw()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        rb.AddForce(new Vector2(throwPower * direction, liftPower), ForceMode2D.Impulse);
    }
}
