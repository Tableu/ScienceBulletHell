using System;
using UnityEngine;

[CreateAssetMenu(menuName = "References/Player References")]
[Serializable]
public class PlayerReferences : ScriptableObject
{
    public PlayerInputActions InputActions;
    public int DamageMultiplier;
    public void OnEnable()
    {
        DamageMultiplier = 1;
    }

    public void OnDisable()
    {
        
        DamageMultiplier = 1;
    }
}
