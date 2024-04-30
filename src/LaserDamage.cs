using UnityEngine;

public class LaserDamage : MonoBehaviour
{
	private GameObject playerHitbox;

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.CompareTag("PlayerHitbox"))
		{
			playerHitbox = GameObject.Find(collider.gameObject.name);
			playerHitbox.GetComponent<PlayerInteractionManager>().GetAttack(1f);
		}
	}

	private void OnTriggerStay2D(Collider2D collider)
	{
		if (collider.gameObject.CompareTag("PlayerHitbox"))
		{
			playerHitbox.GetComponent<PlayerInteractionManager>().GetAttack(1f);
		}
	}
}
