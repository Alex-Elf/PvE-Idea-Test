using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToPool: MonoBehaviour
{
	[SerializeField] private string id;

	public void ReturnToPool()
	{
		ObjectPooler.Instance.ReturnToPool(id, this);
	}
	public void SetId(string id)
	{
		this.id = id;
	}
}

