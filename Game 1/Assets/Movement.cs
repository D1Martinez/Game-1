using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb;

    public float horizontal;
    public float speed = 1f;

    public float jumpV = 15f;
    float jV;

    public bool canJump = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            jV = jumpV - rb.velocity.y;
            canJump = false;
        }
}
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y + jV);

        jV = 0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Jumpable")
        {
            canJump = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Jumpable")
        {
            canJump = false;
        }
    }
}
