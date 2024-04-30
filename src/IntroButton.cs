using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroButton : MonoBehaviour
{
	private float doubleClickTimeThreshold = 0.3f;

	private float lastClickTime;

	private void Start()
	{
	}

	private void Update()
	{
		if (Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	private void OnMouseDown()
	{
		if (Time.time - lastClickTime < doubleClickTimeThreshold)
		{
			SceneManager.LoadScene("Tutorial");
		}
		lastClickTime = Time.time;
	}
}
