using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

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
    bool canMove = true;

    int direction = 1;

    public List<AudioClip> hurtSounds;
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
    }

    void Update()
    {
        Jump();
        GroundCheck();
        DoAttack();
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
    }

    public void SetMove(bool canMove)
    {
        this.canMove = canMove;
        rb.isKinematic = !canMove;
    }
}
