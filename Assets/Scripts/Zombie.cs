using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Character
{

    public int Damage = 10;
    public float MinMovementSpeed = 1;
    public float MaxMovementSpeed = 3;

    public float AttackRange = 3;
    GameObject player;

    // Start is called before the first frame update
    protected override void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        movementSpeed = Random.Range(MinMovementSpeed, MaxMovementSpeed);

        base.Start();
    }

    // Update is called once per frame
    private void Update()
    {
        if(Vector2.Distance(transform.position, player.transform.position) < AttackRange)
        {
            SetState(CharacterState.Attack);

            transform.up = player.transform.position - transform.position;
        }
        else
        {
            SetState(CharacterState.Idle);
        }
    }

    private void FixedUpdate()
    {
        if (state == CharacterState.Attack)
        {
            body.MovePosition(transform.position + transform.up.normalized * movementSpeed * Time.deltaTime);
        }
    }
}
