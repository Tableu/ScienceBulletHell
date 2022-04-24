using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 Direction = Vector3.zero;
    public float Speed;
    public WallMaker RecentWall;

    void FixedUpdate()
    {
        transform.position += Direction * Speed * Time.deltaTime;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
