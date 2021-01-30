using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class PlayerHUDController : MonoBehaviour
{
    private int health = 0;
    private float stamina = 0 ;

    public GameObject KeyEnabled;
    public GameObject KeyDisabled;

    public Image HPBarImage;
    public Image STBarImage;

    public bool keyFound = false;
    private bool isKeySoundActive = true;

    private AudioSource src;
    public AudioClip keyPickupSound;


    // Start is called before the first frame update
    void Start()
    {
        src = GetComponent<AudioSource>();
        src.clip = keyPickupSound;
        src.volume = 0.7f;
    }

    // Update is called once per frame
    void Update()
    {
        // Check vals - set bar widths


        // Check and update key image
        UpdateKeyImage();
    }

    public void UpdateKeyImage(){
        if(keyFound) {
            KeyDisabled.SetActive(false);
            KeyEnabled.SetActive(true);
            PlayKeyPickupOnce();
        } else {
            KeyDisabled.SetActive(true);
            KeyEnabled.SetActive(false);

            isKeySoundActive = true; // reset audio toggle
        }
    }

    public void PlayKeyPickupOnce(){
        if(keyFound && isKeySoundActive){
            isKeySoundActive = false;
            src.PlayOneShot(src.clip);
        }
    }


    public int GetHealthVal(){
        return health;
    }

    public void SetHealthVal(int val) {
        health = val;
        return;
    }

    public float GetStaminaVal(){
        return stamina;
    }

    public void SetStaminaVal(int val) {
        stamina = val;
        return;
    }


}
