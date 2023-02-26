using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Menu : MonoBehaviour
{
    [SerializeField]
    GameObject MainMenu;
    [SerializeField]
    GameObject CharSelectMenu;
    [SerializeField]
    GameObject MapSelectMenu;
    [SerializeField]
    GameObject BackgroundImage; 
    [SerializeField]
    GameObject InGameUI;

    public static Menu instance; 

    private void Start()
    {
        if(!instance) instance = this;

        BackgroundImage.SetActive(true);
        MainMenu.SetActive(true);
        MapSelectMenu.SetActive(false);
        CharSelectMenu.SetActive(false);
        InGameUI.SetActive(false);
        
        // set text namae to each character 
        // add event listeners
        Button[] buttons =  CharSelectMenu.GetComponentsInChildren<Button>();

        for(int i = 0;  i < InGameCharacters.instance.characters.Count; i++)
        {
            buttons[i].GetComponent<Text>().text = InGameCharacters.instance.characters[i].name;
            buttons[i].name = InGameCharacters.instance.characters[i].name;
            

            buttons[i].onClick.AddListener(() => SelectCharacter(buttons[i]));      
        }

        // set text name to each map 
        // add event listeners
        Button[] mapButtons = MapSelectMenu.GetComponentsInChildren<Button>();

        for (int i = 0; i < MapSelector.instance.maps.Count; i++)
        {
            mapButtons[i].GetComponent<Text>().text = MapSelector.instance.maps[i].name;
            mapButtons[i].name = MapSelector.instance.maps[i].name;
            //Debug.Log(MapSelector.instance.maps[i].name);
            // There is a bug here - map is selected as map 0 for temp fix
           
            mapButtons[i].onClick.AddListener(() => SelectMap(mapButtons[i]));    
        }

        FMOD.Studio.PLAYBACK_STATE musicState;
        AudioManager.instance.menuMusic.getPlaybackState(out musicState);
        if (FMOD.Studio.PLAYBACK_STATE.STOPPED == musicState)
        {
            AudioManager.instance.menuMusic.start();
        }
    }

    public void ReturnToMainMenu()
    {
        MainMenu.SetActive(true);
        MapSelectMenu.SetActive(false);
        CharSelectMenu.SetActive(false);      
    }

    public void ToogleCharSelectMenu()
    {
        AudioManager.instance.PlayOneShot(FModEvents.instance.ok, Vector3.zero);
        MainMenu.SetActive(false);
        MapSelectMenu.SetActive(false);
        CharSelectMenu.SetActive(true);
        Button b =  CharSelectMenu.GetComponentInChildren<Button>();        
        b.Select();
    }

    public void ToogleOptionsMenu()
    {
        AudioManager.instance.PlayOneShot(FModEvents.instance.ok, Vector3.zero);
        MainMenu.SetActive(true);
      
        //Button b = CharSelectMenu.GetComponentInChildren<Button>();
        //b.Select();
    }

    public void ToogleMapSelectMenu()
    {        
        MainMenu.SetActive(false);
        MapSelectMenu.SetActive(true);
        CharSelectMenu.SetActive(false);

        Button []b = MapSelectMenu.GetComponentsInChildren<Button>();
        b[0].Select();

        // AudioManager.instance.menuMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        AudioManager.instance.menuMusic.setParameterByName("Loop", 0);
    }

    public void SelectCharacter(Button button)
    {       
        
        AudioManager.instance.PlayOneShot(FModEvents.instance.charSelect, Vector3.zero);
        foreach (var v in InGameCharacters.instance.characters)
        {
            if (button.name == v.name)
            {
                Debug.Log(button.name);
                
            }
        }        

       // if (GameManager.instance.playerOne.characterSelected.Length > 0) //&& playerTwo.characterSelected == "")        
            ToogleMapSelectMenu();        
    }

    public void SelectMap(Button mapSelect) 
    {
        AudioManager.instance.PlayOneShot(FModEvents.instance.mapSelect, Vector3.zero);
     
        foreach(var m in MapSelector.instance.maps)
        {
            if (m.name == GameManager.instance.mapSelected)
            {                
                Debug.Log("MAP SELECTED " + m.name);                
            }
        }

        MainMenu.SetActive(false);
        MapSelectMenu.SetActive(false);
        CharSelectMenu.SetActive(false);
        BackgroundImage.SetActive(false);

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
        Application.Quit();
    }

}
