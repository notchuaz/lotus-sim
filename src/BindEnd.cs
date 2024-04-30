using UnityEngine;

public class BindEnd : MonoBehaviour
{
	private GameObject enemy;

	private void Start()
	{
		enemy = GameObject.Find("BindSummonPoint");
	}

	private void Update()
	{
		Vector3 position = enemy.transform.position;
		position.z = -0.8f;
		base.transform.position = position;
	}
}
