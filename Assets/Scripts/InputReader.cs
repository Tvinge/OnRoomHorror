using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static PlayerInputActions;

namespace AE
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "Input/InputReader")]
    public class InputReader : ScriptableObject, IPlayerActions
    {
        public event UnityAction<Vector2> Move = delegate { };
        public event UnityAction<Vector2, bool> Look = delegate { };
        public event UnityAction EnableMouseControlCamera = delegate { };
        public event UnityAction DisableMouseControlCamera = delegate { };
        public event UnityAction Interact = delegate { };

        PlayerInputActions inputActions;

        void OnEnable()
        {
            if (inputActions == null)
            {
                inputActions = new PlayerInputActions();
                inputActions.Player.SetCallbacks(this);
            }
        }
        public void EnablePlayerActions()
        {
           inputActions.Player.Enable();
        }
        public Vector3 Direction => inputActions.Player.Move.ReadValue<Vector2>();

        void IPlayerActions.OnLook(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }

        void IPlayerActions.OnInteract(InputAction.CallbackContext context) 
        {
            if (context.phase == InputActionPhase.Performed)
            {
                Interact.Invoke();
            }
        }
        void IPlayerActions.OnMove(InputAction.CallbackContext context)
        {
            Move.Invoke(arg0: context.ReadValue<Vector2>());
        }

    }
}



