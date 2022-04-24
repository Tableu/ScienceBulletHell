using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable][CreateAssetMenu(menuName = "Bullet Patterns/Pattern Data")]
public class BulletPatternData : ScriptableObject
{
    [SerializeField] public List<BulletPattern> BulletPatterns;
}

[Serializable]
public struct BulletPattern
{
    public List<GameObject> bullet;
    public List<GameObject> bulletPattern;
    public float fireRate;
    public float duration;
    public int burstCount;
    public bool targeted;
    public string id;
}