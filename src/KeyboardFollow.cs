using UnityEngine;

public class KeyboardFollow : MonoBehaviour
{
	private GameObject cam;

	private void Start()
	{
		cam = GameObject.Find("Main Camera");
	}

	private void Update()
	{
		Vector3 position = cam.transform.position;
		position.z = -5f;
		base.transform.position = position;
	}
}
