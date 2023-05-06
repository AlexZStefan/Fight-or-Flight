using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public Text combo;
    public bool started = false;
    public float countdownTime = 3f;
    public int comboNumber = 0;
    int _playerNumber;
    private void Start()
    {
        if(GameManager.instance.playerOne.character.transform == transform)
        {
            _playerNumber = 1;
        }
        else if (GameManager.instance.playerTwo.character.transform == transform)
        {
            _playerNumber = 2;
        }
    }

    public void Hit(int playerNumber)
    {
        if(_playerNumber == playerNumber)
        {
            combo.gameObject.SetActive(true);
            countdownTime = 3f;
         started = true;
         comboNumber += 1;
            combo.text = comboNumber.ToString();
        }
    }


    void Update()
    {
        if(started == true)
        {
            
            if (countdownTime > 0)
            {
                countdownTime -= Time.deltaTime;               
            }
            else
            {
                combo.gameObject.SetActive(false);
                started = false;
                comboNumber = 0;
            }
        }
    }
}
