using UnityEngine;

public class BindInit : MonoBehaviour
{
	private GameObject player;

	private void Start()
	{
		player = GameObject.Find("BindInitPoint");
	}

	private void Update()
	{
		Vector3 position = player.transform.position;
		position.z = -2f;
		base.transform.position = position;
	}
}
