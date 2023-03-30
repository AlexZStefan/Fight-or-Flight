using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : ScriptableObject
{
    // right hand colider = "HandColR" 
    public GameObject character;
    public string characterSelected = "";
    public int lives = 3;
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

    public void KillPlayer()
    {
        lives--;
        var spawnZone = GameObject.Find("SpawnZone");
        Vector3 respawn = GameObject.Find("SpawnZone").transform.position + spawnZone.transform.localScale;
        character.transform.position = new Vector3(Random.Range(-respawn.x, respawn.x), spawnZone.transform.position.y, 0);
        stamina = 100;
    }
}
