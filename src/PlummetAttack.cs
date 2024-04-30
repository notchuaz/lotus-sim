using System.Collections;
using UnityEngine;

public class PlummetAttack : MonoBehaviour
{
	public GameObject plummetImpactPrefab;

	private GameObject enemy;

	private GameObject PlayerInteractionManager;

	private GameObject player;

	private float damageMultiplier = 1f;

	private int[] damageValues = new int[8];

	private void Start()
	{
		PlayerInteractionManager = GameObject.Find("PlayerHitbox");
		player = GameObject.Find("Player");
	}

	private void Update()
	{
		damageMultiplier = PlayerInteractionManager.GetComponent<PlayerInteractionManager>().GetDamageMultiplier();
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		for (int i = 0; i < 8; i++)
		{
			damageValues[i] = (int)((float)Random.Range(200000000, 533000000) * damageMultiplier);
		}
		if (collider.gameObject.CompareTag("Enemy"))
		{
			enemy = GameObject.Find(collider.gameObject.name);
			enemy.GetComponent<EnemyDamage>().GetAttack(damageValues);
			player.GetComponent<ImpactAudioManager>().PlayImpactAudio(8);
			StartCoroutine(CreateImpactPlummet(enemy));
		}
		player.GetComponent<ImpactAudioManager>().PlayImpactAudio(8);
	}

	private IEnumerator CreateImpactPlummet(GameObject target)
	{
		for (int i = 0; i < 8; i++)
		{
			float num = Random.Range(-0.2f, 0.2f);
			float num2 = Random.Range(-0.2f, 0.2f);
			Object.Instantiate(position: new Vector3(target.transform.position.x + num, target.transform.position.y + num2, -3f), original: plummetImpactPrefab, rotation: target.transform.rotation);
			yield return new WaitForSeconds(0.1f);
		}
	}
}
