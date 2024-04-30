using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
	private Slider healthSlider;

	private Transform text;

	private void Start()
	{
		healthSlider = GetComponent<Slider>();
		text = base.transform.GetChild(4);
		healthSlider.value = 500000f;
	}

	public void SetHealth(int health)
	{
		healthSlider.value = health;
		text.GetComponent<TextMeshProUGUI>().SetText(health + "/500000");
	}
}
