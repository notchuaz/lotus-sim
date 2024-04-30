using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
	private float doubleClickTimeThreshold = 0.3f;

	private float lastClickTime;

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnMouseDown()
	{
		if (Time.time - lastClickTime < doubleClickTimeThreshold)
		{
			if (SceneManager.GetActiveScene().name.Equals("Tutorial") || SceneManager.GetActiveScene().name.Equals("Respawn Point") || SceneManager.GetActiveScene().name.Equals("End"))
			{
				SceneManager.LoadScene("P1");
			}
			if (SceneManager.GetActiveScene().name.Equals("P1"))
			{
				SceneManager.LoadScene("Respawn Point");
			}
		}
		lastClickTime = Time.time;
	}
}
