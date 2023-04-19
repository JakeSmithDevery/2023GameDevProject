using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
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

    public float MaxSlope = 0.5f;
    public int MaxJumps = 2;
    int currentJumps;

    Vector2 checkpointPosition;
    public int CollectableCount;
    #endregion

    SpriteRenderer spriteRenderer;
    public float MaxMovementSpeed = 10;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        verticalForce.y = JumpForce;
        checkpointPosition = transform.position;
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

        if(horizontal > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if(horizontal < 0)
        {
            spriteRenderer.flipX = false;
        }

        if(!isOnGround)
        {

            if (body.velocity.y > 0)
            {
                spriteRenderer.flipY = false;
            }
            else if(body.velocity.y < 0)
            {
                spriteRenderer.flipY = true;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckIfOnGround(collision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

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
}
