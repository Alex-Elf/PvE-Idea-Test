using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public new Rigidbody rigidbody;

	[SerializeField] private BulletStats bulletStats;


	private void OnCollisionEnter(Collision collision)
	{
		Destroy(gameObject);
	}
}
