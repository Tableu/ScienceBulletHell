using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private BulletPatternData Data;
    [SerializeField] private Transform parent;
    [SerializeField] private GameObject player;
    [SerializeField] private Health Health;
    private int burstCount;
    private List<BulletPattern>.Enumerator enumerator;
    private float patternStartTime;
    private float fireStartTime;
    private int patternIndex;
    private void Start()
    {
        enumerator = Data.BulletPatterns.GetEnumerator();
        enumerator.MoveNext();
        patternStartTime = Time.time;
        fireStartTime = 0;
        burstCount = 0;
        patternIndex = 0;
        Health.OnDeath += delegate
        {
            enabled = false;
        };
    }
    
    private void FixedUpdate()
    {
        if (Time.time - patternStartTime > enumerator.Current.duration)
        {
            if (!enumerator.MoveNext())
            {
                enumerator = Data.BulletPatterns.GetEnumerator();
                enumerator.MoveNext();
            }
            patternStartTime = Time.time;
            burstCount = 0;
            fireStartTime = 0;
            patternIndex = 0;
        }

        if (burstCount < enumerator.Current.burstCount && Time.time - fireStartTime > enumerator.Current.fireRate)
        {
            Vector2 dir = Vector2.down;
            if (enumerator.Current.targeted)
            {
                dir = player.transform.position - transform.position;
            }
            BulletAttacks.SpawnBullets(gameObject, enumerator.Current.bullet[patternIndex], enumerator.Current.bulletPattern[patternIndex], parent,
                8, dir);
            patternIndex++;
            fireStartTime = Time.time;
            if (patternIndex >= enumerator.Current.bulletPattern.Count)
            {
                patternIndex = 0;
                burstCount++;
            }
        }
    }

    public void ChangePhase(BulletPatternData data)
    {
        Data = data;
        enumerator = Data.BulletPatterns.GetEnumerator();
        enumerator.MoveNext();
        patternStartTime = Time.time;
        fireStartTime = 0;
        burstCount = 0;
        patternIndex = 0;
    }
}
