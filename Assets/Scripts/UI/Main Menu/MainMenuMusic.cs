using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MainMenuMusic : MonoBehaviour
{
    private AudioSource src;
    public AudioClip clip;

    private void Start(){
        src = GetComponent<AudioSource>();
        src.clip = clip;
        src.volume = 0.65f;
        src.loop = true;
        src.Play();
    }    
}
