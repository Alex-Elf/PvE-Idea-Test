using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using LocalObjectPooler;

public class SoundManager : MonoBehaviour
{
	[SerializeField] private SoundManagerEffectSource poolObjectPrefab;

	private ComponentObjectPooler<SoundManagerEffectSource> sourcePool;

    private void Start()
	{
		sourcePool = new(poolObjectPrefab, transform);
	}

	public SoundManagerEffectSource PlayEffect(AudioClip audioClip, Transform sourcePosition, float volume = 1)
	{
		var availableSource = sourcePool.GetFreeObject();

		availableSource.gameObject.SetActive(true);

		availableSource.PlayWholeClip(audioClip, volume);

		availableSource.transform.parent = sourcePosition;
		availableSource.transform.position = Vector3.zero;
        availableSource.OnPlayEnded += AvailableSource_OnPlayEnded;
		return availableSource;
	}

    private void AvailableSource_OnPlayEnded(SoundManagerEffectSource obj)
    {
		sourcePool.ReturnToPool(obj);
		obj.transform.parent = sourcePool.Container;

	}
}
