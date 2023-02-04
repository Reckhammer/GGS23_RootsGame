using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorProjectileController : MonoBehaviour
{
    //public Transform target;
    public float smoothing = 5f;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position;// - target.position;
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Vector3 targetCameraPosition = transform.position + offset;
        //transform.position = Vector3.Lerp(transform.position, targetCameraPosition, smoothing * Time.deltaTime);

    }
}
