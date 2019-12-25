using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
	public float speed = 6.0f;
	public float turnSpeed = 3.0f;

	private CharacterController charController;
	private RaycastHit lookAtHit;

	void Start()
	{
		charController = GetComponent<CharacterController>();
	}
	public void LookAt(Vector3 forward)
	{
		forward.y = 0;
		forward.Normalize();

		transform.rotation = Quaternion.Lerp(
			transform.rotation,
			Quaternion.LookRotation(forward, Vector3.up),
			1 - Mathf.Exp(-turnSpeed * Time.deltaTime)
			);
		return;
	}

	public void Move(Vector3 movement)
	{
		movement *= speed;

		movement = Vector3.ClampMagnitude(movement, speed);
		movement *= Time.deltaTime;
		charController.Move(movement);

	}

}
