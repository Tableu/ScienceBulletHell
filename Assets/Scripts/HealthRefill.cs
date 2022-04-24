using UnityEngine;

public class HealthRefill : MonoBehaviour
{
    public int Amount;

    private void OnCollisionEnter2D(Collision2D other)
    {
        Health health = other.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.CurrentHealth += Amount;
            if (health.CurrentHealth > health.MaxHealth)
            {
                health.CurrentHealth = health.MaxHealth;
            }
            Destroy(gameObject);
        }
    }
}
