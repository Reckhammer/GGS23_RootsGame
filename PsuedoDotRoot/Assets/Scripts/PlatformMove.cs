using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Apple;

public class PlatformMove : MonoBehaviour
{
    public GameObject[] wayPoints;
    public float speed = 1f;
    private int current = 0;

    private Rigidbody2D myRigidbody2D;

    private void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if ( Vector2.Distance(transform.position, wayPoints[current].transform.position) <= 0f )
        {
            current++;
            if ( current >= wayPoints.Length )
            {
                current = 0;
            }
        }

        myRigidbody2D.position = Vector2.MoveTowards(transform.position, wayPoints[current].transform.position, Time.deltaTime * speed);
    }
}
