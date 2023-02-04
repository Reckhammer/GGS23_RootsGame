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

    private GameObject cam;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position;// - target.position;
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.tag == "Attachable")
        {
            attached = collision.gameObject;
            transform.position = attached.transform.position + new Vector3 (0,0,-1);
            rb.velocity = new Vector3 (0,0,0);
        }
        //else
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
        
    }

    void OnDestroy() 
    {
        if (attached == null)
        {
            cam = GameObject.Find("Main Camera");
            cam.GetComponent<CameraController>().ChangeCameraTarget(GameObject.Find("Player").transform);
            player = GameObject.Find("Player");
            player.GetComponent<PlyaerController>().canMove = true;
        }
    }
}
