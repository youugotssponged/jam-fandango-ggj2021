using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuController : MonoBehaviour
{
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
