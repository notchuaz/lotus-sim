using UnityEngine;

public class EnemyGetRange : MonoBehaviour
{
	private bool inRange;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public bool GetInRange()
	{
		return inRange;
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.name.Equals("PlayerHitbox"))
		{
			inRange = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collider)
	{
		if (collider.gameObject.name.Equals("PlayerHitbox"))
		{
			inRange = false;
		}
	}
}
