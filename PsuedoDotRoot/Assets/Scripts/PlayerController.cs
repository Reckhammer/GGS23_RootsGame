using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpForce = 10f;
    public GameObject projectileCursor;
    public GameObject cam;
    public float projectileSpeed = 1f;
    public int maxJumpCount;
    public bool canMove = true;

    private Rigidbody2D rb;
    private Vector3 playerOffset = new Vector3 (0,1,0);
    private bool isGrounded;
    private int jumpCount;
    private Vector2 facing = new Vector2(1,0);
    private GameObject currentProjectile;
    private float horizontal;
    private float vertical;
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";
    private bool doorEntry = false;
    [SerializeField] private GameObject lastDoor;
    //private bool isSudo = false;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public event Action PlayerDied;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis(HORIZONTAL);
        vertical = Input.GetAxis(VERTICAL);

        if (horizontal != 0f || vertical != 0f)
        {
            if (Mathf.Abs(horizontal) == Mathf.Abs(vertical))
            {
                facing = new Vector2(horizontal,0);
            }
            else
            {
                facing = new Vector2(horizontal, vertical).normalized;
            }
        }

        if (Input.GetButtonDown("Jump") && (isGrounded || jumpCount > 0) && canMove == true)
        {
            Jump();
        }

        if (Input.GetButtonDown("Fire1") && currentProjectile == null && isGrounded)
        {
            
            currentProjectile = Instantiate(projectileCursor, transform.position + playerOffset, Quaternion.identity);
            Rigidbody2D rig = currentProjectile.GetComponent<Rigidbody2D>();
            rig.velocity = facing * projectileSpeed;
            canMove = false;
            cam.GetComponent<CameraController>().ChangeCameraTarget(currentProjectile.transform);
            
        }

        if (horizontal > 0f && canMove)
        {
            spriteRenderer.flipX = false;
        }
        else if (horizontal < 0f && canMove)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void FixedUpdate()
    {
        if (canMove == true)
        {
            rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
        }

        if (animator != null)
        {
            if (rb.velocity.magnitude > 0f)
                animator.SetBool("Move", true);
            else
                animator.SetBool("Move", false);
        }
    }

    private void Jump ()
    {
        if (animator != null)
        {
            animator.SetTrigger("Jump");
        }

        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isGrounded = false;
        jumpCount--;
    }

    private void ResetJumpParams()
    {
        isGrounded = true;
        jumpCount = maxJumpCount;
    }

    private void Death()
    {
        // Play Death Anim
        if ( animator != null )
        {
            animator.SetBool("Dead", true);
        }

        // Disable Player Movement
        canMove = false;
        isGrounded = false;

        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        collider.offset = new Vector2(0, .35f);
        collider.size = new Vector2(.75f, .75f);

        PlayerDied?.Invoke();
    }

    private void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            ResetJumpParams();
        }
        else if (collision.gameObject.tag == "Attachable" )
        {
            ResetJumpParams();

            Vector2 collisionDirection = this.transform.position - collision.transform.position;

            if (collisionDirection.y > 0) // collided object is below
            {
                //rb.gravityScale = 0;
                this.transform.SetParent(collision.transform);
            }
        }
        else if (collision.gameObject.tag =="Powerup")
        {
            //isSudo = true;
            
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            Death();
            
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Attachable")
        {
            this.transform.SetParent(null);
            //rb.gravityScale = 2;
        }
    }
    private void OnTriggerStay2D(Collider2D other) 
    {
        if (vertical > 0f && other.gameObject.tag == "Door" && doorEntry == false && canMove == true)
        {
            doorEntry = true;
            transform.position = other.GetComponent<ExitDoor>().connectedDoor.transform.position;
            lastDoor = other.gameObject;
        }
        if (vertical == 0f && doorEntry == true)
            {doorEntry = false;}
    }
}
