using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MainMenuController : MonoBehaviour
{
    public AudioClip clip;
    public AudioSource src;

    // Needs to play theme, and handle button sounds?

    private void Start(){

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

    public void QuitToDesktop(){
        Application.Quit();
    }
}
