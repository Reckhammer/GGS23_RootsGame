using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Apple;

public class PlatformMove : MonoBehaviour
{
    public GameObject[] wayPoints;  // Positions that the platform will move between
    public float speed = 1f;        // How fast the platforms will move to get to waypoints
    private int current = 0;        // Current waypoint that this is moving towards

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
