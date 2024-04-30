using UnityEngine;

public class QuestComplete : MonoBehaviour
{
	private GameObject orchid;

	private float doubleClickTimeThreshold = 0.3f;

	private float lastClickTime;

	private void Start()
	{
		orchid = GameObject.Find("Orchid");
	}

	private void OnMouseDown()
	{
		if (Time.time - lastClickTime < doubleClickTimeThreshold)
		{
			orchid.GetComponent<Orchid>().GoNextStep();
		}
		lastClickTime = Time.time;
	}
}
