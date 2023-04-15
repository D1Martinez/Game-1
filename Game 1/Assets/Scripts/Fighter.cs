using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    Rigidbody2D rb;
    ColliderDistance2D cd;

    public Collider2D playerCollider;
    public float distance;

    public Vector2 fighter;
    public Vector2 player;
    public Vector2 direction;

    public float speed = 5f;

    public float jumpV = 15f;
    float jV;

    public bool canJump = true;
    float intGravity;
    public float downGravity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        intGravity = rb.gravityScale;
    }

    void Update()
    {
        //Calculating Distance to Player
        cd = rb.Distance(playerCollider);
        distance = cd.distance;

        //Calculating Player Direction
        fighter = transform.position;
        player = playerCollider.transform.position;

        direction = player - fighter;

        //Debug.Log(result.magnitude);

        direction.Normalize();

        if (direction.y > 0.5f && canJump)
        {
            jV = jumpV - rb.velocity.y;
            canJump = false;
            rb.gravityScale = intGravity;
        }
        if (direction.y < -0.5f)
        {
            rb.gravityScale = downGravity;
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(direction.x * speed, rb.velocity.y + jV);

        jV = 0f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Jumpable Floor")
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
