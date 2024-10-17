using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Color ActivatedSprite;
    bool isActivated;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isActivated)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                isActivated = true;
                SpriteRenderer renderer = GetComponent<SpriteRenderer>();
                renderer.color = ActivatedSprite;
            }
        }
    }
}
