using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    public GameObject[] wayPoints;  // Positions that the platform will move between
    public float speed = 1f;        // How fast the platforms will move to get to waypoints
    private int currentPos = 0;        // Current waypoint that this is moving towards

    private Rigidbody2D myRigidbody2D;

    private void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if ( Vector2.Distance(transform.position, wayPoints[currentPos].transform.position) <= 0f )
        {
            currentPos++;
            if ( currentPos >= wayPoints.Length )
            {
                currentPos = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, wayPoints[currentPos].transform.position, Time.fixedDeltaTime * speed);
    }
}
