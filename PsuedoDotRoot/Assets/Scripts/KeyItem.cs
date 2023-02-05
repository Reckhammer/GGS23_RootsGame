using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class KeyItem : MonoBehaviour
{
    public GameObject targetObject;         // Object that this is following
    public string targetTag = "Player";     // Tag of object to follow
    public float followSpeed = 5f;          // How fast this will move to follow target
    public float distFromTarget = .5f;      // Distance from target to stop moving

    // Update is called once per frame
    void Update()
    {
        if ( targetObject != null )
        {
            if ( Vector2.Distance(transform.position, targetObject.transform.position) > distFromTarget )
                transform.position = Vector2.MoveTowards(transform.position, targetObject.transform.position, Time.deltaTime * followSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == targetTag)
        {
            targetObject = collision.gameObject;
            // Send event/notification that key item was picked up
        }
    }
}
