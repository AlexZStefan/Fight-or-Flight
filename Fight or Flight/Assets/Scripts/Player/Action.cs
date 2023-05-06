using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Action : MonoBehaviour
{
    public bool wasTriggered = false;
    public int damage = 10;
    public Slider healthBar;
    public int ultimate = 1;

    private void OnTriggerEnter(Collider other)
    {        
        if (other.tag == "Player" && wasTriggered == false)
        {
            // add force to the hit player in the right direction
            var rb = other.transform.GetComponent<Rigidbody>();
            float staminaLeft = 0;
            var directionOfPush = (transform.root.position - other.transform.root.position).normalized;          
         
            // check and deduct health acording to the damage
            if (other.gameObject == GameManager.instance.playerOne.character)
            {
                if (healthBar == null)
                {
                    healthBar = GameObject.Find("HealthBarP1").GetComponent<Slider>();
                }
                GameManager.instance.playerOne.stamina -= 10;
                staminaLeft = GameManager.instance.playerOne.stamina;
                healthBar.value = GameManager.instance.playerOne.stamina;

                // Play character get hit animation 

                if (GameManager.instance.playerOne.stamina < 1)
                {
                    rb.AddForce(new Vector3(-directionOfPush.x * (100 - staminaLeft) * ultimate, 1, 0), ForceMode.Impulse);

                }
                else
                {
                    rb.AddForce(new Vector3(-directionOfPush.x * (100 - staminaLeft), 5, 0), ForceMode.Impulse);
                }
            }

            if (other.gameObject == GameManager.instance.playerTwo.character)
            {
                if (healthBar == null)
                {
                    healthBar = GameObject.Find("HealthBarP2").GetComponent<Slider>();
                }
                GameManager.instance.playerTwo.stamina -= damage;
                healthBar.value = GameManager.instance.playerTwo.stamina;
                staminaLeft = GameManager.instance.playerTwo.stamina;
                // Play character get hit animation 

                if (GameManager.instance.playerTwo.stamina < 1)
                {
                    rb.AddForce(new Vector3(-directionOfPush.x * (100 - staminaLeft) * ultimate, 1, 0), ForceMode.Impulse);

                }
                else
                {
                    rb.AddForce(new Vector3(-directionOfPush.x * (100 - staminaLeft), 5, 0), ForceMode.Impulse);

                }
            }
                    


            AudioManager.instance.PlayOneShot(FModEvents.instance.block, Vector3.zero);
            transform.GetComponent<Collider>().enabled = false;
            wasTriggered = true;
        }

    }
}
