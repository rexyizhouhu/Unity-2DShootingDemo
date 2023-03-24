using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    public float health = 100;
    public bool facingRight = true;
    public Transform gun;
    public Transform shotPos;
    public float attack = 20;
    public bool burstMode = false;
    public float speedAfterHurt = 7.5f;
    public float attackAfterHurt = 30.0f;
    public HealthBar healthBar;
    public int kills;
    public readonly float invincibleTime = 1f;
    public float curSpeed;

    //Animator animator;
    float h;
    float v;
    SpriteRenderer m_SpriteRenderer;
    Color originalColor;
    float curInvincibleTime;

    // Start is called before the first frame update
    void Start()
    {
        //animator = GetComponent<Animator>();
        //animator.SetInteger("Health", 100);
        curSpeed = speed;
        healthBar.SetMaxHealth(health);
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = m_SpriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Time.time);
        

        if (curInvincibleTime > 0) curInvincibleTime -= Time.deltaTime;

        GetInputs();

        MoveCharacter();

        CheckHealth();
        //SetAnimatorParams();
    }

    void GetInputs()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
    }

    void MoveCharacter()
    {
        transform.position += curSpeed * Time.deltaTime * new Vector3(h, v, 0);
        //Debug.Log(curSpeed);
        //if (h > 0) transform.localScale = new Vector3(1, 1, 0);
        //else if (h < 0) transform.localScale = new Vector3(-1, 1, 0);

        ModifyFacingDirection();
    }

    void ModifyFacingDirection()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var direction = mousePos - transform.position;

        if (!facingRight && direction.x > 0 || facingRight && direction.x < 0) Flip();
    }

    void Flip()
    {
        //Debug.Log(facingRight);
        //Debug.Log(h);
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        scale = gun.transform.localScale;
        scale *= -1;
        gun.transform.localScale = scale;

        var rotation = shotPos.transform.localRotation;
        if (rotation.x > 0) rotation.x = 0;
        else rotation.x += 180;
        shotPos.transform.localRotation = rotation;
    }

    void SetAnimatorParams()
    {
        bool isMoving = h != 0 || v != 0;

        //animator.SetBool("Move", isMoving);
        //animator.SetFloat("Strafe", h);
        //animator.SetBool("Fire", fire);
    }

    void CheckHealth()
    {
        if (health <= 0)
        {
            PlayerDied();
        }
        if (health <= 50)
        {
            if (m_SpriteRenderer.color != Color.yellow && m_SpriteRenderer.color != Color.red)
            {
                m_SpriteRenderer.color = Color.red;
            }

            if (burstMode) return;

            speed = speedAfterHurt;
            attack = attackAfterHurt;
            burstMode = true;
        }
    }

    void PlayerDied()
    {
        LevelManager.instance.GameOver();
        gameObject.SetActive(false);
    }

    public void TakeDamage(float damage, Vector3 direction)
    {
        if (curInvincibleTime > 0) return;

        health -= damage;
        healthBar.SetHealth(health);

        if (direction.x != 0 || direction.y != 0)
        {
            transform.position += direction * 2;
        }

        m_SpriteRenderer.color = Color.yellow;
        Invoke("ChangeColorBack", 0.5f);
    }

    void ChangeColorBack()
    {
        m_SpriteRenderer.color = originalColor;
    }
}
