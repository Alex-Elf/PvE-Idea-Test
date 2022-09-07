using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletStats", menuName = "Scriptable object/BulletStats", order = 51)]
public class BulletStats : ScriptableObject
{
	public Bullet prefab;
	public GameObject hitEffectPrefab;

	public float bulletSpeed;
}
