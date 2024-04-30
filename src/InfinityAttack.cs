using System.Collections;
using UnityEngine;

public class InfinityAttack : MonoBehaviour
{
	private GameObject enemy;

	private GameObject PlayerInteractionManager;

	private float damageMultiplier = 1f;

	private int[] damageValues = new int[2];

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
			enemy = collider.gameObject;
			StartCoroutine(GiveDamage());
		}
	}

	private IEnumerator GiveDamage()
	{
		while (true)
		{
			for (int i = 0; i < 2; i++)
			{
				damageValues[i] = (int)((float)Random.Range(300000000, 700000000) * damageMultiplier);
				enemy.GetComponent<EnemyDamage>().ContinuousAttack(damageValues);
				yield return new WaitForSeconds(0.2f);
			}
		}
	}
}
