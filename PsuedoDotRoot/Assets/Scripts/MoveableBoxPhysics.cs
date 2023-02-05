using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableBoxPhysics : MonoBehaviour
{
    private bool isGrounded;
    public float jumpForce = 10f;
    private float vertical;
    private const string VERTICAL = "Vertical";
    private Rigidbody2D rb;
    public bool isAttached = false;
    [SerializeField] private bool hasJumped = false;

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxis(VERTICAL);

        if (Input.GetButtonDown("Jump") && isGrounded && isAttached && hasJumped == false)
        {
            hasJumped = true;
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
        }

        
    }

    private void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Player")
        {
            if (vertical == 0f) { hasJumped = false; }
            isGrounded = true;
        }
    }
}
