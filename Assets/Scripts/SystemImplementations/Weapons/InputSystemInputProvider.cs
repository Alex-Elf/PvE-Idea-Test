using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Character.Weapons
{
    public class InputSystemInputProvider : WeaponsInputProvider
    {
        [SerializeField] protected string actionPath = "Gameplay/Fire";

        private PlayerInput playerInput;

        [Inject]
        private void Construct(DiContainer diContainer)
        {
            playerInput = diContainer.Resolve<PlayerInput>();
        }

        protected virtual void Start()
        {
            var runAction = playerInput.currentActionMap.FindAction(actionPath);
            if (runAction != null)
            {
                runAction.performed += Action_performed;
                runAction.canceled += Action_canceled;
            }
        }

        private void Action_performed(InputAction.CallbackContext obj)
        {
        }

        private void Action_canceled(InputAction.CallbackContext obj)
        {
        }

    }
}
