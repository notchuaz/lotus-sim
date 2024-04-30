using System.Collections;
using UnityEngine;

public class ImpactAudioManager : MonoBehaviour
{
	public AudioClip impact;

	private AudioSource audioSource;

	private int numAttacks;

	private float volume = 0.5f;

	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}

	private void Update()
	{
	}

	public void PlayImpactAudio(int numAttacks, float volume = 0.5f)
	{
		this.numAttacks = numAttacks;
		this.volume = volume;
		StartCoroutine(PlayImpact());
	}

	private IEnumerator PlayImpact()
	{
		for (int i = 0; i < numAttacks; i++)
		{
			audioSource.PlayOneShot(impact, volume);
			yield return new WaitForSeconds(0.1f);
		}
	}
}
