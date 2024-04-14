using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyAIType aiType = EnemyAIType.AGRO_TARGET;
    public float speed = 5f;
    public float agroRadius = 5f;
    public float attack = 5f;
    public float attackCooldown = 2f;
    float cooldownTimer;

    Rigidbody2D rb;
    Player foundPlayer;

    bool canMove = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        RaycastHit2D[] hits = Physics2D.CircleCastAll(rb.position, agroRadius, Vector2.up, 0f, Player.Instance.GetLayerMask());
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.GetComponent<Player>() == null)
            {
                continue;
            }

            foundPlayer = hit.collider.GetComponent<Player>();
            break;
        }

        if (aiType == EnemyAIType.PLAYER_TARGET)
        {
            PlayerTargetAI();
        }
        else if (aiType == EnemyAIType.AGRO_TARGET)
        {
            AgroTargetAI();
        }

        if (foundPlayer != null)
        {
            float playerDistance = Vector2.Distance(rb.position, foundPlayer.GetPosition());

            if (playerDistance <= 1.5f)
            {
                cooldownTimer -= Time.deltaTime;

                if (cooldownTimer <= 0f)
                {
                    foundPlayer.GetComponent<Health>().TakeDamage(attack);
                    cooldownTimer = attackCooldown;
                }
            }
            else
            {
                cooldownTimer = attackCooldown;
            }
        }
    }

    void PlayerTargetAI()
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

    void AgroTargetAI()
    {
        if (!canMove || rb == null)
        {
            return;
        }

        int direction = 1;

        if (foundPlayer != null)
        {
            Vector2 distance = Player.Instance.GetPosition() - rb.position;

            if (distance.x < 0)
            {
                direction = -1;
            }

            rb.velocity = new Vector2(direction * speed, rb.velocity.y);
        }
        // else {
        //     if (Physics2D.)

        //     rb.velocity = new Vector2(direction * speed, rb.velocity.y);
        // }
    }

    public virtual void Attack()
    {

    }

    public void SetMove(bool canMove)
    {
        this.canMove = canMove;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, agroRadius);
    }
}

public enum EnemyAIType
{
    PLAYER_TARGET, AGRO_TARGET
}
