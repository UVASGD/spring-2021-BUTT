using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_dos : MonoBehaviour
{
    public float forceAmount = 3;
    public GameObject player;
    public float damage = 1;
    private Rigidbody2D rb;
    public Rigidbody2D bullet;
    LineRenderer laser;
    private bool laserFire = false;
    public float bulletSpeed = 50;
    public float laserLength = 200;
    public int laserDamage = 1;
   // public float shootOnBeatLeniency = .06F;
   /// public float laserDuration = .1F;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Setup(GameObject player)
    {
        rb = GetComponent<Rigidbody2D>();
        this.player = player;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnBeat(int beatNum)
    {
        
        if (beatNum % 2 == 0)
        {
            laserFire = true;
            FireLaser();
        }
        else
        {
            laserFire = false;
        }

        if (laserFire)
        {
            FireLaser();
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.SendMessage("Damage", damage);
        }
    }

    void FireBullet()
    {
        //Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Transform playerTransform = player.GetComponent<Transform>();
        Vector3 bulletDirection = playerTransform.position - transform.position;
        bulletDirection = new Vector3(bulletDirection.x, bulletDirection.y, 0);
        bulletDirection.Normalize();
        Rigidbody2D bulletClone = (Rigidbody2D)Instantiate(bullet, transform.position, transform.rotation);
        bulletClone.velocity = bulletSpeed * new Vector2(bulletDirection.x, bulletDirection.y);

        
    }
    void FireLaser()
    {
       // lastLaserFire = Time.time;
        //Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 bulletDirection = transform.position;
        bulletDirection = new Vector3(bulletDirection.x, bulletDirection.y, 0);
        Vector2 bulletDirection2D = new Vector2(bulletDirection.x, bulletDirection.y);
        bulletDirection.Normalize();

        RaycastHit2D[] laserHits = Physics2D.CapsuleCastAll(new Vector2(transform.position.x, transform.position.y), new Vector2(transform.localScale.x, transform.localScale.y),
            CapsuleDirection2D.Horizontal, 0, bulletDirection2D, laserLength);

        foreach (RaycastHit2D collision in laserHits)
        {
            if (collision.collider != null && collision.collider.tag == "Player")
            {
                collision.collider.gameObject.SendMessage("Damage", laserDamage);
      
            }
        }
        GetComponent<Parry>().ammo--;
        Vector3 laserTip = bulletDirection * laserLength;
        laserTip.z = 6;
        laser.SetPositions(new Vector3[] { new Vector3(0, 0, 6), laserTip });
        

    }
}
