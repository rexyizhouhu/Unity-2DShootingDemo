using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, mousePos.y, Camera.main.transform.position.z + 1);
    }
}
