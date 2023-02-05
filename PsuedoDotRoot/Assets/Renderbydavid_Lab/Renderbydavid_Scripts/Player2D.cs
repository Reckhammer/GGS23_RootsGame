using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D : MonoBehaviour
{
    public float MoveSpeed;
    public float JumpPower;
    void Update()
    {
        float MoveX = Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime;

        GetComponent<Rigidbody2D>().AddForce(Vector2.right * MoveX, ForceMode2D.Impulse);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
        }
    }
}
