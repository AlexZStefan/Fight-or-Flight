using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : ScriptableObject
{
    public GameObject character;
    public string characterSelected = "";

    public Transform startingPosition;

    public float stamina = 100;

    int lowPunch  = 5;
    int highPunch = 10;

    public void CleanPlayer()
    {
        Destroy(character);
        character = null;

        characterSelected = "";
    }

    public void LowPunch(Player other)
    {
        other.stamina -= lowPunch;
    }

    public void HighPunch(Player other)
    {
        other.stamina -= highPunch;
    }
}
