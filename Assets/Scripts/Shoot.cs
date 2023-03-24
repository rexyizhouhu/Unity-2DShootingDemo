using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Shoot : MonoBehaviour
{

    public Bullet bullet;
    public Bullet KameHameHa;
    public Transform shotPoint;
    public Transform gun;
    public Camera cam;
    public Animator animator;
    public GameObject muzzleFlash;
    public int frameToFlash = 7;
    bool isFlashing = false;
    public int specialBulletRemain = 3;

    Vector2 direction;
    float timeShot = 0;
    readonly float shotFreq = 0.05f;
    PlayerController player;

    void Start()
    {
        player = GetComponent<PlayerController>();
        muzzleFlash.SetActive(false);
    }

    void Update()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePos - gun.transform.position;
        gun.transform.right = direction;

        //var scalar = gun.transform.localScale;
        //scalar.y *= -1;
        //if (mousePos.x - transform.position.x < 0)
        //{
        //    gun.transform.localScale = scalar;
        //}
        //else
        //{
        //    gun.transform.localScale = scalar;
        //}

        if (timeShot <= 0)
        {
            if (player.burstMode)
            {
                if (Input.GetMouseButton(1) && specialBulletRemain > 0)
                {
                    // slow enemies
                    ShotKameHameHa();
                }
            }
        }


        if (timeShot <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                Shot();
            }
            else
            {
                player.curSpeed = player.speed;
            }
        }
        else
        {
            timeShot -= Time.deltaTime;
        }
    }

    void ShotKameHameHa()
    {
        Instantiate(KameHameHa, shotPoint.position, shotPoint.rotation);

        animator.SetTrigger("Shoot");
        cam.GetComponent<CameraShake>().Shake(1.5f, .5f);
        player.curSpeed = 0;

        if (!isFlashing) StartCoroutine(FlashMuzzleFlash());

        timeShot = 1;
        specialBulletRemain--;
    }

    void Shot()
    {
        InitiateBullet(Quaternion.Euler(0, 0, 0), player);
        InitiateBullet(Quaternion.Euler(0, 0, 15), player);
        InitiateBullet(Quaternion.Euler(0, 0, -15), player);
        //var newBullet1 = Instantiate(bullet, shotPoint.position, shotPoint.rotation);
        //var newBullet2 = Instantiate(bullet, shotPoint.position, shotPoint.rotation * Quaternion.Euler(0, 0, 15));
        //var newBullet3 = Instantiate(bullet, shotPoint.position, shotPoint.rotation * Quaternion.Euler(0, 0, -15));
        animator.SetTrigger("Shoot");
        //Debug.Log(Quaternion.AngleAxis(-15, shotPoint.forward));
        //newBullet.isCharFacingRight = GetComponentInParent<PlayerController>().facingRight;
        timeShot = shotFreq;
        cam.GetComponent<CameraShake>().Shake(.1f, .15f);
        player.curSpeed = player.speed / 1.5f;

        if (!isFlashing) StartCoroutine(FlashMuzzleFlash());
    }

    void InitiateBullet(Quaternion changeInAngle, PlayerController shooter)
    {
        var newBullet = Instantiate(bullet, shotPoint.position, shotPoint.rotation * changeInAngle);
        newBullet.shooter = shooter;
    }

    IEnumerator FlashMuzzleFlash()
    {
        muzzleFlash.SetActive(true);
        isFlashing = true;

        for (int i = 0; i < frameToFlash; ++i)
        {
            yield return 0;
        }

        muzzleFlash.SetActive(false);
        isFlashing = false;
    }
}
