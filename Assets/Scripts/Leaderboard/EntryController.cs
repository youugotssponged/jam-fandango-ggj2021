using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntryController
{
    Player player;
    private Text[] initials = new Text[3];

    public EntryController(Text[] t) {
        initials = t;
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

    public string getInitials() {
        string ins = "";
        foreach (Text t in initials)
            ins += t.text;
        return ins;
    }

}
