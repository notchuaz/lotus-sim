using UnityEngine;

public class StunFollow : MonoBehaviour
{
	private GameObject stunPoint;

	private void Start()
	{
		stunPoint = GameObject.Find("StunPoint");
	}

	private void Update()
	{
		Vector3 position = stunPoint.transform.position;
		position.z = -2f;
		base.transform.position = position;
	}
}
