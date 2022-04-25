using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private BulletPatternData Data;
    [SerializeField] private string startingWeapon;
    private float fireTime;
    private BulletPattern currentPattern;
    private int patternIndex = 0;

    private void Start()
    {
        currentPattern = Data.BulletPatterns.Find(pattern => pattern.id == startingWeapon);
    }

    private void FixedUpdate()
    {
        if (Time.time-fireTime >= currentPattern.fireRate && Mouse.current.leftButton.isPressed)
        {
            Vector2 dir = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - transform.position;
            BulletAttacks.SpawnBullets(gameObject, currentPattern.bullet[patternIndex], currentPattern.bulletPattern[patternIndex], parent, 9, dir);
            patternIndex++;
            if (patternIndex >= currentPattern.bulletPattern.Count)
            {
                patternIndex = 0;
            }
            fireTime = Time.time;
            AkSoundEngine.PostEvent("player_magic_shoot_event", GameObject.Find("WwiseGlobal"));
        }
    }
}