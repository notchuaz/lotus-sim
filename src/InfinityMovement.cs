using UnityEngine;

public class InfinityMovement : MonoBehaviour
{
	private GameObject cam;

	private void Start()
	{
		cam = GameObject.Find("Main Camera");
	}

	private void Update()
	{
		Vector3 position = cam.transform.position;
		position.z = 1f;
		base.transform.position = position;
	}
}
