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
            AkSoundEngine.PostEvent("music_arena_stop_event", GameObject.Find("WwiseGlobal"));
            SceneManager.LoadScene("Scenes/Death Screen");
            AkSoundEngine.PostEvent("music_death_play_event", GameObject.Find("WwiseGlobal"));
        };
    }
}
