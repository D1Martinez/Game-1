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

    public GameObject bow, bowBar;

    public string lastWallTouched;

    public float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    public float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    public Transform groundCheck;
    public LayerMask groundLayer;

    public bool isWallSliding;
    public float wallSlidingSpeed = 2f;
    public Transform wallCheck;
    public LayerMask wallLayer;

    public GameObject currentPlatform;
    public Collider2D playerCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = regularGravity;
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        //Visuals, Flip and Running Animation
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
            animator.SetBool("Moving X", true);
        }
        else
        {
            animator.SetBool("Moving X", false);
        }

        if (IsGrounded() || IsWalled())
        {
            coyoteTimeCounter = coyoteTime;
            
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && coyoteTimeCounter > 0f && jumpBufferCounter > 0f)
        {
            jV = jumpVelocity - rb.velocity.y;
            canJump = false;
        }
        if (Input.GetKeyUp(KeyCode.Space) && coyoteTimeCounter > 0f && jumpBufferCounter > 0f)
        {
            coyoteTimeCounter = 0f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.gravityScale = downGravity;
            if(currentPlatform != null)
            {
                StartCoroutine(DisableCollsion());
            }
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            rb.gravityScale = regularGravity;
        }
        if(Input.GetKeyDown(KeyCode.S) && currentPlatform != null)
        {
            StartCoroutine(DisableCollsion());
        }

        //if ray down has tag/layer platform, 
        //Physics2D.IgnoreCollision(oneself, with it, true)
        WallSlide();
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y + jV);

        jV = 0f;

        animator.SetFloat("Y Velocity", rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Jumpable Floor")
        {
            canJump = true;
            lastWallTouched = collision.name;
        }
        if (collision.tag == "Jumpable Wall" && collision.name != lastWallTouched)
        {
            canJump = true;
            lastWallTouched = collision.name;
        }
        if (collision.CompareTag("Deadly"))
        {
            Die();
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
            Die();
        }
        if (collision.gameObject.CompareTag("Platform"))
        {
            currentPlatform = collision.gameObject;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            currentPlatform = null;
        }
    }
    IEnumerator DisableCollsion()
    {
        Collider2D platformCol = currentPlatform.GetComponent<Collider2D>();

        Physics2D.IgnoreCollision(playerCollider, platformCol);
        yield return new WaitForSeconds(0.25f);
        Physics2D.IgnoreCollision(playerCollider, platformCol, false);
    }
    public void Die()
    {
        animator.SetBool("Alive", false);
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        gameObject.tag = "Dead";
        bow.SetActive(false);
        bowBar.SetActive(false);
        this.enabled = false;
    }
    bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }
    void WallSlide()
    {
        if (Input.GetKey(KeyCode.S))
        {
            return;
        }
        if (IsWalled() && !IsGrounded() && horizontal != 0f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
            animator.SetBool("Wall Sliding", true);
            return;
        }
        isWallSliding = false;
        animator.SetBool("Wall Sliding", false);
    }
}
