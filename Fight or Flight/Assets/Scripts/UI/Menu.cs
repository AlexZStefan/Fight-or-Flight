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
            Debug.Log(MapSelector.instance.maps[i].name);
            mapButtons[i].onClick.AddListener(()=> SelectMap(mapButtons[i]));
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
        MainMenu.SetActive(false);
        MapSelectMenu.SetActive(false);
        CharSelectMenu.SetActive(true);
        Button b =  CharSelectMenu.GetComponentInChildren<Button>();        
        b.Select();
    }

    public void ToogleMapSelectMenu()
    {
        MainMenu.SetActive(false);
        MapSelectMenu.SetActive(true);
        CharSelectMenu.SetActive(false);

        Button b = MapSelectMenu.GetComponentInChildren<Button>();
        b.Select();        
    }

    public void SelectCharacter(Button button)
    {
        foreach(var v in InGameCharacters.instance.characters)
        {
            if (button.name == v.name)
            {
                GameManager.instance.playerOne.characterSelected = button.name;
            }
        }        

       // if (GameManager.instance.playerOne.characterSelected.Length > 0) //&& playerTwo.characterSelected == "")
        
            ToogleMapSelectMenu();        
    }

    public void SelectMap(Button mapSelect) 
    {
        foreach(var m in MapSelector.instance.maps)
        {
            Debug.Log(m.name);
            if (m.name == mapSelect.name)
            {                
                Debug.Log(mapSelect.name);
                GameManager.instance.mapSelected = mapSelect.name;
            }
        }

        MainMenu.SetActive(false);
        MapSelectMenu.SetActive(false);
        CharSelectMenu.SetActive(false);
        BackgroundImage.SetActive(false);
        

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
