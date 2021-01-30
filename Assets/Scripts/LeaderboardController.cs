using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LeaderboardController
{
    private string filePath = Application.persistentDataPath + "/leaderboard.json";
    private List<Entry> entries = new List<Entry>();

    public LeaderboardController() {
        if (!File.Exists(filePath))
            File.Create(filePath);
        loadEntries();
    }

    ///<summary>Loads entries from the preset file path</summary>
    private void loadEntries() {
        string json = System.IO.File.ReadAllText(filePath);
        EntryList el = JsonUtility.FromJson<EntryList>(json);
        if (el != null) //if the file wasn't empty
            entries = el.list;
        sortEntries();
    }

    ///<summary>Saves entries to the preset file path. Call this before the game ends</summary> 
    public void saveEntries() {
        sortEntries();
        EntryList el = new EntryList(entries);
        string listJson = JsonUtility.ToJson(el);
        System.IO.File.WriteAllText(filePath, listJson);
    }

    ///<summary>Checks if the given player has beaten the slowest time in the leaderboard and adds them to it if they have</summary>
    public void addEntry(Player p) {
        if (entries.Count == 10) { //if the list is full then remove the last entry
            if (hasBeatenTime(p.time)) {//if the user has been quick enough to get on the leaderboard, remove the last entry and add them to it
                entries.RemoveAt(9);
                entries.Add(new Entry(p.playerInitials, p.time));
            }
        } else {
            entries.Add(new Entry(p.playerInitials, p.time));
        }
        sortEntries();
    }

    ///<summary>Returns true if the given time is quicker than the slowest in the leaderboard</summary>
    private bool hasBeatenTime(double time) {
        return (time < entries[9].time);
    }

    ///<summary>Sorts the entries in the list from quickest to slowest</summary>
    private void sortEntries() {
        entries.Sort(Comparer<Entry>.Create((e1, e2) => 
            e1.time > e2.time ? 1 : e1.time < e2.time ? -1 : 0) //if e1 is higher return 1, is e2 is higher return -1, if equal return 0
        );
    }

}

///<summary>Unity apparently can't serialise a list, but can serialise a class that contains a list, so that's what this is for</summary>
[System.Serializable]
class EntryList {
    public List<Entry> list;

    public EntryList(List<Entry> l) {
        list = l;
    }

}

///<summary>Holds the relevant data that is stored in the leaderboard</summary>
[System.Serializable]
class Entry
{
    public string initials;
    public double time;
    
    public Entry(string i, double d) {
        initials = i;
        time = d;
    }

    public string toString() {
        return "Initials: " + initials + "\nTime: " + time;
    }

}
