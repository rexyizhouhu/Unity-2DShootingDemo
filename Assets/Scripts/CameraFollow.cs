using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public float followSpeed = 2;
    public Transform target;
    public float turnSpeed = 8;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(target.position.x, target.position.y, -10);
        transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);

        if (target.GetComponent<PlayerController>().facingRight)
        {
            transform.position = Vector3.Slerp(transform.position, newPos + new Vector3(10, 0), turnSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Slerp(transform.position, newPos + new Vector3(-10, 0), turnSpeed * Time.deltaTime);
        }
    }
}
