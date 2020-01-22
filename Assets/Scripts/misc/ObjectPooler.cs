using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : PrefabSingleton<ObjectPooler>
{
	[SerializeField] private ObjectsToPool objectsToPool;
	private Dictionary<string, List<GameObject>> pools;
	private void Awake()
	{
		for (int i = 0; i < objectsToPool.objectsToPool.Length; i++)
		{
			var objToPool = objectsToPool.objectsToPool[i];
			Debug.Log(objToPool.prefab.GetHashCode());
		}
	}
}
