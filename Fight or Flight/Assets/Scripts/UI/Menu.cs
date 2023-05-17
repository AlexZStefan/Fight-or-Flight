using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    GameObject MainMenu;
    [SerializeField]
    GameObject CharSelectMenu;
    [SerializeField]
    GameObject MapSelectMenu;
    [SerializeField]
    GameObject LanguageMenu;
    [SerializeField]
    GameObject InGameUI;
    [SerializeField]
    GameObject QuitPrompt;

    public GameObject pauseMenu;
    public GameObject winMenu;
    [SerializeField]
    private GameObject optionsMenu;

    public GameObject AvatarP1;
    public GameObject AvatarP2;

    public bool quitPrompt = false;

    UnityEngine.EventSystems.BaseEventData selectCharacter;

    [field: Header("Character Buttons")]
    List<Button> characterButtons = new List<Button>();
    [field: SerializeField] public Button char1 { get; private set; }

    [field: Header("Map Buttons")]
    List<Button> mapButtons = new List<Button>();

    public static Menu instance;

    private void Start()
    {
        if (instance) return;

        MapSelectMenu.SetActive(false);
        CharSelectMenu.SetActive(false);
        InGameUI.SetActive(false);

        foreach (var b in CharSelectMenu.transform.Find("PlayerButtons").GetComponentsInChildren<Button>())
        {
            characterButtons.Add(b);
            b.onClick.AddListener(() => SelectCharacter(b));
        }

        foreach (var b in MapSelectMenu.GetComponentsInChildren<Button>())
        {
            mapButtons.Add(b);
            b.onClick.AddListener(() => SelectMap(b));
        }
        LanguageMenu.GetComponentInChildren<Button>().Select();

        FMOD.Studio.PLAYBACK_STATE musicState;
        AudioManager.instance.menuMusic.getPlaybackState(out musicState);
        if (FMOD.Studio.PLAYBACK_STATE.STOPPED == musicState)
        {
            AudioManager.instance.menuMusic.start();
        }

        instance = this;
    }

    public void ChangeResolutionHD()
    {
        Screen.SetResolution(1920, 1080, true);
    }

    public void ChangeResolution1280x720()
    {
        Screen.SetResolution(1280, 720, true);
    }

    public void Restart()
    {
        GameManager.instance.ResetGame();
    }

    public void PlayMenuMusic()
    {
        AudioManager.instance.menuMusic.start();
    }

    public void Resume()
    {
        GameManager.instance.isPaused = !GameManager.instance.isPaused;
        pauseMenu.SetActive(GameManager.instance.isPaused);
    }

    public void ReturnToMainMenu()
    {
        MainMenu.SetActive(true);
        MapSelectMenu.SetActive(false);
        CharSelectMenu.SetActive(false);

        pauseMenu.SetActive(false);
        winMenu.SetActive(false);
        optionsMenu.SetActive(false);
        LanguageMenu.SetActive(false);
        MainMenu.GetComponentInChildren<Button>().Select();
    }

    public void ToogleCharSelectMenu()
    {
        AudioManager.instance.PlayOneShot(FModEvents.instance.ok, Vector3.zero);
        MainMenu.SetActive(false);
        MapSelectMenu.SetActive(false);
        CharSelectMenu.SetActive(true);
        Button b = CharSelectMenu.GetComponentInChildren<Button>();
        b.Select();
        b.OnSelect(selectCharacter);
    }

    public void ToogleOptionsMenu()
    {
        AudioManager.instance.PlayOneShot(FModEvents.instance.ok, Vector3.zero);
        if (optionsMenu.activeSelf)
        {
            optionsMenu.SetActive(false);
            MainMenu.SetActive(true);
            MainMenu.GetComponentInChildren<Button>().Select();
        }
        else
        {
            MainMenu.SetActive(false);
            optionsMenu.SetActive(true);
            optionsMenu.GetComponentInChildren<Button>().Select();
        }
    }

    public void ToogleMapSelectMenu()
    {
        MainMenu.SetActive(false);
        MapSelectMenu.SetActive(true);
        CharSelectMenu.SetActive(false);

        Button[] b = MapSelectMenu.GetComponentsInChildren<Button>();
        b[0].Select();

        AudioManager.instance.menuMusic.setParameterByName("Loop", 0);
    }

    public void SelectCharacter(Button button)
    {
        AudioManager.instance.PlayOneShot(FModEvents.instance.charSelect, Vector3.zero);
        foreach (var v in InGameCharacters.instance.characters)
        {
            if (button.name == v.name)
            {
                if (GameManager.instance.playerOne.characterSelected.Length == 0)
                {
                    GameManager.instance.playerOne.characterSelected = v.name;
                }
                else if (GameManager.instance.playerTwo.characterSelected.Length == 0)
                {
                    GameManager.instance.playerTwo.characterSelected = v.name;
                    ToogleMapSelectMenu();
                }
            }
        }
    }

    public void SelectMap(Button mapSelect)
    {
        AudioManager.instance.PlayOneShot(FModEvents.instance.mapSelect, Vector3.zero);
        Debug.Log("MAP SELECTED ");
        foreach (var m in MapSelector.instance.maps)
        {
            if (m.name == mapSelect.name)
            {
                GameManager.instance.mapSelected = m.name;
                Debug.Log("MAP SELECTED " + m.name);
            }
        }

        MainMenu.SetActive(false);
        MapSelectMenu.SetActive(false);
        CharSelectMenu.SetActive(false);

        AudioManager.instance.menuMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        GameManager.instance.StartGame();
    }

    public void ActivateInGameUI()
    {
        InGameUI.SetActive(true);
    }
    public void DisableInGameUI()
    {
        InGameUI.SetActive(false);
    }

    public void Quit()
    {
        quitPrompt = !quitPrompt;
        QuitPrompt.SetActive(quitPrompt);
    }
    private void Update()
    {
        if (MainMenu.activeInHierarchy == true)
        {
            if (quitPrompt == true)
            {
                //if (Gamepad.all[0].buttonSouth.isPressed)
                //{
                //    QuitPrompt.SetActive(false);
                //    quitPrompt = false;
                //}
                //if (Gamepad.all[0].buttonEast.isPressed)
                //{
                //    QuitPromptValue();
                //}

                if (Gamepad.current[UnityEngine.InputSystem.LowLevel.GamepadButton.Circle].isPressed)
                {
                    //Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                    QuitPromptValue();
                }

                if (Gamepad.current[UnityEngine.InputSystem.LowLevel.GamepadButton.Cross].isPressed)
                {

                    //Debug.Log("BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB");
                }
            }
            //if (Input.GetButton("Fire2") ;
        }
    }

    public void QuitPromptValue()
    {
        Debug.Log("Game quited");
        Application.Quit();
    }
}
