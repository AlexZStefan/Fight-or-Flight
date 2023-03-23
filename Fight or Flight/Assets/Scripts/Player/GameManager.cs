using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public string mapSelected = "";
    public bool gameStarted = false;
    public Player playerOne;
    public Player playerTwo;
    public PlayerInputManager playerInputManager;
    public GameObject currentMap;
    public GameObject playerInputPrefab;
    public GameObject playerInputPrefab2;

    private void Awake()
    {
   

        playerOne = (Player)ScriptableObject.CreateInstance("Player");
        playerTwo = (Player)ScriptableObject.CreateInstance("Player");

        playerInputManager = GetComponent<PlayerInputManager>();
        playerInputManager.playerPrefab = playerInputPrefab;

        if (!instance)
            instance = this;

   
    }

    public void StartGame()
    {
       // GameManager.instance.playerOne.characterSelected = InGameCharacters.instance.characters[0].name;
       // GameManager.instance.mapSelected = MapSelector.instance.maps[0].name;

        // called from menu script uppond sellecting the map
        
        // Instantiate map 
        foreach (var v in MapSelector.instance.maps)
        {
            if (v.name == mapSelected)
            {
                currentMap = Instantiate(v);
                break;
            }
            else
            {
            }
        }

        // Instantiate characters Player 1 & 2
        foreach (var v in InGameCharacters.instance.characters)
        {
            if (v.name == playerOne.characterSelected)
            {
                playerOne.character = Instantiate(v);
                playerOne.character.GetComponent<ThirdPersonUserControl>().playerIndex = 1;
                
                playerInputManager.JoinPlayer(1, 0, null, InputSystem.devices[0]);
                playerOne.startingPosition = GameObject.Find("PlayerSpawn1").transform;
                playerOne.character.transform.position = playerOne.startingPosition.position;
                playerOne.character.transform.rotation = playerOne.startingPosition.rotation;
            }            

            if (v.name == playerTwo.characterSelected)
            {
                playerTwo.character = Instantiate(v);
                // assign character to the player input 
                playerTwo.character.GetComponent<ThirdPersonUserControl>().playerIndex = 2;
               // playerInputManager.JoinPlayer(2, 0, null, InputSystem.devices[0]);
               playerInputManager.JoinPlayer(2);                 
                playerTwo.startingPosition = GameObject.Find("PlayerSpawn2").transform;
                playerTwo.character.transform.position = playerTwo.startingPosition.position;
                playerTwo.character.transform.rotation = playerTwo.startingPosition.rotation;
            }        
        }
                
        GameObject.Find("TargetGroup1").GetComponent<Cinemachine.CinemachineTargetGroup>().AddMember(playerOne.character.transform,2,0);
        GameObject.Find("TargetGroup1").GetComponent<Cinemachine.CinemachineTargetGroup>().AddMember(playerTwo.character.transform,2,0);

        //ui and music
        CharSelection.instance.ChangeAvatarImage(1);
        CharSelection.instance.ChangeAvatarImage(2);
        Menu.instance.ActivateInGameUI();
        AudioManager.instance.inGameMusic.start();


        gameStarted = true;
    }

    // cleans player selections
    public void EndGame()
    {
        gameStarted = false;
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
