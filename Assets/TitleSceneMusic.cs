using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneMusic : MonoBehaviour
{
    // Start is called before the first frame update
   private void Start()
    {
        AkSoundEngine.PostEvent("music_death_stop_event", GameObject.Find("WwiseGlobal"));
        AkSoundEngine.PostEvent("music_arena_stop_event", GameObject.Find("WwiseGlobal"));
        AkSoundEngine.PostEvent("music_title_play_event", GameObject.Find("WwiseGlobal"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
