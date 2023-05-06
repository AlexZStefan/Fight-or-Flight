using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject fireballPrefab; 
    public float fireballSpeed = 10.0f;

    public void Fire()
    {
        // Get the player's transform component
        // Instantiate a fireball prefab
        GameObject fireball = Instantiate(fireballPrefab, transform.position + transform.forward + transform.up, Quaternion.identity);

        // Calculate the direction to launch the fireball
        Vector3 launchDirection = transform.forward *3;

        // Launch the fireball in the calculated direction
        fireball.GetComponent<Rigidbody>().velocity = launchDirection * fireballSpeed;
    }
}
