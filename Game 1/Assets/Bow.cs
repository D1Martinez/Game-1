using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public GameObject shooter;
    public GameObject arrow;
    public Transform shotPoint;

    public float launchForce;

    public int maxAmmo = 3;
    public int ammo;
    public float rechargeRate = 3f;
    bool isRecharging = false;

    Vector2 bowPosition;
    Vector2 mousePosition;
    Vector2 direction;
    private void Start()
    {
        ammo = maxAmmo;
        isRecharging = false;
    }

    private void Update()
    {
        bowPosition = transform.position;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePosition - bowPosition;
        transform.right = direction;

        if (Input.GetMouseButtonDown(0) && ammo > 0)
        {
            Shoot();
        }
        if(ammo < maxAmmo && !isRecharging)
        {
            StartCoroutine(Recharge());
        }
    }
    void Shoot()
    {
        GameObject newArrow = Instantiate(arrow, shotPoint.position, shotPoint.rotation);
        Physics2D.IgnoreCollision(newArrow.GetComponent<Collider2D>(), shooter.GetComponent<Collider2D>());
        newArrow.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce;

        ammo--;
    }
    IEnumerator Recharge()
    {
        isRecharging = true;

        yield return new WaitForSeconds(rechargeRate);

        isRecharging = false;
        ammo++;
    }
}