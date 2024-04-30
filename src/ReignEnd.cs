using System.Collections;
using UnityEngine;

public class ReignEnd : MonoBehaviour
{
	public GameObject rushEndImpactPrefab;

	private GameObject enemy;

	private GameObject PlayerInteractionManager;

	private float damageMultiplier = 1f;

	private int[] damageValues = new int[12];

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
		for (int i = 0; i < 12; i++)
		{
			damageValues[i] = (int)((float)Random.Range(100000000, 300000000) * damageMultiplier);
		}
		if (collider.gameObject.CompareTag("Enemy"))
		{
			enemy = GameObject.Find(collider.gameObject.name);
			enemy.GetComponent<EnemyDamage>().GetAttack(damageValues);
			StartCoroutine(CreateImpactReign(enemy));
		}
	}

	private IEnumerator CreateImpactReign(GameObject target)
	{
		for (int i = 0; i < 12; i++)
		{
			float num = Random.Range(-0.2f, 0.2f);
			float num2 = Random.Range(-0.2f, 0.2f);
			Object.Instantiate(position: new Vector3(target.transform.position.x + num, target.transform.position.y + num2, -3f), original: rushEndImpactPrefab, rotation: target.transform.rotation);
			yield return new WaitForSeconds(0.1f);
		}
	}
}
