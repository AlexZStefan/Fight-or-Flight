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

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

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
        crouch = playerActions.Player.Crouch.ReadValue<float>();
        movement = playerActions.Player.Movement.ReadValue<Vector2>();
        jump = playerActions.Player.Jump.ReadValue<float>();
        melee = playerActions.Player.LightAttack.ReadValue<float>();
        ranged = playerActions.Player.RangedAttack.ReadValue<float>();


        if (movement != Vector2.zero)
        {
            Debug.Log("Movement: " + movement);
            transform.position -= new Vector3(movement.x, 0, movement.y) * 3 * Time.deltaTime;

            //animation for movement
            anim.Play("Run");
            transform.forward = new Vector3(-movement.x, 0, -movement.y) * Time.deltaTime;

        }
        else if (crouch != 0f)
        {
            Debug.Log("Crouch: " + crouch);
            anim.Play("Crouch");
        }
        else if (jump != 0f)
        {
            Debug.Log("Jump: " + jump);
            anim.Play("Jump");
            // if is Grounded
            AudioManager.instance.PlayOneShot(FModEvents.instance.jumpSound, Vector3.zero);

        }
        else if (melee != 0f)
        {
            Debug.Log("Melee: " + melee);
            anim.Play("Meelee");
        }

        else if (ranged != 0f) 
        {
            Debug.Log("Ranged: " + ranged);
            anim.Play("Range");
        }

        else
        {
            // idle animation
            anim.Play("Idle");
        }
    }

}


