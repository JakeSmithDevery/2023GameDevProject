using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lvl_1_player : MonoBehaviour
{

    public float MovementSpeed;
    public float AirMovementSpeed;

    #region Old COde
    public float JumpForce;

    Rigidbody2D body;
    float horizontal;
    Vector2 horizontalForce;
    Vector2 verticalForce;
    public bool isOnGround;
    public bool IsOnWall;

    public float MaxSlope = 0.5f;
    public int MaxJumps = 2;
    public int MinJumps = 0;
    public int currentJumps;

    Vector2 checkpointPosition;
    public int CollectableCount;
    #endregion

    Animator animator;
    SpriteRenderer spriteRenderer;
    public float MaxMovementSpeed = 10;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        verticalForce.y = JumpForce;
        checkpointPosition = transform.position;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (isOnGround)
            horizontalForce.x = horizontal * MovementSpeed * Time.deltaTime;
        else
            horizontalForce.x = horizontal * AirMovementSpeed * Time.deltaTime;

        body.AddForce(horizontalForce);

        if (Input.GetKeyDown(KeyCode.Space) && CanJump())
        {
            body.AddForce(verticalForce, ForceMode2D.Impulse);
            isOnGround = false;
            currentJumps++;
        }

        if (horizontal > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (horizontal < 0)
        {
            spriteRenderer.flipX = false;
        }

        
        if (isOnGround)
        {
            if (horizontal < 0)
            {
                animator.Play("walking");
                spriteRenderer.flipX = true;
            }
            else if (horizontal > 0)
            {
                animator.Play("walking");
                spriteRenderer.flipX = false;
            }
            else if (horizontal == 0)
            {
                animator.Play("idle");
            }
        }
        if (!isOnGround)
        {
            if (body.velocity.y <0)
            {
                animator.Play("jump");
                spriteRenderer.flipX = false;
            }
            else if (body.velocity.y > 0)
            {
                animator.Play("jump");
                spriteRenderer.flipX= true; 
            }
            else if (horizontal == 0)
            {
                animator.Play("jump");
            }
        }
        
        body.velocity = Vector2.ClampMagnitude(body.velocity, MaxMovementSpeed);
    }

    bool CanJump()
    {
        return isOnGround || currentJumps < MaxJumps;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckIfOnGround(collision);
        CheckIfonWall(collision);   
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("end_lvl"))
        {
            SceneManager.LoadScene("Level_2");
        }
    }

    void CheckIfOnGround(Collision2D collision)
    {
        if (!isOnGround)
            if (collision.contacts.Length > 0)
            {
                ContactPoint2D contact = collision.contacts[0];
                //how close does the normal match the up direction
                float dot = Vector2.Dot(contact.normal, Vector2.up);
                isOnGround = dot >= MaxSlope;

                if (isOnGround)
                    currentJumps = 0;
            }
    }

    void CheckIfonWall(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
                IsOnWall = true;
                currentJumps = 0;
                MaxJumps = 1;
        }
        else
        {
            MaxJumps = 2;
            IsOnWall = false;
        }

    }
} 
