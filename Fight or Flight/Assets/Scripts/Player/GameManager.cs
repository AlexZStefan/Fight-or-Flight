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
       // GameManager.instance.playerOne.characterSelected = InGameCharacters.instance.characters[0].name;
       // GameManager.instance.mapSelected = MapSelector.instance.maps[0].name;

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

        // Instantiate characters Player 1 & 2
        foreach (var v in InGameCharacters.instance.characters)
        {
            if (v.name == playerOne.characterSelected)
            {
                playerOne.character = Instantiate(v);
                playerOne.startingPosition = GameObject.Find("PlayerSpawn1").transform;
                playerOne.character.transform.position = playerOne.startingPosition.position;
                playerOne.character.transform.rotation = playerOne.startingPosition.rotation;
                Debug.Log("Spawned P1: " +  v.name);
            }            

            if (v.name == playerTwo.characterSelected)
            {
                playerTwo.character = Instantiate(v);
                playerTwo.startingPosition = GameObject.Find("PlayerSpawn2").transform;
                playerTwo.character.transform.position = playerTwo.startingPosition.position;
                playerTwo.character.transform.rotation = playerTwo.startingPosition.rotation;
                Debug.Log("Spawned P2: " + v.name);
            }        
        }

        GameObject.Find("TargetGroup1").GetComponent<Cinemachine.CinemachineTargetGroup>().AddMember(playerOne.character.transform,2,0);
        GameObject.Find("TargetGroup1").GetComponent<Cinemachine.CinemachineTargetGroup>().AddMember(playerTwo.character.transform,2,0);

        //ui and music
        CharSelection.instance.ChangeAvatarImage(1);
        CharSelection.instance.ChangeAvatarImage(2);
        Menu.instance.ActivateInGameUI();
        AudioManager.instance.inGameMusic.start();
    }

    // cleans player selections
    public void EndGame()
    {
        Debug.Log("Game Ended");        
        // music
        AudioManager.instance.inGameMusic.setParameterByName("Loop", 0);

        // game 
        Menu.instance.DisableInGameUI();
        mapSelected = "";
        GameObject.Find("TargetGroup1").GetComponent<Cinemachine.CinemachineTargetGroup>().RemoveMember(playerOne.character.transform);
        GameObject.Find("TargetGroup1").GetComponent<Cinemachine.CinemachineTargetGroup>().RemoveMember(playerTwo.character.transform);

        // data
        playerOne.CleanPlayer();
        playerTwo.CleanPlayer();
        Destroy(currentMap);
        currentMap = null;
    }    

    void CleanUpRound()
    {
        AudioManager.instance.inGameMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }




}
