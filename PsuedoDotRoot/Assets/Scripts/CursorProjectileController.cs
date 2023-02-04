using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorProjectileController : MonoBehaviour
{
    //public Transform target;
    public float smoothing = 5f;
    public GameObject attached;
    public GameObject player;
    public Rigidbody2D rb;
    public Rigidbody2D attachedRB;
    public float jumpForce = 7f;
    public float moveSpeed = 10f;

    private GameObject cam;
    private Vector3 offset = new Vector3 (0,0,-1);
    private bool destroying;
    private float destructTimer;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position;// - target.position;
        rb = GetComponent<Rigidbody2D>();

        destructTimer = 5f;
        //Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        if (attached != null)
        {
            attachedRB.transform.position += new Vector3(horizontal * moveSpeed * Time.deltaTime, 0);
        }

        if (destroying == true) { destructTimer -= 1 * Time.deltaTime; }
        if (destructTimer < 0)  { Destroy(gameObject); }
        if (attached != null) { transform.position = attached.transform.position + new Vector3 (0,0,-1);}

        if (Input.GetButtonDown("Fire1") && attached != null)
        {
            Destroy(gameObject);
        }


        /*if (Input.GetButtonDown("Jump") && attached != null)
        {
            attachedRB.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }*/

        

    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.tag == "Attachable")
        {
            attached = collision.gameObject;
            attachedRB = collision.gameObject.GetComponent<Rigidbody2D>();
            transform.position = attached.transform.position + offset;
            rb.velocity = new Vector3 (0,0,0);
            destroying = false;
        }
        //else
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
        
    }

    void OnDestroy() 
    {
            cam = GameObject.Find("Main Camera");
            cam.GetComponent<CameraController>().ChangeCameraTarget(GameObject.Find("Player").transform);
            player = GameObject.Find("Player");
            player.GetComponent<PlayerController>().canMove = true;
    }
}
