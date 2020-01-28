using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ObjectsToPool;

public class ObjectPooler : PrefabSingleton<ObjectPooler>
{
	[SerializeField] private string pathToScriptableObj = "objectsToPool/ObjectsToPool";
	[SerializeField] private ObjectsToPool objectsToPool;
	private Dictionary<string, List<BackToPool>> pools;
	private Dictionary<string, PoolObj> idToPoolObj;
	private Dictionary<string, Transform> idToHolders;
	private void Awake()
	{
		CheckObjectsToPoolParam();

		pools = new Dictionary<string, List<BackToPool>>();
		idToPoolObj = new Dictionary<string, PoolObj>();
		idToHolders = new Dictionary<string, Transform>();
		for (int i = 0; i < objectsToPool.objectsToPool.Length; i++)
		{
			var objToPool = objectsToPool.objectsToPool[i];
			var id = objToPool.prefab.gameObject.GetInstanceID().ToString();
			var l = new List<BackToPool>();
			var prefabsParent = new GameObject(objToPool.prefab.name+" prefabs holder").transform;
			prefabsParent.parent = transform;
			for (int j = 0; j < objToPool.maxAmount; j++)
			{
				var inst = InstantiatePrefab(id, objToPool.prefab, prefabsParent);
				l.Add(inst);
			}
			pools.Add(id, l);
			idToPoolObj.Add(id, objToPool);
			idToHolders.Add(id, prefabsParent);
		}
	}
	private void CheckObjectsToPoolParam()
	{
		if(objectsToPool == null)
		{
			objectsToPool = Resources.Load(pathToScriptableObj) as ObjectsToPool;
		}
	}

	public GameObject GetFromPool(string id)
	{
		if (pools.ContainsKey(id))
		{
			if (pools[id].Count > 0)
			{
				var backToPool = pools[id][0];
				pools[id].RemoveAt(0);
				return backToPool.gameObject;
			}
			else
			{
				if (idToPoolObj[id].canExpand)
				{
					InstantiatePrefab(id, idToPoolObj[id].prefab, idToHolders[id]);
				}
			}
		}
		return null;
	}
	private BackToPool InstantiatePrefab(string id, BackToPool prefab, Transform parent)
	{
		var inst = Instantiate(prefab.gameObject, parent);
		inst.SetActive(false);
		var backToPool = inst.GetComponent<BackToPool>();
		backToPool.SetId(id);
		return backToPool;
	}

	public void ReturnToPool(string id, BackToPool backToPool)
	{
		var l = pools[id];
		backToPool.gameObject.SetActive(false);
		l.Add(backToPool);
	}
}
