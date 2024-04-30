using UnityEngine;

public class Guard : MonoBehaviour
{
	public AudioClip guardAudio;

	private bool didNotGuard;

	private bool isPlayingAudio;

	private GameObject player;

	private GameObject playerWeapon;

	private GameObject PlayerInteractionManager;

	private AudioSource audioSource;

	private void Start()
	{
		player = GameObject.Find("GuardPoint");
		playerWeapon = GameObject.Find("Player");
		PlayerInteractionManager = GameObject.Find("PlayerHitbox");
		audioSource = GetComponent<AudioSource>();
	}

	private void Update()
	{
		Vector3 position = player.transform.position;
		position.z = -2f;
		base.transform.position = position;
		if (!isPlayingAudio)
		{
			isPlayingAudio = true;
			audioSource.PlayOneShot(guardAudio);
		}
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.CompareTag("EnemyAttack"))
		{
			didNotGuard = PlayerInteractionManager.GetComponent<PlayerInteractionManager>().DidGuard();
			if (!didNotGuard)
			{
				playerWeapon.GetComponent<PlayerWeapon>().CreateGuardShield();
			}
		}
	}
}
