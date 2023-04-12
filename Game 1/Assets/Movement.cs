using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb;

    float horizontal;
    public float speed = 7f;

    public float jumpVelocity = 15f;
    float jV;

    public float regularGravity = 3f;
    public float downGravity = 6.5f;
    bool canJump = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = regularGravity;
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            jV = jumpVelocity - rb.velocity.y;
            canJump = false;
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.gravityScale = downGravity;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            rb.gravityScale = regularGravity;
        }

    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y + jV);

        jV = 0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Jumpable Floor")
        {
            canJump = true;
        }
        if (collision.tag == "Jumpable Wall")
        {
            canJump = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Jumpable Floor")
        {
            canJump = false;
        }
    }
}
