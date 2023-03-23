using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    float shakeMag;
    float shakeLen;

    public void Shake(float duration, float magnitude)
    {
        shakeLen = duration;
        shakeMag = magnitude;
        InvokeRepeating("BeginShake", 0, 0.01f);
        Invoke("StopShake", duration);
    }

    void BeginShake()
    {
        Vector3 orignalPosition = transform.position;

        float x = Random.Range(-1f, 1f) * shakeMag;
        float y = Random.Range(-1f, 1f) * shakeMag;
        transform.position += new Vector3(x, y, 0);
    }

    void StopShake()
    {
        CancelInvoke("BeginShake");
        transform.localPosition = Vector3.zero;
    }
}
