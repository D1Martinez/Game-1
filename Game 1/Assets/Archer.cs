using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    Rigidbody2D rb;
    ColliderDistance2D cd;

    public Collider2D playerCollider;
    public float distance;

    public Vector2 archer;
    public Vector2 player;
    public Vector2 result;

    public float chaseSpeed = 0.001f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Calculating Distance to Player
        cd = rb.Distance(playerCollider);
        distance = cd.distance;

        //Calculating Player Direction
        archer = transform.position;
        player = playerCollider.transform.position;

        result = player - archer;

        //Debug.Log(result.magnitude);
        
        result.Normalize();

        
        
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + result.normalized * chaseSpeed);
    }
}
