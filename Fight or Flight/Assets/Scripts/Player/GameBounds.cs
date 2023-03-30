using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameBounds : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(other.gameObject == GameManager.instance.playerOne.character)                
            {
                GameManager.instance.playerOne.KillPlayer();
                GameManager.instance.playerTwo.character.GetComponent<Rigidbody>().velocity = Vector3.zero;

                var healthBar = GameObject.Find("HealthBarP1").GetComponent<Slider>();
                healthBar.value = 100;
            }
            if(other.gameObject == GameManager.instance.playerTwo.character)
            {

                GameManager.instance.playerTwo.character.GetComponent<Rigidbody>().velocity = Vector3.zero;
                GameManager.instance.playerTwo.KillPlayer();

                var healthBar = GameObject.Find("HealthBarP2").GetComponent<Slider>();
                healthBar.value = 100;

            }

        }
    }

}
