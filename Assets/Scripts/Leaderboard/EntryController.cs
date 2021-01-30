using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntryController : MonoBehaviour
{
    Player player;
    public Text[] initials = new Text[3];
    LeaderboardController lbc;
    // Start is called before the first frame update
    void Start()
    {
        lbc = new LeaderboardController();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void upButtonPressed(int i) {
        int currentLetter = (int)initials[i].text.ToCharArray()[0];
        if (currentLetter == 65) {
            currentLetter = 90;
        } else {
            currentLetter--;
        }
        initials[i].text = (char)currentLetter + "";
    }

    public void downButtonPressed(int i) {
        int currentLetter = (int)initials[i].text.ToCharArray()[0];
        if (currentLetter == 90) {
            currentLetter = 65;
        } else {
            currentLetter++;
        }
        initials[i].text = (char)currentLetter + "";
    }

    public void submitButtonPressed() {
        string ins = "";
        foreach (Text t in initials)
            ins += t.text;
        Debug.Log(ins);
        // player.playerInitials = ins; //add player hook up here when J is done with game manager
        // lbc.addEntry(player);
        // lbc.saveEntries();
    }

}
