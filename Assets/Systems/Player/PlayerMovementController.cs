using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerMovementController : MonoBehaviour
{
	[SerializeField] private CharacterController charController;

	[Header("Movement")]
	[SerializeField] private bool checkForwardDirection = false;
	[SerializeField] private bool canMove = true;
	[SerializeField] private bool inversedMovement = false;
	[SerializeField] private float speed = 6.0f;
	[SerializeField] private float runSpeedMultiplier = 2f;

	[Header("Look")]
	[SerializeField] private bool canRotate = true;
	[SerializeField] private bool mirroredRotation = false;
	[SerializeField] private float turnSpeed = 3.0f;


	private Vector3 targetForward;
    private InputAction moveAction;
    private bool isRuning;
    private readonly float threshold = 0.001f;

	private PlayerInput playerInput;

	public CharacterController CharacterController => charController;
	public bool RotationControlledElsewhere { get; set; }

	[Inject] private void Construct(DiContainer diContainer)
	{
		playerInput = diContainer.Resolve<PlayerInput>();
	}

	private void Start()
	{
		targetForward = charController.transform.forward;

		moveAction = playerInput.currentActionMap.FindAction("Move");
		var runAction = playerInput.currentActionMap.FindAction("Run");
		if(runAction != null)
        {
            runAction.performed += RunAction_performed;
			runAction.canceled += RunAction_canceled;
		}
	}

    private void RunAction_performed(InputAction.CallbackContext obj)
    {
		isRuning = true;
	}
	private void RunAction_canceled(InputAction.CallbackContext obj)
	{
		isRuning = false;
	}

	private void FixedUpdate()
	{
		if (canRotate && RotationControlledElsewhere && Vector3.SqrMagnitude(targetForward - charController.transform.forward) > threshold)
		{
			charController.transform.rotation = Quaternion.Lerp(
				charController.transform.rotation,
				Quaternion.LookRotation(targetForward, Vector3.up),
				1 - Mathf.Exp(-turnSpeed * Time.deltaTime)
				);
		}
		
		if(canMove && moveAction != null)
		{
			Vector2 movementInput = moveAction.ReadValue<Vector2>();
			Move(movementInput, isRuning);
		}
	}
	public void LookAt(Vector3 forward)
	{
		Debug.Log(forward);
		forward.y = 0;
		forward.Normalize();
		forward = (mirroredRotation)? Vector3.Reflect(-forward, Vector3.forward): forward;
		targetForward = forward;
	}

	private void Move(Vector2 movementInput, bool run)
	{
        Vector3 movement = new(movementInput.x, 0, movementInput.y);

        var targetSpeedMultiplier = run ? runSpeedMultiplier : 1;
        var endSpeedMultiplier = speed * targetSpeedMultiplier * Time.deltaTime;

        movement *= endSpeedMultiplier;
        if (inversedMovement) movement = -movement;

        if (checkForwardDirection) movement = (charController.transform.forward * movement.z) + (charController.transform.right * movement.x);

        charController.Move(movement);
    }
}
