using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardUIController : MonoBehaviour
{
    public List<Text> Rows;

    public Text yourRecent;

    private string path;

    void Start()
    {
        path = Application.persistentDataPath + '/' + "leaderboard.json";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FetchLeaderboardData() {
        string things = File.ReadAllText(path);
        
    }
}
