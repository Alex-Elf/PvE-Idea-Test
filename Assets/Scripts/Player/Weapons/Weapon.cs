using LocalObjectPooler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public short ammoMagazineCurrent;
	public short ammoMagazineMax;
	public short ammoTotal;
	public short ammoTotalMax;

	[SerializeField] private WeaponStats stats;
	[SerializeField] private Transform instantiatePos;
	[SerializeField] private AudioSource aSource;

	private float lastShotTime;

	private IEnumerator attackResult;
	private WaitForSeconds shotDelay;

	private ComponentObjectPooler<Bullet> bulletPool;

	private void Awake()
	{
		var bulletsPoolContainer = new GameObject("bullets pool");
		bulletsPoolContainer.transform.SetParent(transform);
		bulletsPoolContainer.SetActive(false);
		bulletPool = new(stats.bullet.prefab, bulletsPoolContainer.transform);

		attackResult = Attack();
		shotDelay = new WaitForSeconds(stats.shotDelay);

		ammoMagazineCurrent = stats.ammoMagazineCurrent;
		ammoMagazineMax = stats.ammoMagazineMax;
		ammoTotal = stats.ammoTotal;
		ammoTotalMax = stats.ammoTotalMax;

	}

	private IEnumerator Attack()
	{
		while(Time.time - lastShotTime < stats.shotDelay)
		{
			yield return null;
		}

		do
		{
			AttackOnce();
			yield return shotDelay;
		}
		while (ammoMagazineCurrent != 0 && stats.isAuto);
		
	}

	private void AttackOnce()
	{
		var bullet = bulletPool.GetFreeObject();
		bullet.Setup(bulletPool, stats.bullet);
		bullet.transform.SetParent(null);
		bullet.gameObject.SetActive(true);
		bullet.transform.position = instantiatePos.position;
		bullet.rigidbody.AddForce(transform.forward * stats.bullet.bulletSpeed, ForceMode.VelocityChange);

		ammoMagazineCurrent = (ammoMagazineCurrent > 0) ? ammoMagazineCurrent-- : ammoMagazineCurrent;
		ammoTotal = (ammoTotal > 0) ? ammoTotal-- : ammoTotal;
		aSource.PlayOneShot(stats.attackSound);
		lastShotTime = Time.time;
	}

	public void StartAttacking()
	{
		attackResult = Attack();
		StartCoroutine(attackResult);
	}
	
	public void StopAttacking()
	{
		StopCoroutine(attackResult);
	}
}
