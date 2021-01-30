using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PausePanel;
    public bool isPaused;

    void Start()
    {
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            Debug.Log("SPACE PRESSED");
            if(isPaused){
                Resume();
            } else {
                Pause();
            }
        }
    }

    public void Pause(){
        isPaused = true;
        Time.timeScale = 0f;
        PausePanel.SetActive(true);
    }

    public void Resume(){
        isPaused = false;
        Time.timeScale = 1f;
        PausePanel.SetActive(false);
    }

    public void ReturnToMainMenu(){
        SceneManager.LoadScene(1);
    }
    public void QuitToDesktop(){
        Application.Quit();
    }
}
