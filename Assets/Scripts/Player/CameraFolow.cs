using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFolow : MonoBehaviour
{
	public Transform Target;
	public float CameraSpeed;


	private Vector3 offset;
	private Vector3 desiredPos;
	private float threshold = 0.01f;

	private void Start()
	{
		offset = Target.position - transform.position;
	}
	private void Update()
	{
		desiredPos = Target.position - offset;

		if ((transform.position - desiredPos).sqrMagnitude > threshold)
		{
			transform.position = Vector3.Lerp(transform.position,
				desiredPos,
				1 - Mathf.Exp(-CameraSpeed * Time.deltaTime)
				);
		}
	}

}
