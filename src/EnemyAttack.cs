using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAttack : MonoBehaviour
{
	private float damage;

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
			if (SceneManager.GetActiveScene().name.Equals("Tutorial"))
			{
				playerHitbox = GameObject.Find(collider.gameObject.name);
				playerHitbox.GetComponent<PlayerInteractionManager>().GetAttack(damage);
			}
			if (SceneManager.GetActiveScene().name.Equals("P2"))
			{
				playerHitbox = GameObject.Find(collider.gameObject.name);
				playerHitbox.GetComponent<PlayerInteractionManager>().GetAttack(0.1f);
			}
		}
	}
}
