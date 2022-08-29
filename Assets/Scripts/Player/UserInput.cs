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




	private Vector3 v3up = Vector3.up, v3zero = Vector3.zero;
	private readonly Vector2 v2zero = Vector2.zero;

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
		InputActions.Gameplay.Run.performed += RunPressedCallback;
		InputActions.Gameplay.MousePos.performed += MouseMovementCallback;
		InputActions.Gameplay.AttackMain.performed += AttackMainCallback;
	}

	private void Start()
	{
		controller = GetComponent<PlayerController>();
	}

	public void AttackMainCallback(InputAction.CallbackContext ctx)
	{
		controller.AttackMain(ctx.ReadValue<float>() != 0);
	}

	public void RunPressedCallback(InputAction.CallbackContext ctx)
	{
		runIsPressed = ctx.ReadValue<float>() != 0;
	}
	public void MouseMovementCallback(InputAction.CallbackContext ctx)
	{
		//Vector3 mousePos = InputActions.Gameplay.MousePos.ReadValue<Vector2>();
		Vector3 mousePos = ctx.ReadValue<Vector2>();

		Ray cameraRay = Camera.main.ScreenPointToRay(mousePos);

		Vector3 pointOnPlane;
		var horizontalPlane = new Plane(v3up, v3zero);
		if (RayPlaneIntersection(cameraRay, horizontalPlane, out pointOnPlane))
		{
			Vector3 forward = pointOnPlane - transform.position;
			controller.LookAt(forward);
		}
		
	}

	private void Update()
	{
		Vector2 movementInput = InputActions.Gameplay.Movement.ReadValue<Vector2>();
		if(movementInput != v2zero)
		{
			controller.Move(movementInput, runIsPressed);
		}

		//Vector3 mousePos = InputActions.Gameplay.MousePos.ReadValue<Vector2>();
		//if (mousePos != lastMousePos)
		//{
		//	lastMousePos = mousePos;
		//	Ray cameraRay = Camera.main.ScreenPointToRay(mousePos);

		//	Vector3 pointOnPlane;
		//	var horizontalPlane = new Plane(v3up, v3zero);
		//	if (RayPlaneIntersection(cameraRay, horizontalPlane, out pointOnPlane))
		//	{
		//		Vector3 forward = pointOnPlane - transform.position;
		//		controller.LookAt(forward);
		//	}
		//}
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
