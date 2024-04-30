using System.Collections;
using TMPro;
using UnityEngine;

public class cdCooldownScript : MonoBehaviour
{
	private string cooldownText;

	private int cooldown;

	private float beginSkill;

	private TextMeshPro text;

	private Transform skillPoint;

	private void Start()
	{
		beginSkill = Time.time;
	}

	private void Update()
	{
		Vector3 position = skillPoint.position;
		position.z = -6f;
		base.transform.position = position;
		if (Time.time > beginSkill + (float)cooldown)
		{
			Object.Destroy(base.gameObject);
		}
	}

	public void GetCooldown(int cooldown, Transform skillPoint)
	{
		this.cooldown = cooldown;
		this.skillPoint = skillPoint;
		StartCoroutine(UpdateTimer(cooldown));
	}

	private IEnumerator UpdateTimer(int cd)
	{
		text = GetComponentInChildren<TextMeshPro>();
		while (cd > 0)
		{
			text.SetText(cd.ToString());
			cd--;
			yield return new WaitForSeconds(1f);
		}
	}
}
