using UnityEngine;

public class PlayerOpenKeyboard : MonoBehaviour
{
	public GameObject keyboardPrefab;

	public GameObject cam;

	private bool isKeyboardOpen;

	private void Start()
	{
	}

	private void Update()
	{
		if (Input.GetKey(KeyCode.Backslash))
		{
			if (!isKeyboardOpen)
			{
				isKeyboardOpen = true;
				Vector3 position = cam.transform.position;
				position.z = -5f;
				Object.Instantiate(keyboardPrefab);
			}
		}
		else if (GameObject.FindWithTag("Keyboard") != null)
		{
			isKeyboardOpen = false;
			Object.Destroy(GameObject.FindWithTag("Keyboard"));
		}
	}
}
