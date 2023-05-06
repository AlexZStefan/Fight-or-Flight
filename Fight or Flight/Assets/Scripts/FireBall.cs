using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireBall : MonoBehaviour
{
    int ultimate = 5;
    int damage = 15;

    private void Start()
    {
        StartCoroutine(DestroyObject());   
    }

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log(other.tag);
        if (other.transform.tag == "Player")
        {
            var directionOfPush = (transform.root.position - other.transform.root.position).normalized;

            var rb = other.transform.GetComponent<Rigidbody>();
            if (other.gameObject == GameManager.instance.playerOne.character)
            {
                var healthBar = GameObject.Find("HealthBarP1").GetComponent<Slider>();

                GameManager.instance.playerOne.stamina -= damage;
                healthBar.value = GameManager.instance.playerOne.stamina;
                var staminaLeft = GameManager.instance.playerOne.stamina;
                // Play character get hit animation 
                AudioManager.instance.PlayOneShot(FModEvents.instance.block, Vector3.zero);

                if (GameManager.instance.playerOne.stamina < 1)
                {
                    rb.AddForce(new Vector3(-directionOfPush.x * (100 - staminaLeft) * ultimate, 1, 0), ForceMode.Impulse);
                }
                else
                {
                    rb.AddForce(new Vector3(-directionOfPush.x * (100 - staminaLeft), 5, 0), ForceMode.Impulse);
                }

            }
            else if (other.gameObject == GameManager.instance.playerTwo.character)
            {
                var healthBar = GameObject.Find("HealthBarP2").GetComponent<Slider>();
                GameManager.instance.playerTwo.stamina -= damage;
                healthBar.value = GameManager.instance.playerTwo.stamina;
                var staminaLeft = GameManager.instance.playerTwo.stamina;
                // Play character get hit animation 
                AudioManager.instance.PlayOneShot(FModEvents.instance.block, Vector3.zero);

                if (GameManager.instance.playerTwo.stamina < 1)
                {
                    rb.AddForce(new Vector3(-directionOfPush.x * (100 - staminaLeft) * ultimate, 1, 0), ForceMode.Impulse);

                }
                else
                {
                    rb.AddForce(new Vector3(-directionOfPush.x * (100 - staminaLeft), 5, 0), ForceMode.Impulse);

                }
            }
            Destroy(gameObject);
        }
    }

    IEnumerator DestroyObject()
    {
        for(int i = 0; i < 10; i--)
        {
            yield return new WaitForSeconds(1);

            if(i == 0 && gameObject != null)
            {
                Destroy(gameObject);
            }
        }
    }
}

