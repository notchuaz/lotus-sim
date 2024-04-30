using TMPro;
using UnityEngine;

public class CDDisplayScript : MonoBehaviour
{
	public GameObject cdTextPrefab;

	public GameObject cam;

	public Transform featherFloatPoint;

	public Transform rushPoint;

	public Transform plummetPoint;

	public Transform reignPoint;

	public Transform ruinPoint;

	public Transform infinityPoint;

	public Transform guardPoint;

	public Transform bindPoint;

	public Transform buffPoint;

	public Transform potionPoint;

	private TextMeshPro cdText;

	private GameObject[] cdArray;

	private void Start()
	{
		cdText = cdTextPrefab.transform.GetChild(0).GetComponent<TextMeshPro>();
		cdArray = new GameObject[10];
	}

	private void Update()
	{
		Vector3 position = base.transform.position;
		position.x = cam.transform.position.x + 5.56f;
		position.y = cam.transform.position.y - 3.41f;
		position.z = -5f;
		base.transform.position = position;
	}

	public void CreateCooldown(string skillType, float cd)
	{
		if (skillType.Equals("tp"))
		{
			Vector3 position = featherFloatPoint.position;
			position.z = -6f;
			cdArray[0] = Object.Instantiate(cdTextPrefab, position, featherFloatPoint.rotation);
			cdArray[0].GetComponent<cdCooldownScript>().GetCooldown((int)cd, featherFloatPoint);
		}
		if (skillType.Equals("rush"))
		{
			Vector3 position2 = rushPoint.position;
			position2.z = -6f;
			cdArray[1] = Object.Instantiate(cdTextPrefab, position2, rushPoint.rotation);
			cdArray[1].GetComponent<cdCooldownScript>().GetCooldown((int)cd, rushPoint);
		}
		if (skillType.Equals("plummet"))
		{
			Vector3 position3 = plummetPoint.position;
			position3.z = -6f;
			cdArray[2] = Object.Instantiate(cdTextPrefab, position3, plummetPoint.rotation);
			cdArray[2].GetComponent<cdCooldownScript>().GetCooldown((int)cd, plummetPoint);
		}
		if (skillType.Equals("reign"))
		{
			Vector3 position4 = reignPoint.position;
			position4.z = -6f;
			cdArray[3] = Object.Instantiate(cdTextPrefab, position4, reignPoint.rotation);
			cdArray[3].GetComponent<cdCooldownScript>().GetCooldown((int)cd, reignPoint);
		}
		if (skillType.Equals("ruin"))
		{
			Vector3 position5 = ruinPoint.position;
			position5.z = -6f;
			cdArray[4] = Object.Instantiate(cdTextPrefab, position5, ruinPoint.rotation);
			cdArray[4].GetComponent<cdCooldownScript>().GetCooldown((int)cd, ruinPoint);
		}
		if (skillType.Equals("infinity"))
		{
			Vector3 position6 = infinityPoint.position;
			position6.z = -6f;
			cdArray[5] = Object.Instantiate(cdTextPrefab, position6, infinityPoint.rotation);
			cdArray[5].GetComponent<cdCooldownScript>().GetCooldown((int)cd, infinityPoint);
		}
		if (skillType.Equals("guard"))
		{
			Vector3 position7 = guardPoint.position;
			position7.z = -6f;
			cdArray[6] = Object.Instantiate(cdTextPrefab, position7, guardPoint.rotation);
			cdArray[6].GetComponent<cdCooldownScript>().GetCooldown((int)cd, guardPoint);
		}
		if (skillType.Equals("bind"))
		{
			Vector3 position8 = bindPoint.position;
			position8.z = -6f;
			cdArray[7] = Object.Instantiate(cdTextPrefab, position8, bindPoint.rotation);
			cdArray[7].GetComponent<cdCooldownScript>().GetCooldown((int)cd, bindPoint);
		}
		if (skillType.Equals("buff"))
		{
			Vector3 position9 = buffPoint.position;
			position9.z = -6f;
			cdArray[8] = Object.Instantiate(cdTextPrefab, position9, buffPoint.rotation);
			cdArray[8].GetComponent<cdCooldownScript>().GetCooldown((int)cd, buffPoint);
		}
		if (skillType.Equals("potion"))
		{
			Vector3 position10 = potionPoint.position;
			position10.z = -6f;
			cdArray[8] = Object.Instantiate(cdTextPrefab, position10, potionPoint.rotation);
			cdArray[8].GetComponent<cdCooldownScript>().GetCooldown((int)cd, potionPoint);
		}
	}
}
