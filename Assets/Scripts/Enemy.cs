using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyAIType aiType = EnemyAIType.AGRO_TARGET;
    public float speed = 5f;

    Rigidbody2D rb;

    bool canMove = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (!canMove || rb == null || Player.Instance == null)
        {
            return;
        }

        int direction = 1;

        Vector2 distance = Player.Instance.GetPosition() - rb.position;

        if (distance.x < 0)
        {
            direction = -1;
        }

        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }

    public virtual void Attack()
    {

    }

    public void SetMove(bool canMove)
    {
        this.canMove = canMove;
    }
}

public enum EnemyAIType
{
    PLAYER_TARGET, AGRO_TARGET
}
