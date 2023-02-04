using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour
{
    public GameObject targetObject;
    public string targetTag = "Player";
    public float followSpeed = 2f;

    // Update is called once per frame
    void Update()
    {
        if ( targetObject != null )
        {
            transform.position = Vector2.MoveTowards(transform.position, targetObject.transform.position, Time.deltaTime * followSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == targetTag)
            targetObject = collision.gameObject;
    }
}
