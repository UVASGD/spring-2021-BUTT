using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float bulletSpeed = 50;
    public Rigidbody2D bullet;
    // Start is called before the first frame update
    void Start()
    {
        //nothing.
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && GetComponent<Parry>().ammo > 0)
            Fire();
    }
    void Fire()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 bulletDirection = worldPosition - transform.position;
        bulletDirection.Normalize();
        Rigidbody2D bulletClone = (Rigidbody2D)Instantiate(bullet, transform.position, transform.rotation);
        bulletClone.velocity = bulletSpeed * new Vector2(bulletDirection.x, bulletDirection.y);
        GetComponent<Parry>().ammo--;
    }
}
