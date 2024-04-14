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

    protected Rigidbody2D rb;
    protected Player foundPlayer;

    bool canMove = true;

    public List<AudioClip> hurtSounds;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        GetComponent<Health>().onHit = Hit;
        GetComponent<Health>().onDeath = OnDeath;
    }

    void Update()
    {
        foundPlayer = null;

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
                    cooldownTimer = attackCooldown;
                    Attack();
                }
            }
            else
            {
                cooldownTimer = 0f;
            }
        }

        EnemyUpdate();
    }

    public virtual void EnemyUpdate()
    {

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
    }

    public virtual void Attack()
    {
        if (foundPlayer != null)
        {
            foundPlayer.GetComponent<Health>().TakeDamage(attack);
        }

        if (aiType == EnemyAIType.PLAYER_TARGET)
        {
            Player.Instance.AddKnockback();
        }
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

    void Hit()
    {
        int randIndex = Random.Range(0, hurtSounds.Count);
        AudioManager.Instance.PlaySound(hurtSounds[randIndex]);
    }

    void OnDeath()
    {
        int random = Random.Range(0, 100);

        if (random <= 5)
        {
            Player.Instance.GetComponent<Health>().Heal(10);
        }
    }
}

public enum EnemyAIType
{
    PLAYER_TARGET, AGRO_TARGET
}
