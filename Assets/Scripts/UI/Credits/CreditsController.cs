using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class CreditsController : MonoBehaviour
{
    private AudioSource src;
    public AudioClip click;

    public GameObject textArea;
    
    // Start is called before the first frame update
    void Start()
    {
        src = GetComponent<AudioSource>();
        src.clip = click;
        src.volume = 0.7f;
    }

    // Update is called once per frame
    void Update()
    {
        textArea.transform.Translate(0, 50.0f * Time.deltaTime, 0);
    }

    public void PlayClickOnHover() 
    {
        src.PlayOneShot(src.clip);
    }

    public void ReturnToMainMenu(){
        SceneManager.LoadScene(1);
    }
}
