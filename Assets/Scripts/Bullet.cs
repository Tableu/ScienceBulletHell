using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 Direction;
    public float Speed;

    void FixedUpdate()
    {
        transform.position += Direction * Speed * Time.deltaTime;
    }
}
