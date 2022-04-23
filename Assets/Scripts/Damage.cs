using UnityEngine;

public class Damage : MonoBehaviour
{
    public int damage;
    public float knockbackStrength;
    public float knockbackDuration;

    private void OnCollisionEnter2D(Collision2D other)
    {
        var health = other.collider.GetComponent<Health>();
        if (health != null)
        {
            health.ApplyDamage(this);
        }
    }
}
