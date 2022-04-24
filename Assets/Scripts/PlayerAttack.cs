using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private BulletPatternData Data;
    private float fireTime;
    private BulletPattern currentPattern;

    private void Start()
    {
        currentPattern = Data.BulletPatterns[0];
    }

    private void FixedUpdate()
    {
        if (Time.time-fireTime >= currentPattern.fireRate && Mouse.current.leftButton.isPressed)
        {
            BulletAttacks.SpawnBullets(gameObject, currentPattern.bullet, currentPattern.bulletPattern, parent, 9);
            fireTime = Time.time;
        }
    }
}