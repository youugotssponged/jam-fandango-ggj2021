using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class InstructionsController : MonoBehaviour
{
    private AudioSource src;
    public AudioClip clip;

    private void Start() {
        src = GetComponent<AudioSource>();
        src.clip = clip;
        src.volume = 0.7f;
    }
    public void ReturnToMainMenu(){
        SceneManager.LoadScene(1);
    }

    public void PlayClickSound(){
        src.PlayOneShot(src.clip);
    }
}
