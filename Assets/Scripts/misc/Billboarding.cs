using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Billboarding : MonoBehaviour
{
	private bool isTracking;
	private void OnBecameInvisible()
	{
		isTracking = false;
	}
	private void OnBecameVisible()
	{
		isTracking = true;
	}
	private void Update()
	{
		if(isTracking)
			transform.LookAt(Camera.main.transform);
	}
}
