using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public bool isPaused = true;
    public Text countdown;

    private void Awake()
    {
        QualitySettings.vSyncCount = 1;
       // Application.targetFrameRate = 60;

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

        var players = Transform.FindObjectsOfType<ThirdPersonCharacter>();
        foreach (var p in players)
        {
            Vector3 centre = p.transform.position;
            centre.x = 0;
            p.transform.LookAt (centre);
            p.InitCharacter();
        }

        GameObject.Find("TargetGroup1").GetComponent<Cinemachine.CinemachineTargetGroup>().AddMember(playerOne.character.transform,2,0);
        GameObject.Find("TargetGroup1").GetComponent<Cinemachine.CinemachineTargetGroup>().AddMember(playerTwo.character.transform,2,0);

        //ui and music
        CharSelection.instance.ChangeAvatarImage(1);
        CharSelection.instance.ChangeAvatarImage(2);
        Menu.instance.ActivateInGameUI();
        AudioManager.instance.inGameMusic.start();

        gameStarted = true;
        isPaused = true;

        StartCoroutine(starGameCountdown());
    }

    IEnumerator starGameCountdown()
    {
        countdown.gameObject.SetActive(true);
        AudioManager.instance.PlayOneShot(FModEvents.instance.readyFight, Vector3.zero);
        for (int i = 3; i >= 0; i--)
        {
            countdown.text = i.ToString();
            
            if(i == 0)
            {
                countdown.text = "Fight!";
            }
            yield return new WaitForSeconds(1);

        }
        isPaused = false;
        countdown.gameObject.SetActive(false);
    }

    private void Update()
    {
        

        if (gameStarted && (playerOne.lives < 0 || playerTwo.lives < 0))
        {
            if(playerOne.lives < 0)
            {
                Menu.instance.winMenu.transform.Find("Win").GetComponent<Text>().text = "Player 2 wins!";
            }
            else
            {
                Menu.instance.winMenu.transform.Find("Win").GetComponent<Text>().text = "Player 1 wins!";
            }
            gameStarted = false;
            Menu.instance.winMenu.SetActive(true);
            isPaused = true;
            Menu.instance.winMenu.GetComponentInChildren<Button>().Select();
        }
    }

    public void ResetGame()
    {
        
        Menu.instance.winMenu.SetActive(false);
        gameStarted = true;
        
        playerTwo.startingPosition = GameObject.Find("PlayerSpawn2").transform;
        playerTwo.character.transform.position = playerTwo.startingPosition.position;
        playerTwo.character.transform.rotation = playerTwo.startingPosition.rotation;

        playerOne.startingPosition = GameObject.Find("PlayerSpawn1").transform;
        playerOne.character.transform.position = playerOne.startingPosition.position;
        playerOne.character.transform.rotation = playerOne.startingPosition.rotation;

        GameObject.Find("HealthBarP1").GetComponent<Slider>().value = 100;
  
                playerOne.stamina = 100;

        GameObject.Find("HealthBarP2").GetComponent<Slider>().value = 100;

        playerTwo.stamina = 100;

        playerOne.lives = 4;
        playerTwo.lives = 4;

        StartCoroutine(starGameCountdown());
    }

    // cleans player selections
    public void EndGame()
    {
        CleanUpRound();
        Menu.instance.PlayMenuMusic();
        gameStarted = false;
        // music
        AudioManager.instance.inGameMusic.setParameterByName("Loop", 0);

        // game 
        Menu.instance.DisableInGameUI();
        mapSelected = "";
        GameObject.Find("TargetGroup1").GetComponent<Cinemachine.CinemachineTargetGroup>().RemoveMember(playerOne.character.transform);
        GameObject.Find("TargetGroup1").GetComponent<Cinemachine.CinemachineTargetGroup>().RemoveMember(playerTwo.character.transform);

        PlayerInput []inputs = GameObject.FindObjectsOfType<PlayerInput>();

        foreach(var i in inputs)
        {
            Destroy(i.gameObject);
        }
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
