using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
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
    public TextMeshProUGUI countdown;

    private void Awake()
    {
        QualitySettings.vSyncCount = 1;
        playerOne = (Player)ScriptableObject.CreateInstance("Player");
        playerTwo = (Player)ScriptableObject.CreateInstance("Player");

        playerInputManager = GetComponent<PlayerInputManager>();
        playerInputManager.playerPrefab = playerInputPrefab;

        if (!instance) instance = this;
    }

    public void StartGame()
    {
        // Instantiate map
        if (MapSelector.instance.map.TryGetValue(mapSelected, out GameObject selectedMap))
        {
            currentMap = Instantiate(selectedMap);
        }

        // Instantiate characters Player 1 & 2
        foreach (var character in InGameCharacters.instance.characters)
        {
            InstantiateCharacter(playerOne, character, playerOne.characterSelected, 1);
            InstantiateCharacter(playerTwo, character, playerTwo.characterSelected, 2);
        }

        foreach (var player in Transform.FindObjectsOfType<ThirdPersonCharacter>())
        {
            InitialiseCharacter(player);
        }

        // Add camera target to player 1 & 2
        GameObject.Find("TargetGroup1").GetComponent<Cinemachine.CinemachineTargetGroup>().AddMember(playerOne.character.transform, 2, 0);
        GameObject.Find("TargetGroup1").GetComponent<Cinemachine.CinemachineTargetGroup>().AddMember(playerTwo.character.transform, 2, 0);

        // UI and Music
        CharSelection.instance.ChangeAvatarImage(1);
        CharSelection.instance.ChangeAvatarImage(2);
        Menu.instance.ActivateInGameUI();
        AudioManager.instance.inGameMusic.start();
        gameStarted = true;
        isPaused = true;

        StartCoroutine(starGameCountdown());
    }

    void InstantiateCharacter(Player player, GameObject character, string characterSelected, int playerIndex)
    {
        if (character.name == characterSelected)
        {
            player.character = Instantiate(character);
            player.character.GetComponent<ThirdPersonUserControl>().playerIndex = playerIndex;

            playerInputManager.JoinPlayer(playerIndex);
            player.startingPosition = GameObject.Find("PlayerSpawn" + playerIndex).transform;
            player.character.transform.position = player.startingPosition.position;
            player.character.transform.rotation = player.startingPosition.rotation;
        }
    }
    void InitialiseCharacter(ThirdPersonCharacter player)
    {
        Vector3 centre = player.transform.position;
        centre.x = 0;
        player.transform.LookAt(centre);
        player.InitCharacter();
    }

    IEnumerator starGameCountdown()
    {
        countdown.gameObject.SetActive(true);
        AudioManager.instance.PlayOneShot(FModEvents.instance.readyFight, Vector3.zero);

        for (int i = 3; i > 0; i--)
        {
            countdown.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        countdown.text = "Fight!";
        yield return new WaitForSeconds(0.25f);

        isPaused = false;
        countdown.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (gameStarted && (playerOne.lives < 0 || playerTwo.lives < 0))
        {
            string winnerGamertag = playerOne.lives < 0 ? "Player 2" : "Player 1";
            Menu.instance.winMenu.transform.Find("Win").GetComponent<TextMeshProUGUI>().text = winnerGamertag + " Wins!";
            Menu.instance.winMenu.SetActive(true);
            Menu.instance.winMenu.GetComponentInChildren<Button>().Select();
            gameStarted = false;
            isPaused = true;
        }
    }

    public void ResetGame()
    {
        ResetCharacter(playerOne, 1);
        ResetCharacter(playerTwo, 2);

        Menu.instance.winMenu.SetActive(false);
        gameStarted = true;
        StartCoroutine(starGameCountdown());
    }

    private void ResetCharacter(Player player, int playerId)
    {
        player.startingPosition = GameObject.Find($"PlayerSpawn{playerId}").transform;
        player.character.transform.position = playerTwo.startingPosition.position;
        player.character.transform.rotation = playerTwo.startingPosition.rotation;

        GameObject.Find($"HealthBarP{playerId}").GetComponent<Slider>().value = 100;
        player.stamina = 100;
        player.lives = 4;
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

        PlayerInput[] inputs = GameObject.FindObjectsOfType<PlayerInput>();

        foreach (var i in inputs)
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
