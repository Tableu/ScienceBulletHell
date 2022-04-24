using System;
using UnityEngine;

public class SineBullet : Bullet
{
    public float frequency;
    public float magnitude;
    private float startTime;

    private void Start()
    {
        startTime = Time.time;
    }

    private new void FixedUpdate()
    {
        Vector3 pos = transform.position + Direction * Speed * Time.deltaTime;
        transform.position = pos + Vector3.right * Mathf.Sin((Time.time-startTime) * frequency) * magnitude;
    }

}
