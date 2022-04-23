using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    public Rigidbody2D Rigidbody2D;
    public float Speed;
    public float MaxSpeed;
    protected float _maxSpeed;
}
