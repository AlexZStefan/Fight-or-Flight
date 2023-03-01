using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //if (other.gameObject == GameManager.instance.playerOne.character)
            //{
            //    GameManager.instance.playerOne.LowPunch();
            //}
            if (other.gameObject == GameManager.instance.playerTwo.character)
            {
                GameManager.instance.playerTwo.LowPunch();                
            }
        }
    }
}
