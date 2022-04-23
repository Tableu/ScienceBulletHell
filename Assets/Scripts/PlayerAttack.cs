using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform parent;
    public float Cooldown;
    private float fireTime;
    
    private void FixedUpdate()
    {
        if (Time.time-fireTime >= Cooldown && Mouse.current.leftButton.isPressed)
        {
            GameObject b = Instantiate(bullet, transform.position, Quaternion.identity, parent);
            b.layer = 9;
            Bullet script = b.GetComponent<Bullet>();
            if (script != null)
            {
                script.Direction = (Vector2)(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) -
                                   transform.position).normalized;
            }
            fireTime = Time.time;
        }
    }
}
