using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform StartPoint;
    public Transform EndPoint;

    public float MovementSpeed;
    Vector3 direction;
    public bool isMovingToEnd = true;
    GameObject player;
    Lvl2Player lvl2Player;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        lvl2Player = player.gameObject.GetComponent<Lvl2Player>();
    }
    void Update()
    {
        if (EndPoint != null && StartPoint != null)
        {
            if (isMovingToEnd)
            {
                direction = EndPoint.position - transform.position;

                if (Vector2.Distance(transform.position, EndPoint.position) <= 0.25f)
                    isMovingToEnd = false;
            }
            else
            {
                direction = StartPoint.position - transform.position;

                if (Vector2.Distance(transform.position, StartPoint.position) <= 0.25f)
                    isMovingToEnd = true;
            }

            direction.Normalize();
            transform.position += direction * MovementSpeed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(transform);

            lvl2Player.chaseover = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(null);

            lvl2Player.chaseover = false;
        }
    }
}
