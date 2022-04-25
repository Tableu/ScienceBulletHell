using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public AudioSource source;
    public AudioClip audioClip;
    private void Start()
    {
        source = GetComponent<AudioSource>();
        source.loop = true;
        source.PlayOneShot(audioClip);
    }
    public void OnClick()
    {
        SceneManager.LoadScene("Scenes/GameScene");
    }
}