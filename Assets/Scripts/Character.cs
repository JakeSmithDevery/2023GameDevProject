using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum CharacterState
{
    Idle,
    Attack
}
public class Character : MonoBehaviour
{
    public CharacterState state;
    public Sprite IdleSprite;
    public Sprite AttackSprite;
    public float movementSpeed = 5;
    protected Rigidbody2D body;

    SpriteRenderer spriteRenderer;

    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        state = CharacterState.Idle;
        SetState(state);
    }

    // Update is called once per frame
    public void SetState(CharacterState newState)
    {
        state = newState;

        if(newState == CharacterState.Idle)
        {
            spriteRenderer.sprite = IdleSprite;
        }

        else if (newState == CharacterState.Attack)
        {
            spriteRenderer.sprite = AttackSprite;
        }
    }
}
