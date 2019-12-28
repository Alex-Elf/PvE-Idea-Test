using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStats", menuName = "Scriptable object/Weapon stats", order = 51)]
public class WeaponStats : ScriptableObject
{
	public float baseDamage;
	public float spread;
	public float shotDelay;
	public bool isAuto;

	public short ammoMagazineCurrent;
	public short ammoMagazineMax;
	public short ammoTotal;
	public short ammoTotalMax;

	public AudioClip attackSound;
	public GameObject prefab;
	public BulletStats bullet;
}
