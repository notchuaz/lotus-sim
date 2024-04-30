using UnityEngine;

public class InfinityMovementStart : MonoBehaviour
{
	private const float INFINITY_SPAWN_TIME = 0.58f;

	private GameObject cam;

	private Material objMaterial;

	private float spawnTime;

	private void Start()
	{
		cam = GameObject.Find("Main Camera");
		objMaterial = GetComponent<Renderer>().material;
		Color color = objMaterial.color;
		float a = 0f;
		Color color2 = new Color(color.r, color.g, color.b, a);
		objMaterial.color = color2;
		spawnTime = Time.time;
	}

	private void Update()
	{
		Vector3 position = cam.transform.position;
		position.z = 1f;
		base.transform.position = position;
		if (Time.time > spawnTime + 0.58f)
		{
			Color color = objMaterial.color;
			float a = 1f;
			Color color2 = new Color(color.r, color.g, color.b, a);
			objMaterial.color = color2;
		}
	}
}
