using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LiveScreenController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LoadMainMenu(){
        SceneManager.LoadScene(1);
    }
}
