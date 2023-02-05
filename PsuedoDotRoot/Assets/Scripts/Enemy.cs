using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    enum EnemyStatus
    {
        Waiting,
        Moving
    }

    public Rigidbody2D myRigidbody2D;   // Reference to this object's Rigidbody component
    public GameObject[] waypoints;      // Positions that this will move between
    public float speed = 10f;           // How fast this will move between points
    public float waypointDelay = 2f;    // Time to wait before moving to other waypoint

    private EnemyStatus currentStatus = EnemyStatus.Waiting;
    private int currentPos;
    private float timeElapsed = 0f;

    private void Awake()
    {
        if ( myRigidbody2D == null )
            myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if ( currentStatus == EnemyStatus.Moving )
        {
            EnemyMovement();
        }
        else if ( currentStatus == EnemyStatus.Waiting )
        {
            WaitForDelay();
        }
    }

    private void EnemyMovement()
    {
        if ( Vector2.Distance(transform.position, waypoints[currentPos].transform.position) <= 0f )
        {
            // Destination is Reached
            currentPos++;
            if (currentPos >= waypoints.Length)
            {
                currentPos = 0;
            }

            currentStatus = EnemyStatus.Waiting;

            return;
        }

        myRigidbody2D.position = Vector2.MoveTowards(transform.position, waypoints[currentPos].transform.position, Time.deltaTime * speed);
    }

    private void WaitForDelay()
    {
        timeElapsed += Time.deltaTime;

        if ( timeElapsed >= waypointDelay )
        {
            timeElapsed = 0f;
            currentStatus = EnemyStatus.Moving;
            return;
        }
    }
}
