using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerController))]
public class UserInput : MonoBehaviour
{

	private MainInput inputActions;
	private MainInput InputActions { get
		{
			if(inputActions == null)
			{
				inputActions = new MainInput();
			}
			return inputActions;
		}
	}

	private PlayerController controller;
	private Vector3 lastMousePos;

	bool runIsPressed = false;





	private void OnEnable()
	{
		InputActions.Enable();
	}
	private void OnDisable()
	{
		InputActions.Disable();

	}
	private void Awake()
	{
		InputActions.Gameplay.Movement.performed += ctx => MovementCallback(ctx);
		InputActions.Gameplay.Run.performed += ctx => RunPressedCallback(ctx);
	}

	private void Start()
	{
		controller = GetComponent<PlayerController>();
	}

	public void RunPressedCallback(InputAction.CallbackContext ctx)
	{
		runIsPressed = ctx.ReadValue<float>() != 0;
	}

	public void MovementCallback(InputAction.CallbackContext ctx)
	{
		controller.Move(ctx.ReadValue<Vector2>(), runIsPressed);
	}

	private void Update()
	{

		Vector3 mousePos = InputActions.Gameplay.MousePos.ReadValue<Vector2>();
		if (mousePos != lastMousePos)
		{
			lastMousePos = mousePos;
			Ray cameraRay = Camera.main.ScreenPointToRay(mousePos);

			Vector3 pointOnPlane;
			var horizontalPlane = new Plane(Vector3.up, Vector3.zero);
			if (RayPlaneIntersection(cameraRay, horizontalPlane, out pointOnPlane))
			{
				Vector3 forward = pointOnPlane - transform.position;
				controller.LookAt(forward);
			}
		}
	}

	private void OldInput()
	{
		float deltaX = Input.GetAxis("Horizontal");
		float deltaZ = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(deltaX, 0, deltaZ);
		bool run = Input.GetButton("Run");
		if (movement != Vector3.zero)
		{
			controller.Move(movement, run);
		}

		Vector3 mousePos = Input.mousePosition;
		if (mousePos != lastMousePos)
		{
			lastMousePos = mousePos;
			Ray cameraRay = Camera.main.ScreenPointToRay(mousePos);

			Vector3 pointOnPlane;
			var horizontalPlane = new Plane(Vector3.up, Vector3.zero);
			if (RayPlaneIntersection(cameraRay, horizontalPlane, out pointOnPlane))
			{
				Vector3 forward = pointOnPlane - transform.position;
				controller.LookAt(forward);
			}
		}
	}

	private bool RayPlaneIntersection(Ray ray, Plane plane, out Vector3 hitPoint)
	{
		float intesectionDistance;
		hitPoint = Vector3.positiveInfinity;

		if(plane.Raycast(ray, out intesectionDistance))
		{
			hitPoint = ray.GetPoint(intesectionDistance);
			return true;
		}
		return false;
	}
}
