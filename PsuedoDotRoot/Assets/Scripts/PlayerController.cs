using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpForce = 10f;
    public GameObject projectileCursor;
    public GameObject cam;
    public float projectileSpeed = 1f;
    public int maxJumpCount;
    public string state; 
    public bool canMove = true;

    private Rigidbody2D rb;
    private bool isGrounded;
    private int jumpCount;
    private Vector2 facing;
    private GameObject currentProjectile;
    private float horizontal;
    private float vertical;
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";
    private bool doorEntry = false;
    [SerializeField] private GameObject lastDoor;
    private bool isSudo = false;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
            
            currentProjectile = Instantiate(projectileCursor, transform.position, Quaternion.identity);
            Rigidbody2D rig = currentProjectile.GetComponent<Rigidbody2D>();
            rig.velocity = facing * projectileSpeed;
            canMove = false;
            cam.GetComponent<CameraController>().ChangeCameraTarget(currentProjectile.transform);
            
        }
    }

    private void FixedUpdate()
    {
        if (canMove == true)
        {
            rb.position += new Vector2(horizontal * moveSpeed * Time.deltaTime, 0);
        }

        
    }

    private void Jump ()
    {
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        isGrounded = false;
        jumpCount--;
    }

    private void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag =="Attachable")
        {
            isGrounded = true;
            jumpCount = maxJumpCount;

            this.transform.SetParent(collision.transform);
        }
        if (collision.gameObject.tag =="Powerup")
        {
            isSudo = true;
            
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Attachable")
        {
            this.transform.SetParent(null);
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
