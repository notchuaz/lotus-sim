using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMovement : MonoBehaviour
{
	private const float MIN_X = -2.54f;

	private const float MAX_X = 2f;

	private const float OFFSET_Y = -1.87f;

	private const float OFFSET_Z = -10f;

	public Transform target;

	private float smoothFactor = 7f;

	private void FixedUpdate()
	{
		if (SceneManager.GetActiveScene().name.Equals("Tutorial") || SceneManager.GetActiveScene().name.Equals("Respawn Point") || SceneManager.GetActiveScene().name.Equals("End"))
		{
			Vector3 position = Vector3.Lerp(b: new Vector3(target.position.x, -1.87f, -10f), a: base.transform.position, t: smoothFactor * Time.fixedDeltaTime);
			base.transform.position = position;
			if (base.transform.position.x <= -2.54f)
			{
				Vector3 position2 = new Vector3(-2.54f, base.transform.position.y, base.transform.position.z);
				base.transform.position = position2;
			}
			if (base.transform.position.x >= 2f)
			{
				Vector3 position3 = new Vector3(2f, base.transform.position.y, base.transform.position.z);
				base.transform.position = position3;
			}
		}
	}
}
