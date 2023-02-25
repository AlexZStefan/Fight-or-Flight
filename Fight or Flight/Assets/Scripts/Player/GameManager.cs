using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public string mapSelected = "";

    public Player playerOne;
    public Player playerTwo;

    private void Awake()
    {
        if (!instance)
            instance = this;

        playerOne = new Player();
        playerTwo = new Player();
    }


    public void StartGame() 
    {
        // called from menu script uppond sellecting the map
        Debug.Log("GameStarted");

        Menu.instance.ActivateInGameUI();
    }

    public void EndGame()
    {
        // called from menu script uppond sellecting the map
        Debug.Log("GameStarted");

        Menu.instance.DisableInGameUI();
    }

}
