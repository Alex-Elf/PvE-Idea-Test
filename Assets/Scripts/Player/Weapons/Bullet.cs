using LocalObjectPooler;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public new Rigidbody rigidbody;

	private BulletStats bulletStats;

	private System.Action onBulletDespawn;
    private ComponentObjectPooler<Bullet> bulletPool;

    private void Start()
	{
		onBulletDespawn += Despawn;


	}

    private void OnCollisionEnter(Collision collision)
	{
		onBulletDespawn?.Invoke();
	}

    internal void Setup(ComponentObjectPooler<Bullet> bulletPool, BulletStats stats)
    {
		this.bulletPool = bulletPool;
		this.bulletStats = stats;
	}

	private void Despawn()
    {
		transform.SetParent(bulletPool.Container);
		rigidbody.velocity = Vector3.zero;
		bulletPool.ReturnToPool(this);
    }
}
