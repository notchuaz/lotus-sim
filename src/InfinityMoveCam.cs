using UnityEngine;

public class InfinityMoveCam : MonoBehaviour
{
	private GameObject cam;

	private void Start()
	{
		cam = GameObject.Find("Main Camera");
	}

	private void Update()
	{
		Vector3 position = cam.transform.position;
		position.z = -4f;
		base.transform.position = position;
	}
}
