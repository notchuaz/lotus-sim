using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyDamage : MonoBehaviour
{
	public Transform deathPoint;

	public GameObject damageSkinPrefab;

	public GameObject healthBar;

	public GameObject deathPrefab;

	private GameObject mainAttack;

	private TextMeshPro damageLine;

	private int attackForm;

	private int[] damageValues;

	private long health;

	private bool death;

	private bool setDeathTimer;

	private float deathTimer;

	private void Start()
	{
		damageLine = damageSkinPrefab.transform.GetChild(0).GetComponent<TextMeshPro>();
		if (SceneManager.GetActiveScene().name.Equals("P1"))
		{
			health = 3000000000000L;
		}
		if (SceneManager.GetActiveScene().name.Equals("P2"))
		{
			health = 550000000000L;
		}
	}

	private void Update()
	{
		if (!SceneManager.GetActiveScene().name.Equals("Tutorial") && !SceneManager.GetActiveScene().name.Equals("Respawn Point"))
		{
			healthBar.GetComponent<BossHealthBarScript>().SetHealth(health);
			CheckHealth();
		}
	}

	public void GetAttack(int[] damageValues)
	{
		this.damageValues = damageValues;
		long num = 0L;
		for (int i = 0; i < this.damageValues.Length; i++)
		{
			num += this.damageValues[i];
		}
		ChangeHealth(num);
	}

	public void ContinuousAttack(int[] damageValues)
	{
		this.damageValues = damageValues;
		long num = 0L;
		for (int i = 0; i < this.damageValues.Length; i++)
		{
			num += this.damageValues[i];
		}
		ChangeHealth(num);
		StartCoroutine(CreateDamageLines(this.damageValues));
	}

	private void ChangeHealth(long damage)
	{
		if (health - damage > 0)
		{
			health -= damage;
		}
		else
		{
			health = 0L;
		}
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (LayerMask.LayerToName(collider.gameObject.layer) != "Collider")
		{
			if (damageValues != null)
			{
				StartCoroutine(CreateDamageLines(damageValues));
			}
			damageValues = null;
		}
	}

	private IEnumerator CreateDamageLines(int[] damageLines)
	{
		float offsetY = 1f;
		for (int i = 0; i < damageLines.Length; i++)
		{
			Vector3 position = new Vector3(base.transform.position.x, base.transform.position.y + offsetY, -1f);
			Quaternion identity = Quaternion.identity;
			if (damageLines[i] == 0)
			{
				damageLine.SetText("MISS");
			}
			else
			{
				damageLine.SetText(damageLines[i].ToString());
			}
			Object.Instantiate(damageSkinPrefab, position, identity);
			offsetY += 0.36f;
			yield return new WaitForSeconds(0.1f);
		}
	}

	private void CheckHealth()
	{
		if (!SceneManager.GetActiveScene().name.Equals("P1"))
		{
			return;
		}
		if (health == 0L && !death)
		{
			death = true;
			if (!setDeathTimer)
			{
				setDeathTimer = true;
				deathTimer = Time.time;
			}
			GetComponent<CapsuleCollider2D>().enabled = false;
			if (GameObject.FindWithTag("EnemyAttack") != null)
			{
				Object.Destroy(GameObject.FindWithTag("EnemyAttack"));
				Object.Destroy(GameObject.FindWithTag("LaserSource"));
			}
			Vector3 position = deathPoint.position;
			position.z = -6f;
			Object.Instantiate(deathPrefab, position, deathPoint.rotation);
		}
		if (death && Time.time > deathTimer + 4.5f)
		{
			SceneManager.LoadScene("End");
		}
	}
}
