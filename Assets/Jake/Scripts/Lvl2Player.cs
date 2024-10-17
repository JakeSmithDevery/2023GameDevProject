using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lvl2Player : MonoBehaviour
{
    public float MovementSpeed;
    public float AirMovementSpeed;
    int MaxDash = 1;
    int currentDash;
    public int dashSpeed = 10;

    #region Old COde
    public float JumpForce;

    Rigidbody2D body;
    float horizontal;
    Vector2 horizontalForce;
    Vector2 verticalForce;
    public bool isOnGround;

    public float MaxSlope = 0.5f;
    public int MaxJumps = 2;
    int currentJumps;

    Vector2 checkpointPosition;
    public int CollectableCount;
    #endregion

    SpriteRenderer spriteRenderer;
    public float MaxMovementSpeed = 10;
    Animator animator;
    public bool chaseover = true;
    public AudioSource Dash;
    public AudioSource Jump;
    public AudioSource Death;


    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        verticalForce.y = JumpForce;
        checkpointPosition = transform.position;
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (isOnGround)
        {
            horizontalForce.x = horizontal * MovementSpeed * Time.deltaTime;
        }
        else
        {
            horizontalForce.x = horizontal * AirMovementSpeed * Time.deltaTime;
        }

        body.AddForce(horizontalForce);

        if (Input.GetKeyDown(KeyCode.Space) && CanJump())
        {
            Jump.Play();
            body.AddForce(verticalForce, ForceMode2D.Impulse);
            isOnGround = false;
            currentJumps++;
        }

        if (Input.GetKeyDown(KeyCode.E) && CanDash())
        {
            Dash.Play();
            body.AddForce(horizontalForce * dashSpeed, ForceMode2D.Impulse);
            isOnGround = false;
            currentDash++;
        }
        if(horizontal == 0 && body.velocity.y == 0)
        {
            animator.Play("Player_Idle");
        }

        if (horizontal > 0)
        {
            spriteRenderer.flipX = false;
            
            
        }
        
        if (horizontal > 0 && body.velocity.y == 0 || horizontal < 0 && body.velocity.y == 0 )
        {
            animator.Play("Player_Walk");
        }

        else if (horizontal < 0)
        {
            spriteRenderer.flipX = true;
            
        }

        if (!isOnGround)
        {

            if (body.velocity.y > 0)
            {
                animator.Play("Player_Jump");
            }
            
        }
        else
        {
            spriteRenderer.flipY = false;
            
        }

        //Limit the velocity of the player to be MaxMovementSpeed
        body.velocity = Vector2.ClampMagnitude(body.velocity, MaxMovementSpeed);
    }

    bool CanJump()
    {
        return isOnGround || currentJumps < MaxJumps;
    }

    bool CanDash()
    {
        return isOnGround || currentDash < MaxDash;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckIfOnGround(collision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Death"))
        {
            Death.Play();
            body.velocity = Vector2.zero;
            transform.position = checkpointPosition;
        }

        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            checkpointPosition = collision.gameObject.transform.position;
        }

        if (collision.gameObject.CompareTag("StartPoint"))
        {
            chaseover = false;
        }

        if (collision.gameObject.CompareTag("EndChase"))
        {
            chaseover = true;
            MovementSpeed = 400;
        }


        if (collision.gameObject.CompareTag("Change level"))
        {
            SceneManager.LoadScene("Level_3");
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
                    currentDash = 0;
            }
    }


}
