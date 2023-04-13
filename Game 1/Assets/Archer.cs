using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    Rigidbody2D rb;

    public Collider2D playerCollider;

    Vector2 archer;
    Vector2 player;
    Vector2 direction;

    public Transform bow;
    public Transform shotPoint;
    public GameObject arrow;

    public float firerate = 2f;
    float nextFire;

    public int maxAmmo = 3;
    public int ammo;
    public float reloadTime = 3f;
    public float launchForce;
    //public float bloom = 2f;
    //float spray;
    public float maxDelay = 0.5f;
    float delay;
    public LayerMask layers;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ammo = maxAmmo;
        delay = Random.Range(0f, maxDelay);
    }

    void Update()
    {
        //Calculating Player Direction
        archer = transform.position;
        player = playerCollider.transform.position;
        direction = player - archer;

        bow.right = direction.normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 999f, layers);

        if(ammo > 0 && nextFire + delay < Time.time && hit.collider.CompareTag("Player"))
        {
            Shoot();
        }

        //If physics ray hits player, then update bow and shoot, upon loss of sight make them
        //return to rest position
    }
    void Shoot()
    {
        //spray = Random.Range(-bloom, bloom);

        GameObject newArrow = Instantiate(arrow, shotPoint.position, shotPoint.rotation);
        Physics2D.IgnoreCollision(newArrow.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        newArrow.GetComponent<Rigidbody2D>().velocity = bow.right * launchForce;

        StartCoroutine(Reload());

        ammo--;
        nextFire = Time.time + firerate;
        delay = Random.Range(0f, maxDelay);
    }
    IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);

        ammo++;
    }
}
