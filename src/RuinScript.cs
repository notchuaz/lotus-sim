using System.Collections;
using UnityEngine;

public class RuinScript : MonoBehaviour
{
	public GameObject ruinImpactPrefab;

	private GameObject enemy;

	private GameObject PlayerInteractionManager;

	private GameObject player;

	private Coroutine damageBefore;

	private Coroutine damageAfter;

	private float damageMultiplier = 1f;

	private int[] damageValues = new int[5];

	private int[] damageValuesAfter = new int[8];

	private int damageBeforeIter;

	private int damageAfterIter;

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
		if (collider.gameObject.CompareTag("Enemy"))
		{
			enemy = GameObject.Find(collider.gameObject.name);
			damageBefore = StartCoroutine(GiveDamage());
		}
	}

	private void OnTriggerExit2D(Collider2D collider)
	{
		if (collider.gameObject.CompareTag("Enemy"))
		{
			if (damageBefore != null)
			{
				StopCoroutine(damageBefore);
			}
			if (damageAfter != null)
			{
				StopCoroutine(damageAfter);
			}
		}
	}

	private IEnumerator GiveDamage()
	{
		while (damageBeforeIter < 19)
		{
			for (int i = 0; i < 5; i++)
			{
				damageValues[i] = (int)((float)Random.Range(200000000, 300000000) * damageMultiplier);
				StartCoroutine(CreateImpactRuin(5));
				player.GetComponent<ImpactAudioManager>().PlayImpactAudio(5, 0.3f);
			}
			if (damageBeforeIter == 18)
			{
				damageAfter = StartCoroutine(GiveDamageAfter());
			}
			enemy.GetComponent<EnemyDamage>().ContinuousAttack(damageValues);
			damageBeforeIter++;
			yield return new WaitForSeconds(0.08f);
		}
	}

	private IEnumerator GiveDamageAfter()
	{
		while (damageAfterIter < 8)
		{
			for (int i = 0; i < 8; i++)
			{
				damageValuesAfter[i] = (int)((float)Random.Range(400000000, 600000000) * damageMultiplier);
				player.GetComponent<ImpactAudioManager>().PlayImpactAudio(8, 0.11f);
				StartCoroutine(CreateImpactRuin(8));
			}
			enemy.GetComponent<EnemyDamage>().ContinuousAttack(damageValuesAfter);
			damageAfterIter++;
			yield return new WaitForSeconds(0.13f);
		}
	}

	private IEnumerator CreateImpactRuin(int numImpacts)
	{
		for (int i = 0; i < numImpacts; i++)
		{
			float num = Random.Range(-0.2f, 0.2f);
			float num2 = Random.Range(-0.2f, 0.2f);
			Object.Instantiate(position: new Vector3(enemy.transform.position.x + num, enemy.transform.position.y + num2, -3f), original: ruinImpactPrefab, rotation: enemy.transform.rotation);
			yield return new WaitForSeconds(0.1f);
		}
	}
}
