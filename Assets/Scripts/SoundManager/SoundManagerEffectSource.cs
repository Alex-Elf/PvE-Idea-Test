using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManagerEffectSource : MonoBehaviour
{
	public event System.Action<SoundManagerEffectSource> OnPlayEnded;

	[SerializeField] private AudioSource source;

	private Coroutine playingRoutine;

	public void PlayWholeClip(AudioClip clip, float volume)
	{
		if(playingRoutine == null)
		{
			playingRoutine = StartCoroutine(PlayingClip(clip, volume));
		}
		else
		{
			Debug.LogError("Cant play clip on already busy source");
		}
	}

	private IEnumerator PlayingClip(AudioClip clip, float volume)
	{
		source.clip = clip;
		source.volume = volume;
		source.Play();

		yield return new WaitForSeconds(clip.length);
		
		PlayEnded();
	}

	public void StopPlaying()
	{
		if (playingRoutine != null) StopCoroutine(playingRoutine);
		PlayEnded();
	}

	private void PlayEnded()
    {
		source.Stop();
		gameObject.SetActive(false);
		OnPlayEnded?.Invoke(this);
		OnPlayEnded = null;
		playingRoutine = null;
	}
}
