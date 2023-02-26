using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputScript : MonoBehaviour
{
    private PlayerAct playerActions;
    private Vector2 movement; // A, D
    private float crouch;     // S
    private float jump;       // W
    private float ranged;     // E
    private float melee;      // Q

    void Awake()
    {
        playerActions = new PlayerAct();
    }

    private void OnEnable()
    {
        playerActions.Player.Enable();
    }

    private void OnDisable()
    {
        playerActions.Player.Disable();
    }

    private void FixedUpdate()
    {
        movement = playerActions.Player.Movement.ReadValue<Vector2>();
        if (movement != Vector2.zero)
        {
            Debug.Log("Movement: " + movement);
            transform.position -= new Vector3(movement.x, 0, movement.y)*3 * Time.deltaTime;
            transform.forward = new Vector3(-movement.x, 0, -movement.y) * Time.deltaTime;
        }
        crouch = playerActions.Player.Crouch.ReadValue<float>();
        if(crouch != 0f) Debug.Log("Crouch: " + crouch);

        jump = playerActions.Player.Jump.ReadValue<float>();
        if (jump != 0f)
        {
            Debug.Log("Jump: " + jump);


            // if is Grounded
            AudioManager.instance.PlayOneShot(FModEvents.instance.jumpSound, Vector3.zero);
        }
        ranged = playerActions.Player.RangedAttack.ReadValue<float>();
        if (ranged != 0f) Debug.Log("Ranged: " + ranged);

        melee = playerActions.Player.LightAttack.ReadValue<float>();
        if (melee != 0f) Debug.Log("Melee: " + melee);
    }
}
