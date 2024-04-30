using System.Collections;
using UnityEngine;

public class MainAttack : MonoBehaviour
{
	public GameObject impactPrefab1;

	public GameObject impactPrefab2;

	public GameObject impactPrefab3;

	private GameObject enemy;

	private GameObject weapon;

	private GameObject PlayerInteractionManager;

	private GameObject player;

	private float damageMultiplier = 1f;

	private int attackTypeForm = 1;

	private int[] damageValues = new int[6];

	private void Start()
	{
		weapon = GameObject.Find("Player");
		attackTypeForm = weapon.GetComponent<PlayerWeapon>().AttackFormInfo();
		PlayerInteractionManager = GameObject.Find("PlayerHitbox");
		player = GameObject.Find("Player");
	}

	private void Update()
	{
		damageMultiplier = PlayerInteractionManager.GetComponent<PlayerInteractionManager>().GetDamageMultiplier();
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		for (int i = 0; i < 6; i++)
		{
			damageValues[i] = (int)((float)Random.Range(150000000, 400000000) * damageMultiplier);
		}
		if (collider.gameObject.CompareTag("Enemy"))
		{
			enemy = GameObject.Find(collider.gameObject.name);
			enemy.GetComponent<EnemyDamage>().GetAttack(damageValues);
			player.GetComponent<ImpactAudioManager>().PlayImpactAudio(6, 1f);
			StartCoroutine(CreateImpactMain(enemy));
		}
	}

	private IEnumerator CreateImpactMain(GameObject target)
	{
		for (int i = 0; i < 6; i++)
		{
			float num = Random.Range(-0.2f, 0.2f);
			float num2 = Random.Range(-0.2f, 0.2f);
			Vector3 position = new Vector3(target.transform.position.x + num, target.transform.position.y + num2, -3f);
			if (attackTypeForm == 1)
			{
				Object.Instantiate(impactPrefab1, position, target.transform.rotation);
			}
			else if (attackTypeForm == 2)
			{
				Object.Instantiate(impactPrefab2, position, target.transform.rotation);
			}
			else if (attackTypeForm == 3)
			{
				Object.Instantiate(impactPrefab3, position, target.transform.rotation);
			}
			yield return new WaitForSeconds(0.1f);
		}
	}
}
