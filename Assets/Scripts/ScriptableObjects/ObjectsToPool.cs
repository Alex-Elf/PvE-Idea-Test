using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectsToPool", menuName = "Scriptable object/ObjectsToPool", order = 0)]
public class ObjectsToPool : ScriptableObject
{
	[System.Serializable]
	public class PoolObj
	{
		public string name;
		public BackToPool prefab;
		public int maxAmount;
		public bool canExpand;
	}
	public PoolObj[] objectsToPool;
}
