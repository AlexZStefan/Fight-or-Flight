using System;
using UnityEngine;
//using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.InputSystem;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        public Transform m_Cam;                  // A reference to the main camera in the scenes transform

        [SerializeField]
        Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
        public bool isMoving;
        //public UnityEvent ; 

        private PlayerAct playerActions;
        private Vector2 movement; // A, D
        private float crouchPA;     // S
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


        private void Start()
        {
            //Set Cursor to not be visible
            Cursor.visible = false;

            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
              /*  Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them! */
            }

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();
        }

        void OnApplicationFocus(bool status)
	{
		if (status)
		{
			/*Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;*/
		}
	}

        private void Update()
        {
            if (!m_Jump)
            {
                m_Jump = jump > 0.1 ? true : false;
            }
        }


        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            // fix the character on the right z axis
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);

            crouchPA = playerActions.Player.Crouch.ReadValue<float>();
            movement = playerActions.Player.Movement.ReadValue<Vector2>();
            jump = playerActions.Player.Jump.ReadValue<float>();
            melee = playerActions.Player.LightAttack.ReadValue<float>();
            ranged = playerActions.Player.RangedAttack.ReadValue<float>();
                                  
            // read inputs
            // float h = CrossPlatformInputManager.GetAxis("Horizontal");
            //   float v = CrossPlatformInputManager.GetAxis("Vertical");
            //if (movement != Vector2.zero)
            //{
            //    Debug.Log("Movement: " + movement);
            //    transform.position -= new Vector3(movement.x, 0, movement.y) * 3 * Time.deltaTime;

            //    //animation for movement                
            //    transform.forward = new Vector3(-movement.x, 0, -movement.y) * Time.deltaTime;
            //}

            float h = movement.y > 0 ? 1 : 0;
            float v = movement.x > 0 ? 1 : 0;
            bool crouch = crouchPA >0 ? true : false;

            if( h != 0  || v != 0)
            {
                isMoving = true;
            }
            else
                isMoving = false;

            // calculate move direction to pass to character
            if (m_Cam != null)
            {
                // calculate camera relative direction to move:
                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3((1), 0, (1))).normalized;
                m_Move = new Vector3(movement.x * 100, 0, 0);

            }
            else
            {
                // we use world-relative directions in the case of no main camera
                m_Move = new Vector3(-movement.x*100, 0, 0);
                
            }

       

#if !MOBILE_INPUT
            // walk speed multiplier
            if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
            
#endif

            Debug.Log(m_Move.ToString() +" "+ crouch+ " " + m_Jump);
            // pass all parameters to the character control script
            m_Character.Move(m_Move, crouch, m_Jump);
            m_Jump = false;
        }
    }
}
