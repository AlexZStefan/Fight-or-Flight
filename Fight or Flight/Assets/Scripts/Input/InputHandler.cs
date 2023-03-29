using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public ThirdPersonUserControl userControl;
    PlayerInput playerInput;

    void Awake()
    {
        // only do asign player input once for each character
        if (userControl == null)
        {
            var characters = FindObjectsOfType<ThirdPersonUserControl>();
            var pInput = GetComponent<PlayerInput>();
            var index = pInput.playerIndex;

            this.name = "Player: " + index;
            foreach (var c in characters)
            {
                if (c.playerIndex == index)
                {
                    if (c.playerIndex == 2)
                    {
                        //foreach(var k in InputSystem.devices)
                        //{

                        Debug.Log("Change this when 2 players controllers");
                        //}
                        // Debug.Log("InputSystem dev " + InputSystem.devices);
                        //InputSystem.AddDevice<Keyboard>();
                        // pInput.SwitchCurrentControlScheme("Player2", InputSystem.devices[0]);
                        pInput.SwitchCurrentActionMap("Player2");

                    }
                    userControl = c;
                }
            };
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        userControl.movement = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        
          userControl.jump = context.ReadValue<float>();
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        userControl.crouchPA = context.ReadValue<float>();
    }

    public void OnLightAttack(InputAction.CallbackContext context)
    {
        userControl.lightAttack = context.ReadValue<float>();
    }

    public void OnRangedAttack(InputAction.CallbackContext context)
    {        
        userControl.rangedAttack = context.ReadValue<float>();
    }

    public void OnHeavyAttack(InputAction.CallbackContext context)
    {       
        userControl.heavyAttack = context.ReadValue<float>();
    }
}
