using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb;

    public Transform body;
    public Animator animator;

    float horizontal;
    public float speed = 7f;

    public float jumpVelocity = 15f;
    float jV;

    public float regularGravity = 3f;
    public float downGravity = 6.5f;
    bool canJump = true;

    public GameObject bow;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = regularGravity;
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            body.localScale = new Vector3(Input.GetAxisRaw("Horizontal") * 2, 2, 2);
        }
        if(Input.GetAxisRaw("Horizontal") == 0 && body.localScale.x == 2)
        {
            body.localScale = new Vector3(2, 2, 2);
        }
        if (Input.GetAxisRaw("Horizontal") == 0 && body.localScale.x == -2)
        {
            body.localScale = new Vector3(-2, 2, 2);
        }
        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }


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

        //if ray down has tag/layer platform, 
        //Physics2D.IgnoreCollision(oneself, with it, true)

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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Enemy Arrow"))
        {
            animator.SetBool("Alive", false);
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            gameObject.tag = "Dead";
            bow.SetActive(false);
            this.enabled = false;
        }
    }
}
