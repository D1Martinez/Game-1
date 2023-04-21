using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public GameObject shooter;
    public GameObject arrow;
    public Transform shotPoint;
    public SpriteRenderer sr;
    public Sprite emptyBow, notPulled, semiPulled, fullyPulled;

    public float launchForce = 25f;

    public int maxAmmo = 3;
    public int ammo;
    public float reloadTime = 3f;
    bool isReloading = false;

    Vector2 bowPosition;
    Vector2 mousePosition;
    Vector2 direction;

    public float fireDelay = 0.25f;
    float nextFire = 0f;

    bool holding = false;
    float strength = 0f;

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
            holding = true;
            
        }
        if (holding)
        {
            strength += Time.deltaTime;
            strength = Mathf.Clamp(strength, 0, 1);
        }
        if (Input.GetMouseButtonUp(0) && ammo > 0 && nextFire < Time.time && holding)
        {
            holding = false;
            Shoot();
            strength = 0f;
        }
        if (ammo < maxAmmo && !isReloading)
        {
            StartCoroutine(Reload());
        }
        if (strength == 0)
        {
            sr.sprite = emptyBow;
        }
        if(strength > 0.5f && strength < 1f)
        {
            sr.sprite = semiPulled;
        }
        if (strength == 1f)
        {
            sr.sprite = fullyPulled;
        }


    }
    void Shoot()
    {
        GameObject newArrow = Instantiate(arrow, shotPoint.position, shotPoint.rotation);
        Physics2D.IgnoreCollision(newArrow.GetComponent<Collider2D>(), shooter.GetComponent<Collider2D>());
        //newArrow.GetComponent<Rigidbody2D>().velocity = transform.right * minLaunchForce;
        newArrow.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce * strength;

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