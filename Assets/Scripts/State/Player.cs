using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    private int Health{get; set;}
    public double time = 0.0;
    public string playerInitials = "AAA";

    public enum player_states {
        idle = 0,
        walking,
        running,
        hasKey,
        noKey,
        bloodied,
        survived,
        died,
    }

    public player_states CurrentState = player_states.idle;
    public player_states KeyState = player_states.noKey;
    

    public Player() 
    {
        Health = 100;
        time = 0.0;
    }
}
