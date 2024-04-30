using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBarScript : MonoBehaviour
{
	private Slider healthSlider;

	private long health;

	private void Start()
	{
		healthSlider = GetComponent<Slider>();
	}

	public void SetHealth(long health)
	{
		if (health != this.health)
		{
			this.health = health;
			StartCoroutine(ChangeHealth(health));
		}
	}

	private IEnumerator ChangeHealth(long health)
	{
		if (healthSlider.value > (float)health)
		{
			while (healthSlider.value > (float)health)
			{
				healthSlider.value -= 10000000f;
				if (healthSlider.value < (float)health)
				{
					healthSlider.value = health;
				}
				yield return null;
			}
		}
		else
		{
			if (!(healthSlider.value < (float)health))
			{
				yield break;
			}
			while (healthSlider.value < (float)health)
			{
				healthSlider.value += 100000f;
				if (healthSlider.value > (float)health)
				{
					healthSlider.value = health;
				}
				yield return null;
			}
		}
	}
}
