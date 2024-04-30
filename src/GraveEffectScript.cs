using UnityEngine;

public class GraveEffectScript : MonoBehaviour
{
	private GameObject enemy;

	private void Start()
	{
		enemy = GameObject.Find("GraveEffectPoint");
	}

	private void Update()
	{
		Vector3 position = enemy.transform.position;
		position.z = -2f;
		float num = (0f - Mathf.Sin(Time.time * 5f)) * 0.03f;
		position.y += num;
		base.transform.position = position;
	}
}
