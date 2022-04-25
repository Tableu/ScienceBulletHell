using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPhases : MonoBehaviour
{
    public Animator Animator;
    public Health Health;
    public EnemyAttack Attack;
    public BulletPatternData Data;
    private bool secondPhase;
    
    void FixedUpdate()
    {
        if (!secondPhase && Health.CurrentHealth < Health.MaxHealth / 2)
        {
            Animator.SetTrigger("2ndPhase");
            secondPhase = true;
            Attack.ChangePhase(Data);
            AkSoundEngine.PostEvent("enemy_phase02_event", GameObject.Find("WwiseGlobal"));
        }
    }
}
