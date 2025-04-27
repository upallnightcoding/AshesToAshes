using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace A2A
{
    public class PlayerInputCntrl : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput;

        public Vector2 Move { get; private set; }
        public Vector2 Look { get; private set; }
        public bool Run { get; private set; }
        public bool Fire { get; set; } = false;

        private InputActionMap inputActionMap;

        private InputAction moveAction;
        private InputAction lookAction;
        private InputAction runAction;
        private InputAction fireAction;

        private void Awake()
        {
            inputActionMap = playerInput.currentActionMap;

            moveAction = inputActionMap.FindAction("Move");
            lookAction = inputActionMap.FindAction("Look");
            runAction = inputActionMap.FindAction("Run");
            fireAction = inputActionMap.FindAction("Fire");

            fireAction.started += OnFire;

            moveAction.performed += OnMove;
            lookAction.performed += OnLook;
            runAction.performed += OnRun;
            //fireAction.performed += OnFire;

            moveAction.canceled += OnMove;
            lookAction.canceled += OnLook;
            runAction.canceled += OnRun;
            //fireAction.canceled += OnFire;
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            Move = context.ReadValue<Vector2>();
        }

        private void OnLook(InputAction.CallbackContext context)
        {
            Look = context.ReadValue<Vector2>();
        }

        private void OnRun(InputAction.CallbackContext context)
        {
            Run = context.ReadValueAsButton();
        }

        private void OnFire(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Fire = true;
            }

            if (context.canceled)
            {
                //Fire = false;
            }
            //Fire = context.ReadValueAsButton();
        }

        private void OnEnable()
        {
            inputActionMap.Enable();
        }

        private void OnDisable()
        {
            inputActionMap.Disable();
        }
    }
}


