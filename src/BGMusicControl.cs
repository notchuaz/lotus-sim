using UnityEngine;

public class BGMusicControl : MonoBehaviour
{
	public AudioSource bgMusic;

	public AudioSource infinityMusic;

	private GameObject playerWeapon;

	private void Start()
	{
		playerWeapon = GameObject.Find("Player");
	}

	private void Update()
	{
		if (playerWeapon.GetComponent<PlayerWeapon>().GetInfinityStatus())
		{
			if (!infinityMusic.isPlaying)
			{
				bgMusic.Stop();
				infinityMusic.Play();
			}
		}
		else if (!bgMusic.isPlaying)
		{
			infinityMusic.Stop();
			bgMusic.Play();
		}
	}
}
