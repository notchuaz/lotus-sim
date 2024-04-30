using UnityEngine;

public class ReignEffect : MonoBehaviour
{
	private GameObject player;

	private void Start()
	{
		player = GameObject.Find("ReignEffectPoint");
	}

	private void Update()
	{
		Vector3 position = player.transform.position;
		base.transform.position = position;
	}
}
