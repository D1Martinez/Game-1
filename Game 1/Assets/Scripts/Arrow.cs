using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody2D rb;

    bool hasHit = false;

    public float destroyTime = 3f;
    float clock = -1f;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hasHit = false;
    }
    private void Update()
    {
        if (hasHit == false)
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            clock = Time.time;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Death Animation, Fade  to black, Restart level.
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Kill enemy
            collision.gameObject.GetComponent<Health>().EnemyDies(destroyTime);
            Destroy(gameObject, destroyTime);
        }

        hasHit = true;

        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.simulated = false;

        Destroy(gameObject, destroyTime + 3f);
    }
}