using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 5f;

    Rigidbody2D rb;
    bool isGrounded = true;

    public LayerMask groundLayer;

    Ability currentAbility;

    public static Player _instance;
    public static Player Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Player>();
            }

            return _instance;
        }
    }

    float attackCooldown = 0f;

    float maxKnockbackTimer = 0.5f;
    float knockbackTimer;
    bool isKnockback = false;

    bool canMove = true;

    int direction = 1;

    public List<AudioClip> hurtSounds;
    public AudioClip healSound;
    public AudioClip deathSound;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Ability startAbility = new Sword();
        currentAbility = startAbility;
        Inventory.Instance.AddAbility(startAbility);

        UI_Hotbar.Instance.SetActive(0);
        UI_Hotbar.Instance.SetSlot(1, null);
        UI_Hotbar.Instance.SetSlot(2, null);

        GetComponent<Health>().onHit += Hit;
        GetComponent<Health>().onDeath += Die;
        GetComponent<Health>().onHeal += Heal;
    }

    void Update()
    {
        Jump();
        GroundCheck();
        DoAttack();

        if (isKnockback)
        {
            knockbackTimer -= Time.deltaTime;

            if (knockbackTimer <= 0f)
            {
                isKnockback = false;
                canMove = true;
            }
        }
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if (!canMove)
        {
            return;
        }

        float moveX = Input.GetAxisRaw("Horizontal");

        if (Physics2D.CapsuleCast(rb.position, new Vector2(0.5f, 1.5f), CapsuleDirection2D.Vertical, 0, new Vector2(moveX, 0f), 0.2f, groundLayer))
        {
            moveX = 0;
        }

        if (moveX < 0)
        {
            direction = -1;
            GetComponentInChildren<SpriteRenderer>().flipX = true;
        }
        else if (moveX > 0)
        {
            direction = 1;
            GetComponentInChildren<SpriteRenderer>().flipX = false;
        }

        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);
    }

    void Jump()
    {
        if (!canMove)
        {
            return;
        }

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.down, 1.2f, groundLayer);
        if (!hit.collider)
        {
            isGrounded = false;
        }
        else
        {
            isGrounded = true;
        }
    }

    public Ability GetAbility()
    {
        return currentAbility;
    }

    public void SetAbility(Ability ability)
    {
        currentAbility = ability;
        attackCooldown = 0f;
    }

    public void DoAttack()
    {
        attackCooldown -= Time.deltaTime;

        if (attackCooldown <= 0f)
        {
            if (Input.GetButtonDown("Attack") && currentAbility != null)
            {
                attackCooldown = currentAbility.cooldown;
                currentAbility.Attack();
            }
        }
    }

    public Vector2 GetPosition()
    {
        return rb.position;
    }

    public int GetLayerMask()
    {
        int layerMask = 1 << gameObject.layer;

        return layerMask;
    }

    public int GetDirection()
    {
        return direction;
    }

    void Hit()
    {
        if (GetComponent<Health>().GetCurrentHealth() > 0)
        {
            int randIndex = UnityEngine.Random.Range(0, hurtSounds.Count);
            AudioManager.Instance.PlaySound(hurtSounds[randIndex]);
        }
    }

    void Die()
    {
        AudioManager.Instance.PlaySound(deathSound);
        SceneManager.LoadScene("Game Over");
    }

    void Heal()
    {
        AudioManager.Instance.PlaySound(healSound);
    }

    public void SetMove(bool canMove)
    {
        this.canMove = canMove;
        rb.isKinematic = !canMove;
    }

    public void AddKnockback()
    {
        knockbackTimer = maxKnockbackTimer;

        isKnockback = true;
        canMove = false;

        StartCoroutine(DoKnockback());
    }

    IEnumerator DoKnockback()
    {
        yield return new WaitForNextFrameUnit();

        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(6f * -direction, 0f), ForceMode2D.Impulse);
    }
}
