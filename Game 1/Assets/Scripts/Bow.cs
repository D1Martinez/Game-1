using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public GameObject shooter;
    public GameObject arrow;
    public Transform shotPoint;
    public SpriteRenderer sr;
    public Sprite emptyBow;
    public Sprite loadedBow;

    public float launchForce;

    public int maxAmmo = 3;
    public int ammo;
    public float reloadTime = 3f;
    bool isReloading = false;

    Vector2 bowPosition;
    Vector2 mousePosition;
    Vector2 direction;

    public float fireDelay = 0.25f;
    float nextFire = 0f;
    private void Start()
    {
        ammo = maxAmmo;
        isReloading = false;
        //Cursor.visible = false;
    }

    private void Update()
    {
        bowPosition = transform.position;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePosition - bowPosition;
        transform.right = direction;

        if (Input.GetMouseButtonDown(0) && ammo > 0 && nextFire < Time.time)
        {
            Shoot();
        }
        if(ammo < maxAmmo && !isReloading)
        {
            StartCoroutine(Reload());
        }

        if(ammo > 0 && nextFire < Time.time)
        {
            sr.sprite = loadedBow;
        }
        if(ammo == 0 || nextFire > Time.time)
        {
            sr.sprite = emptyBow;
        }

    }
    void Shoot()
    {
        GameObject newArrow = Instantiate(arrow, shotPoint.position, shotPoint.rotation);
        Physics2D.IgnoreCollision(newArrow.GetComponent<Collider2D>(), shooter.GetComponent<Collider2D>());
        newArrow.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce;

        ammo--;

        nextFire = Time.time + fireDelay;
    }
    IEnumerator Reload()
    {
        isReloading = true;

        yield return new WaitForSeconds(reloadTime);

        isReloading = false;
        ammo++;
    }
}