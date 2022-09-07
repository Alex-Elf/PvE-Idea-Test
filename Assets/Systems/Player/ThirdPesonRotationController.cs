using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

[RequireComponent(typeof(PlayerMovementController))]
public class ThirdPesonRotationController : MonoBehaviour
{
	private PlayerInput playerInput;

	[Inject] private void Construct(DiContainer diContainer) 
	{
		playerInput = diContainer.Resolve<PlayerInput>();
	}

	private PlayerMovementController controller;
	private Camera targetCamera;

	private void Start()
	{
		targetCamera = Camera.main;
		controller = GetComponent<PlayerMovementController>();
		var mousePosAction = playerInput.actions.FindAction("MousePosition");
		if(mousePosAction != null)
        {
            mousePosAction.performed += MouseMovementCallback;

		}
	}

	private void MouseMovementCallback(InputAction.CallbackContext ctx)
	{
		//Vector3 mousePos = InputActions.Gameplay.MousePos.ReadValue<Vector2>();
		Vector3 mousePos = ctx.ReadValue<Vector2>();

		Ray cameraRay = targetCamera.ScreenPointToRay(mousePos);

        var horizontalPlane = new Plane(Vector3.up, Vector3.zero);
        if (RayPlaneIntersection(cameraRay, horizontalPlane, out Vector3 pointOnPlane))
		{
			Vector3 forward = pointOnPlane - controller.CharacterController.transform.position;
			controller.LookAt(forward);
		}

	}

	private bool RayPlaneIntersection(Ray ray, Plane plane, out Vector3 hitPoint)
	{
        hitPoint = Vector3.positiveInfinity;

        if (plane.Raycast(ray, out float intesectionDistance))
		{
			hitPoint = ray.GetPoint(intesectionDistance);
			return true;
		}
		return false;
	}
}
