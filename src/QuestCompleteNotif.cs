using UnityEngine;

public class QuestCompleteNotif : MonoBehaviour
{
	private GameObject questNotifPoint;

	private void Start()
	{
		questNotifPoint = GameObject.Find("QuestNotifPoint");
	}

	private void Update()
	{
		Vector3 position = questNotifPoint.transform.position;
		position.z = -2f;
		base.transform.position = position;
	}
}
