using UnityEngine;

public class WrathScript : MonoBehaviour
{
	private GameObject player;

	private void Start()
	{
		player = GameObject.Find("WrathPoint");
	}

	private void Update()
	{
		Vector3 position = player.transform.position;
		position.z = -2f;
		base.transform.position = position;
	}
}
