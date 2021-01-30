using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class SplashScreen : MonoBehaviour
{
    public Image img;
    private AudioSource src;
    public AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        src = GetComponent<AudioSource>();
        src.clip = clip;
        src.volume = 0.65f;

        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(3.0f);

        // Fade 5 seconds
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        src.Play();
        for (float i = 0; i <= 5; i += Time.deltaTime) {
            // set color with i as alpha
            img.color = new Color(i, i, i, i);
            yield return null;
        }
        
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut() {
        for (float i = 2; i >= 0; i -= Time.deltaTime) {
            // set color with i as alpha
            img.color = new Color(i, i, i, i);
            yield return null;
        }
        SceneManager.LoadScene(1);
    }
}
