using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMusic : MonoBehaviour
{
    public AudioSource source;
    public AudioClip audioClip;
    // Start is called before the first frame update
    void Start()
    {
        AkSoundEngine.PostEvent("music_death_stop_event", GameObject.Find("WwiseGlobal"));
        AkSoundEngine.PostEvent("music_arena_stop_event", GameObject.Find("WwiseGlobal"));
        source = GetComponent<AudioSource>();
        source.loop = true;
        source.PlayOneShot(audioClip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
