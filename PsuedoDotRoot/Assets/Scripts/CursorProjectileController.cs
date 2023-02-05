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
    private bool destroying = true;
    private float destructTimer;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position;// - target.position;
        rb = GetComponent<Rigidbody2D>();

        destructTimer = 4f;
    }

    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    // Update is called once per frame
    
    void Update()
    {
        float horizontal = Input.GetAxis(HORIZONTAL);
        float vertical = Input.GetAxis(VERTICAL);
        
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


    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.tag == "Attachable" && attached == null)
        {
            attached = collision.gameObject;
            collision.GetComponent<MoveableBoxPhysics>().isAttached = true;
            attachedRB = collision.gameObject.GetComponent<Rigidbody2D>();
            transform.position = attached.transform.position + offset;
            rb.velocity = new Vector3 (0,0,0);
            destroying = false;
        }
        //else
        if (collision.gameObject.tag == "Ground" && attached == null)
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Mirror" && attached == null)
        {
            destructTimer = 4f;
            rb.velocity = Vector2.Reflect(rb.velocity, collision.transform.right);
        }
        
    }

    void OnDestroy() 
    {
        if (attached != null)
        {
            attached.GetComponent<MoveableBoxPhysics>().isAttached = false;
        }
        cam = GameObject.Find("Main Camera");
        if (cam == null) return;
        
        cam.GetComponent<CameraController>().ChangeCameraTarget(GameObject.Find("Player").transform);
        player = GameObject.Find("Player");
        player.GetComponent<PlayerController>().canMove = true;
    }
}
