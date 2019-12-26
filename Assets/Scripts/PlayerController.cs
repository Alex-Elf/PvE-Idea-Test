using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
	[Header("Movement")]
	public bool canMove = true;
	public float speed = 6.0f;

	[Header("Look")]
	public bool canRotate = true;
	public bool inversedRotation = false;
	public float turnSpeed = 3.0f;


	private CharacterController charController;
	private RaycastHit lookAtHit;

	private Vector3 targetForward;
	private float threshold = 0.01f;
	void Start()
	{
		charController = GetComponent<CharacterController>();
		targetForward = transform.forward;
	}
	private void Update()
	{
		if (canRotate && Vector3.SqrMagnitude(targetForward - transform.forward) > threshold)
		{
			transform.rotation = Quaternion.Lerp(
				transform.rotation,
				Quaternion.LookRotation(targetForward, Vector3.up),
				1 - Mathf.Exp(-turnSpeed * Time.deltaTime)
				);
		}
	}
	public void LookAt(Vector3 forward)
	{
		forward.y = 0;
		forward.Normalize();
		forward = (inversedRotation)? Vector3.Reflect(-forward, Vector3.forward): forward;		targetForward = forward;
	}

	public void Move(Vector2 movementHorisontal, bool run)
	{
		if (canMove)
		{
			Vector3 movement = new Vector3(movementHorisontal.x, 0, movementHorisontal.y);

			movement *= speed;

			movement = Vector3.ClampMagnitude(movement, speed);
			movement *= Time.deltaTime;
			charController.Move(movement);

		}

	}
}
