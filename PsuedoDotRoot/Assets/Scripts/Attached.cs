using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attached : MonoBehaviour
{
    public Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Attachable" || other.gameObject.tag == "Player")
        {
            Vector2 collisionDirection = this.transform.position - other.transform.position;

            if (collisionDirection.y > 0) // collided object is below
            {
                rb.gravityScale = 0;
                this.transform.SetParent(other.transform);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Attachable")
        {
            this.transform.SetParent(null);
            rb.gravityScale = 1;
        }
    }
}
