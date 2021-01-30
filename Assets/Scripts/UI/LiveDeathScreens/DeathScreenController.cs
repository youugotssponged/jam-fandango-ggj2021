using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class DeathScreenController : MonoBehaviour
{
    void Start()
    {
        // music later?
    }

    public void LoadMainMenu(){
        SceneManager.LoadScene(1);
    }

}
