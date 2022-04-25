using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int CurrentHealth;
    public int MaxHealth;
    public Rigidbody2D Rigidbody2D;
    public Movement Movement;
    public Slider HealthBar;
    private bool _inKnockback;
    private Vector2 _knockbackDirection;
    private float _knockbackStart;
    private Damage _dmg;
    public Animator Animator;
    public Collider2D Collider2D;
    public float radius;
    public string EnemyLayer;
    public float DeathTime;

    private void Start()
    {
        if (HealthBar != null)
        {
            HealthBar.maxValue = MaxHealth;
            HealthBar.value = CurrentHealth;
        }
    }

    void FixedUpdate()
    {
        if (_inKnockback)
        {
            if (Time.time - _knockbackStart < _dmg.knockbackDuration)
            {
                Rigidbody2D.AddForce(_dmg.knockbackStrength*_knockbackDirection, ForceMode2D.Impulse);
            }
            else
            {
                _inKnockback = false;
                if (Movement)
                {
                    Movement.enabled = true;
                }
            }
        }
        if (HealthBar != null)
        {
            HealthBar.value = CurrentHealth;
        }

        List<Collider2D> results = new List<Collider2D>();
        Physics2D.OverlapCircle(transform.position, radius, new ContactFilter2D()
        {
            layerMask = LayerMask.GetMask(EnemyLayer),
            useLayerMask = true
        }, results);
        foreach (Collider2D col in results)
        {
            Damage dmg = col.gameObject.GetComponent<Damage>();
            if (dmg != null)
            {
                ApplyDamage(dmg);
            }
            Destroy(col.gameObject);
        }
    }

    public void ApplyDamage(Damage dmg)
    {
        if (_inKnockback)
        {
            return;
        }
        _dmg = dmg;
        CurrentHealth -= _dmg.damage;
        _knockbackDirection = (transform.position - dmg.transform.position).normalized;
        _inKnockback = true;
        Rigidbody2D.AddForce(_dmg.knockbackStrength*_knockbackDirection, ForceMode2D.Impulse);
        AkSoundEngine.PostEvent("player_hurt_event", GameObject.Find("WwiseGlobal"));
        if (Movement != null)
        {
            Movement.enabled = false;
        }
        _knockbackStart = Time.time;
        if (HealthBar != null)
        {
            HealthBar.value = CurrentHealth;
        }
        if (CurrentHealth <= 0)
        {
            if (Animator != null)
            {
                Animator.SetBool("Death", true);
                Collider2D.enabled = false;
                OnDeath?.Invoke();
                StartCoroutine(DeathCoroutine());
            }
        }
    }

    IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(DeathTime);
        OnDeathDelayed?.Invoke();
        Destroy(gameObject);
        HealthBar.gameObject.SetActive(false);
        AkSoundEngine.PostEvent("music_arena_stop_event", GameObject.Find("WwiseGlobal"));
        AkSoundEngine.PostEvent("music_death_play_event", GameObject.Find("WwiseGlobal"));
    }

    public Action OnDeath;
    public Action OnDeathDelayed;

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
#endif
}