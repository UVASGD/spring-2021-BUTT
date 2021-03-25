using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float recoilForce = 10;
    public float bulletSpeed = 50;
    public float laserLength = 200;
    public int laserDamage = 1;
    public float laserDuration = .1F;
    public Rigidbody2D bullet;
    
    LineRenderer laser;
    float lastLaserFire = -1;
    // Start is called before the first frame update
    void Start()
    {
        laser = GetComponent<LineRenderer>();
        laser.positionCount = 2;
        //nothing.
    }

    // Update is called once per frame
    void Update()
    {
        if (lastLaserFire != -1 && Time.time - lastLaserFire > laserDuration) {
            laser.SetPositions(new Vector3[] { Vector3.zero, Vector3.zero });
        }
        if (Input.GetMouseButtonDown(0) && GetComponent<Parry>().ammo > 0)
            FireLaser();
    }
    void FireBullet()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 bulletDirection = worldPosition - transform.position;
        bulletDirection = new Vector3(bulletDirection.x, bulletDirection.y, 0);
        bulletDirection.Normalize();
        Rigidbody2D bulletClone = (Rigidbody2D)Instantiate(bullet, transform.position, transform.rotation);
        bulletClone.velocity = bulletSpeed * new Vector2(bulletDirection.x, bulletDirection.y);
        GetComponent<Parry>().ammo--;
        this.GetComponent<Rigidbody2D>().AddForce(-recoilForce * bulletDirection);
    }

    void FireLaser()
    {
        lastLaserFire = Time.time;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 bulletDirection = worldPosition - transform.position;
        bulletDirection = new Vector3(bulletDirection.x, bulletDirection.y, 0);
        Vector2 bulletDirection2D = new Vector2(bulletDirection.x, bulletDirection.y);
        bulletDirection.Normalize();
        RaycastHit2D[] laserHits = Physics2D.CapsuleCastAll(new Vector2(transform.position.x, transform.position.y), new Vector2(transform.localScale.x, transform.localScale.y),
            CapsuleDirection2D.Horizontal, 0, bulletDirection2D, laserLength);
        foreach (RaycastHit2D collision in laserHits){
            if (collision.collider != null && collision.collider.tag == "Enemy")
            {
                collision.collider.gameObject.SendMessage("Damage", laserDamage);
            }
        }

        GetComponent<Parry>().ammo--;
        Vector3 laserTip = bulletDirection * laserLength;
        laser.SetPositions(new Vector3[] { Vector3.zero, laserTip });
        this.GetComponent<Rigidbody2D>().AddForce(-recoilForce * bulletDirection);

    }
}