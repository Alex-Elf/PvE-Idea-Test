using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649
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
	private void Awake()
	{
		attackResult = Attack();
		shotDelay = new WaitForSeconds(stats.shotDelay);

		ammoMagazineCurrent = stats.ammoMagazineCurrent;
		ammoMagazineMax = stats.ammoMagazineMax;
		ammoTotal = stats.ammoTotal;
		ammoTotalMax = stats.ammoTotalMax;

		if(!ObjectPooler.Instance)
		{
			Debug.LogError("ObjectPooler not found");
		}
	}

	private IEnumerator Attack()
	{
		while(Time.timeSinceLevelLoad - lastShotTime < stats.shotDelay)
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
		var bullet = ObjectPooler.Instance.GetFromPool(stats.bullet.prefab.GetInstanceID().ToString()).GetComponent<Bullet>();
		bullet.gameObject.SetActive(true);
		bullet.transform.position = instantiatePos.position;
		bullet.rigidbody.AddForce(transform.forward * stats.bullet.bulletSpeed, ForceMode.VelocityChange);

		ammoMagazineCurrent = (ammoMagazineCurrent > 0) ? ammoMagazineCurrent-- : ammoMagazineCurrent;
		ammoTotal = (ammoTotal > 0) ? ammoTotal-- : ammoTotal;
		aSource.PlayOneShot(stats.attackSound);
		lastShotTime = Time.timeSinceLevelLoad;
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
