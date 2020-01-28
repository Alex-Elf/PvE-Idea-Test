using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public new Rigidbody rigidbody;

	[SerializeField] private BulletStats bulletStats;
	[SerializeField] private BackToPool backToPool;

	private void OnCollisionEnter(Collision collision)
	{
		gameObject.SetActive(false);
		backToPool.ReturnToPool();
	}
}
