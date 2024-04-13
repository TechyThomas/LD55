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

    public static Player instance;

    float attackCooldown = 0f;
    bool canAttack = true;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Ability startAbility = new Sword();
        currentAbility = startAbility;
        Inventory.instance.AddAbility(startAbility);
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
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
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, rb.velocity.y);
    }

    void Jump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
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
            if (Input.GetMouseButtonDown(0) && currentAbility != null)
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
}
