using UnityEngine;

public class LaserRotation : MonoBehaviour
{
	private const float ROTATION_CD = 30f;

	private float rotation_speed = 11f;

	private float tempSpeed = -11f;

	private float changeRotation;

	private void Start()
	{
	}

	private void Update()
	{
		if (GameObject.FindGameObjectWithTag("BindEnd") != null)
		{
			base.transform.Rotate(new Vector3(0f, 0f, 0f) * Time.deltaTime);
		}
		else
		{
			base.transform.Rotate(new Vector3(0f, 0f, rotation_speed) * Time.deltaTime);
		}
		if (Time.time > changeRotation + 30f)
		{
			changeRotation = Time.time;
			rotation_speed *= -1f;
		}
	}
}
