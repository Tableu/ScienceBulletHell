using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivate : MonoBehaviour
{
    public BoxCollider2D Collider2D;
    public GameObject Enemy;
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy.GetComponent<Health>().enabled = true;
        Enemy.GetComponent<EnemyAttack>().enabled = true;
        Destroy(gameObject);
    }
}
