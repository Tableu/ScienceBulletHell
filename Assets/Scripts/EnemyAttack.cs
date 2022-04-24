using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private BulletPatternData Data;
    [SerializeField] private Transform parent;
    private int burstCount;
    private List<BulletPattern>.Enumerator enumerator;
    private float patternStartTime;
    private int patternIndex;
    private void Start()
    {
        enumerator = Data.BulletPatterns.GetEnumerator();
        enumerator.MoveNext();
        patternStartTime = Time.time;
        burstCount = 0;
        patternIndex = 0;
    }
    
    private void FixedUpdate()
    {
        if (Time.time - patternStartTime > enumerator.Current.duration)
        {
            enumerator.MoveNext();
            patternStartTime = Time.time;
            burstCount = 0;
            patternIndex = 0;
        }

        if (burstCount < enumerator.Current.burstCount)
        {
            BulletAttacks.SpawnBullets(gameObject, enumerator.Current.bullet, enumerator.Current.bulletPattern[patternIndex], parent,
                8);
            patternIndex++;
            if (patternIndex >= enumerator.Current.bulletPattern.Count)
            {
                patternIndex = 0;
                burstCount++;
            }
        }
    }
}
