using System.Collections;
using UnityEngine;

public class ReignSummon : MonoBehaviour
{
	public GameObject reignSummonImpactPrefab;

	private GameObject enemy;

	private GameObject PlayerInteractionManager;

	private float damageMultiplier = 1f;

	private int[] damageValues = new int[4];

	private Coroutine giveDamageCoroutine;

	private void Start()
	{
		PlayerInteractionManager = GameObject.Find("PlayerHitbox");
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
			giveDamageCoroutine = StartCoroutine(GiveDamage());
		}
	}

	private void OnTriggerExit2D(Collider2D collider)
	{
		if (collider.gameObject.CompareTag("Enemy"))
		{
			StopCoroutine(giveDamageCoroutine);
		}
	}

	private IEnumerator GiveDamage()
	{
		while (true)
		{
			StartCoroutine(CreateImpactReign());
			for (int i = 0; i < 4; i++)
			{
				damageValues[i] = (int)((float)Random.Range(100000000, 250000000) * damageMultiplier);
			}
			enemy.GetComponent<EnemyDamage>().ContinuousAttack(damageValues);
			yield return new WaitForSeconds(0.5f);
		}
	}

	private IEnumerator CreateImpactReign()
	{
		for (int i = 0; i < 4; i++)
		{
			float num = Random.Range(-0.2f, 0.2f);
			float num2 = Random.Range(-0.2f, 0.2f);
			Object.Instantiate(position: new Vector3(enemy.transform.position.x + num, enemy.transform.position.y + num2, -3f), original: reignSummonImpactPrefab, rotation: enemy.transform.rotation);
			yield return new WaitForSeconds(0.1f);
		}
	}
}
