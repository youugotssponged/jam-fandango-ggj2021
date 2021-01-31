using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MainMenuController : MonoBehaviour
{
    public AudioClip btnClick;
    public AudioSource src;

    // Needs to play theme, and handle button sounds?

    private void Start(){
        src = GetComponent<AudioSource>();
        src.clip = btnClick;
        src.volume = 0.7f;
    }

    public void PlayClickSoundOnHover(){
        src.PlayOneShot(src.clip);
    }

    public void LoadInstructions(){
        SceneManager.LoadScene(3);
    }
    
    public void LoadGame(){
        SceneManager.LoadScene(4);
    }

    public void LoadLeaderboard(){
        SceneManager.LoadScene(2);
    }

    public void LoadCreditsScene(){
        SceneManager.LoadScene(7);
    }

    public void QuitToDesktop(){   
        Application.Quit();
    }
}
