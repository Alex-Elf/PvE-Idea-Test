using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

[RequireComponent(typeof(PlayerMovementController))]
public class FirstPesonRotationController : MonoBehaviour
{
	[SerializeField] private Vector2 mouseSensitivity = Vector2.one;
	[SerializeField] private Transform cameraTransform;
    [SerializeField] private float cameraXRotationMin = -90;
    [SerializeField] private float cameraXRotationMax = 90;

    private PlayerInput playerInput;

	[Inject] private void Construct(DiContainer diContainer) 
	{
		playerInput = diContainer.Resolve<PlayerInput>();
	}

	private PlayerMovementController controller;
	private Camera targetCamera;
	private float cameraXRotation = 0;

	private void Start()
	{
		targetCamera = Camera.main;
		controller = GetComponent<PlayerMovementController>();
		var mousePosAction = playerInput.actions.FindAction("Gameplay/Look");
		if(mousePosAction != null)
        {
            mousePosAction.performed += MouseLookCallback;
			Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            cameraTransform.localRotation = Quaternion.Euler(cameraXRotation, 0, 0);
        }
    }

    private void MouseLookCallback(InputAction.CallbackContext ctx)
    {
        var delta = ctx.ReadValue<Vector2>();
		var yAxisRotateAmount = delta.x * mouseSensitivity.x * Time.deltaTime;
        controller.CharacterController.transform.Rotate(yAxisRotateAmount * Vector3.up, Space.Self);

		var xAxisRotateAmount = delta.y * mouseSensitivity.y * Time.deltaTime;
		cameraXRotation = Mathf.Clamp(cameraXRotation - xAxisRotateAmount, cameraXRotationMin, cameraXRotationMax);
        cameraTransform.localRotation = Quaternion.Euler(cameraXRotation, 0, 0);
	}
}
