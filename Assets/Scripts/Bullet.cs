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
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hitInfo.collider != null)  
        {
            DestroyBullet();
        }
        
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);  
    }
}
