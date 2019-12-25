using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class UserPlayerControlInput : MonoBehaviour
{
	private PlayerController controller;
	private void Start()
	{
		controller = GetComponent<PlayerController>();
	}
	private void Update()
	{

		float deltaX = Input.GetAxis("Horizontal");
		float deltaZ = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(deltaX, 0, deltaZ);


		if (movement != Vector3.zero)
		{
			controller.Move(movement);
		}


		var cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

		Vector3 pointOnPlane;
		Plane horizontalPlane = new Plane(Vector3.up, Vector3.zero);
		if (RayPlaneIntersection(cameraRay, horizontalPlane, out pointOnPlane))
		{
			var forward = pointOnPlane - transform.position;
			controller.LookAt(forward);
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
