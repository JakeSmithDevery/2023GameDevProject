using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBlockResponse : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;

        if (tag == "tp")
        {
            TeleportTrigger teleportTrigger = collision.gameObject.GetComponent<TeleportTrigger>();
            transform.position = teleportTrigger.TeleportLocation.position;
        }
    }
}
