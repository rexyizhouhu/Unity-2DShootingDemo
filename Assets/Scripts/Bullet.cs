using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed;  
    public float lifetime;   
    public float distance;
    public LayerMask whatIsSolid;
    public bool isCharFacingRight = true;
    public bool random = true;
    public PlayerController shooter;

    Rigidbody2D rb;

    void Start()
    {
        Invoke("DestroyBullet", lifetime);
        rb = GetComponent<Rigidbody2D>();

        if (random) transform.Rotate(transform.forward, Random.Range(-5.0f, 5.0f));
        rb.velocity = transform.right * speed;
    }

    void Update()
    {
        //Debug.Log(transform.rotation);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(shooter.attack);
        }

        if (shooter.attack < 100) DestroyBullet();
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);  
    }
}
