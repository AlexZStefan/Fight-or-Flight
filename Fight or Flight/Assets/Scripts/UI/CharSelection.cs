using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharSelection : MonoBehaviour
{
    public static CharSelection instance;
    public List<Sprite> charImages = new List<Sprite>();
    public List<Sprite> avatarImages = new List<Sprite>();
    public List<Sprite> mapImages = new List<Sprite>();
    public Image player1Frame;
    public Image player2Frame;
    public Image mapFrame;

    private void Start()
    {
        if (instance == null) instance = this;
    }

    public void ChangeImage(GameObject button)
    {
        Debug.Log("Image should change");
        Debug.Log("Button  " + button.name);

        foreach (var i in charImages)
        {
            if (i.name == button.name + "_char")
            {
                Debug.Log("Image Is same" + i.name);

                if (GameManager.instance.playerOne.characterSelected.Length == 0)
                {
                    player1Frame.sprite = i;
                }
                else
                    player2Frame.sprite = i;

            }
        }
    }

    public void ChangeAvatarImage(int playerNumber)
    {
        if (playerNumber == 1)
        {
            foreach (var i in avatarImages)
            {
                if (GameManager.instance.playerOne.characterSelected + "_avatar1" == i.name)
                {
                    Menu.instance.AvatarP1.GetComponent<Image>().sprite = i;
                }
            }
        }
        else
        {
            foreach (var i in avatarImages)
            {
                if (GameManager.instance.playerTwo.characterSelected + "_avatar2" == i.name)
                {
                    Menu.instance.AvatarP2.GetComponent<Image>().sprite = i;
                }
            }
        }
    }

    public void ChangeMapImage(GameObject button)
    {
        foreach (var i in charImages)
        {
            if (i.name == button.name)
            {
                mapFrame.sprite = i;
            }
        }
    }
}
