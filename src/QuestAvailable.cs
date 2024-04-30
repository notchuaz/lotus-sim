using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestAvailable : MonoBehaviour
{
	private GameObject orchid;

	private float doubleClickTimeThreshold = 0.3f;

	private float lastClickTime;

	private void Start()
	{
		orchid = GameObject.FindWithTag("Orchid");
	}

	private void Update()
	{
	}

	private void OnMouseDown()
	{
		if (Time.time - lastClickTime < doubleClickTimeThreshold)
		{
			if (!SceneManager.GetActiveScene().name.Equals("End"))
			{
				orchid.GetComponent<Orchid>().GoNextStep();
			}
			else
			{
				orchid.GetComponent<Orchid>().End();
			}
		}
		lastClickTime = Time.time;
	}
}
