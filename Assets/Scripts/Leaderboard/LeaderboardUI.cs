using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class LeaderboardUI : MonoBehaviour
{
    public GameObject entryPrefab;
    LeaderboardController lbc;
    Entry[] entries;

    public AudioClip clip;
    private AudioSource src;
    // Start is called before the first frame update
    void Start()
    {
        src = GetComponent<AudioSource>();
        src.clip = clip;
        src.volume = 0.7f;


        lbc = new LeaderboardController();
        entries = lbc.GetEntries();
        initEntries();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayClickSound() {
        src.PlayOneShot(src.clip);
    }

    private void initEntries() {
        for (int i = 0; i < entries.Length; i++) {
            GameObject entryObj = Instantiate(entryPrefab, this.gameObject.transform);
            Text[] entryLabels = entryObj.GetComponentsInChildren<Text>();
            entryLabels[0].text = entries[i].initials;
            entryLabels[1].text = formatTime((float)entries[i].time);
            entryObj.transform.localPosition = new Vector3(0, 72 - (i*72), 0);
        }
    }

    private string formatTime(float seconds) {
        string time = "";

        float hours = Mathf.Floor(seconds / 3600);
        if (hours < 10)
            time += "0";
        time += hours + ":";

        float minutes = Mathf.Floor((seconds - (hours * 3600)) / 60);
        if (minutes < 10)
            time += "0";
        time += minutes + ":";

        float secs = seconds - (hours * 3600) - (minutes * 60);
        if (secs < 10)
            time += "0";
        time += secs;

        return time;
    }

    public void exitToMenu() {
        SceneManager.LoadScene(1);
    }

}
