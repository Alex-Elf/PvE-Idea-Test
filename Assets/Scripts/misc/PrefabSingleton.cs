using UnityEngine;

public class PrefabSingleton<T> : MonoBehaviour where T : PrefabSingleton<T>
{
	private const string assetPath = "Singletons/";
	private static T instance = null;
	public static T Instance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<T>();
				if (instance == null)
				{
					var prefab = Resources.Load<T>(assetPath + typeof(T).Name);
					if (prefab == null)
						Debug.LogError("singleton prefab missing: " + assetPath + typeof(T).Name);
					else
						instance = Instantiate(prefab);
				}
			}
			return instance;
		}
	}
	[SerializeField] private bool isPersistent;
	private void Awake()
	{
		if(isPersistent)
			DontDestroyOnLoad(instance.gameObject);
	}
}