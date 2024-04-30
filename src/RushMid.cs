using System.Collections;
using UnityEngine;

public class RushMid : MonoBehaviour
{
	public GameObject rushImpact;

	private GameObject player;

	private GameObject ImpactAudio;

	private GameObject PlayerInteractionManager;

	private GameObject enemy;

	private float damageMultiplier = 1f;

	private int[] damageValues = new int[3];

	private void Start()
	{
		player = GameObject.Find("RushMidPoint");
		ImpactAudio = GameObject.Find("Player");
		PlayerInteractionManager = GameObject.Find("PlayerHitbox");
	}

	private void Update()
	{
		Vector3 position = player.transform.position;
		position.z = -2f;
		base.transform.position = position;
		damageMultiplier = PlayerInteractionManager.GetComponent<PlayerInteractionManager>().GetDamageMultiplier();
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		for (int i = 0; i < 3; i++)
		{
			damageValues[i] = (int)((float)Random.Range(100000000, 250000000) * damageMultiplier);
		}
		if (collider.gameObject.CompareTag("Enemy"))
		{
			enemy = GameObject.Find(collider.gameObject.name);
			enemy.GetComponent<EnemyDamage>().GetAttack(damageValues);
			StartCoroutine(CreateImpactRush(enemy));
			ImpactAudio.GetComponent<ImpactAudioManager>().PlayImpactAudio(3);
		}
	}

	private IEnumerator CreateImpactRush(GameObject target)
	{
		for (int i = 0; i < 3; i++)
		{
			float num = Random.Range(-0.2f, 0.2f);
			float num2 = Random.Range(-0.2f, 0.2f);
			Object.Instantiate(position: new Vector3(target.transform.position.x + num, target.transform.position.y + num2, -3f), original: rushImpact, rotation: target.transform.rotation);
			yield return new WaitForSeconds(0.1f);
		}
	}
}
