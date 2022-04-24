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
    public GameObject bullet;
    public GameObject bulletPattern;
    public float fireRate;
    public string id;
}