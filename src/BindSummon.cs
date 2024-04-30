using UnityEngine;

public class BindSummon : MonoBehaviour
{
	private GameObject enemy;

	private GameObject PlayerInteractionManager;

	private float damageMultiplier = 1f;

	private int[] damageValues = new int[10];

	private void Start()
	{
		enemy = GameObject.Find("BindSummonPoint");
		PlayerInteractionManager = GameObject.Find("PlayerHitbox");
	}

	private void Update()
	{
		Vector3 position = enemy.transform.position;
		position.z = -0.8f;
		base.transform.position = position;
		damageMultiplier = PlayerInteractionManager.GetComponent<PlayerInteractionManager>().GetDamageMultiplier();
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		for (int i = 0; i < 10; i++)
		{
			damageValues[i] = (int)((float)Random.Range(300000000, 500000000) * damageMultiplier);
		}
		if (collider.gameObject.CompareTag("Enemy"))
		{
			enemy = GameObject.Find(collider.gameObject.name);
			enemy.GetComponent<EnemyDamage>().GetAttack(damageValues);
		}
	}
}
