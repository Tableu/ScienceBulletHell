using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    public Health health;
    void Start()
    {
        health.OnDeathDelayed += delegate
        {
            SceneManager.LoadScene("Scenes/Death Screen");
        };
    }
}
