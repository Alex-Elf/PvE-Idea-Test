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

	private float timeSinceLastShot;
	private IEnumerator Shoot()
	{
		while(ammoMagazineCurrent > 0)
		{

		}
		yield return null;
	}

	private void ShootOnce()
	{
		var bullet = Instantiate(stats.bullet.prefab);



	}
}
