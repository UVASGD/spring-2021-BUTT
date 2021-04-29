using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float forceAmount = 3;
    public GameObject player;
    public float damage = 1;
    private Rigidbody2D rb;
    public Rigidbody2D bullet;
    public float bulletSpeed = 10;

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
        if (beatNum / 4 != 0)
        {
            FireBullet();
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
}
