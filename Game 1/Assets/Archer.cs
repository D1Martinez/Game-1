using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    Rigidbody2D rb;

    public Collider2D playerCollider;

    public Vector2 archer;
    public Vector2 player;
    public Vector2 direction;

    public Transform bow;
    public Transform shotPoint;
    public GameObject arrow;

    public float firerate = 2f;
    float nextFire;

    public int maxAmmo = 3;
    public int ammo;
    public float rechargeRate = 3f;
    public float launchForce;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ammo = maxAmmo;
    }

    void Update()
    {
        //Calculating Player Direction
        archer = transform.position;
        player = playerCollider.transform.position;
        direction = player - archer;

        bow.right = direction.normalized;
        
        if(ammo > 0 && nextFire < Time.time)
        {
            Shoot();
        }
    }
    void Shoot()
    {
        GameObject newArrow = Instantiate(arrow, shotPoint.position, bow.rotation);
        newArrow.GetComponent<Rigidbody2D>().velocity = bow.right * launchForce;

        StartCoroutine(Recharge());

        ammo--;
        nextFire = Time.time + firerate;
    }
    IEnumerator Recharge()
    {
        yield return new WaitForSeconds(rechargeRate);

        ammo++;
    }
}
