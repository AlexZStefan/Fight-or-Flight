using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public string mapSelected = "";

    public Player playerOne;
    public Player playerTwo;

    public GameObject currentMap;

    private void Awake()
    {
        if (!instance)
            instance = this;
        playerOne = (Player)ScriptableObject.CreateInstance("Player");
        playerTwo = (Player)ScriptableObject.CreateInstance("Player");

    }


    public void StartGame()
    {
        GameManager.instance.playerOne.characterSelected = InGameCharacters.instance.characters[0].name;
        GameManager.instance.mapSelected = MapSelector.instance.maps[0].name;

        // called from menu script uppond sellecting the map
        Debug.Log("GameStarted");

        // Instantiate map 
        foreach (var v in MapSelector.instance.maps)
        {
            if (v.name == mapSelected)
            {
                Debug.Log("Instantiating map: " + v.name);
                currentMap = Instantiate(v);
                break;
            }
            else
            {
                Debug.Log("Map does not exist: " + mapSelected);
            }
        }

        // Instantiate characters Player 1
        foreach (var v in InGameCharacters.instance.characters)
        {
            if (v.name == playerOne.characterSelected)
            {
                playerOne.character = Instantiate(v);
                playerOne.startingPosition = GameObject.Find("PlayerSpawn1").transform;
                playerOne.character.transform.position = playerOne.startingPosition.position;
                playerOne.character.transform.rotation = playerOne.startingPosition.rotation;
                break;
            }
            else
            {
                Debug.Log("Character does not exist: " + playerOne.character);
            }
        }

        // Player 2 character Here

        Menu.instance.ActivateInGameUI();

        AudioManager.instance.inGameMusic.start();
    }

    public void EndGame()
    {
        // called from menu script uppond sellecting the map
        Debug.Log("Game Ended");

        AudioManager.instance.inGameMusic.setParameterByName("Loop", 0);

        Menu.instance.DisableInGameUI();
    }

    

    void CleanUpRound()
    {
        AudioManager.instance.inGameMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }




}
