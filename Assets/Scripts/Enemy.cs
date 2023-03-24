using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float speed = 8;
    public float health = 1000;
    public float attack = 5;
    

    bool facingRight = true;
    Rigidbody2D rb;
    Animator animator;
    Vector2 movement;
    Vector3 direction;
    readonly float eps = 0.1f;
    float timeToDamege = .5f;
    float damageFreq = 1f;
    bool dead = false;
    float freezingTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (rb && Mathf.Abs(direction.magnitude) > eps) Move(movement);
        //Move(movement);
        //Debug.Log("hi");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(dead);

        if (dead)
        {
            if (Time.timeScale == 0)
            {
                UnfreezeGame();
            }
            else return;
        }

        UnfreezeGame();

        CalculateDir();
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //rb.rotation = angle;
        ModifyEnemyFacing();

        if (timeToDamege <= 0 && Mathf.Abs(direction.magnitude) < 1.0f) HurtPlayer();
        if (timeToDamege > 0) timeToDamege -= Time.deltaTime;

        //Debug.Log(Mathf.Abs(direction.magnitude));
    }

    void ApplyDeath()
    {
        animator.SetTrigger("Dead");
        dead = true;
        rb.isKinematic = false;
        Collider.Destroy(rb);
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void TakeDamage(float damageAmount)
    {
        //Debug.Log("AHHHHHHH");
        if (health <= 0) return;

        health -= damageAmount;
        transform.position -= new Vector3(movement.x, movement.y);

        FreezeGame();

        if (health <= 0) ApplyDeath();
    }

    void FreezeGame()
    {
        Time.timeScale = 0;
        freezingTime = Time.unscaledTime;
    }

    void UnfreezeGame()
    {
        if (Time.timeScale == 0)
        {
            if (Time.unscaledTime - freezingTime >= 0.02)
            {
                Time.timeScale = 1;
            }
        }
    }

    void HurtPlayer()
    {
        var playerController = player.GetComponent<PlayerController>();
        if (playerController.health > 0)
        {
            playerController.TakeDamage(attack, movement);
            //Debug.Log("Damage!");
        }
        timeToDamege = damageFreq;
    }

    void CalculateDir()
    {
        direction = player.position - transform.position;
        movement = direction.normalized;
        //Debug.Log(direction);
        //Debug.Log(player.position);
    }

    void ModifyEnemyFacing()
    {
        if (Mathf.Abs(direction.x) > eps && (direction.x > 0 && !facingRight || direction.x < 0 && facingRight))
        {
            Flip();
        }
    }

    void Move(Vector2 dir)
    {
        //Debug.Log("moving");
        //rb.isKinematic = true;
        animator.SetBool("Move", true);
        rb.MovePosition((Vector2)transform.position + (dir * speed * Time.deltaTime));
        //rb.MovePosition((Vector2)transform.position + new Vector2(10, 0));
        //rb.isKinematic = false;
        //Debug.Log((dir * speed * Time.deltaTime));
        //Debug.Log((Vector2)transform.position + (dir * speed * Time.deltaTime));
        //Debug.Log(transform.position);
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        facingRight = !facingRight;
    }
}
