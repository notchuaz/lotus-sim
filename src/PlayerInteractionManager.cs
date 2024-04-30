using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteractionManager : MonoBehaviour
{
	private const int MAX_HEALTH = 500000;

	private const float PASSIVE_HEALTH_INCREASE_CD = 10f;

	private const float POTION_CD = 5f;

	private const float BUFF_DUR = 60f;

	private const float BUFF_INFINITY_DUR = 30f;

	private const float DMG_MULT = 0.15f;

	private const float DMG_INFINITY_MULT = 0.15f;

	private const float RESPAWN_INVINCIBILITY = 2f;

	private const float LOTUS_ATTACK_CD = 2f;

	private AudioSource audioSource;

	public AudioClip die;

	public GameObject damageLinePrefab;

	public GameObject respawnPrefab;

	public GameObject healthBar;

	public GameObject cdManager;

	public GameObject playerMovement;

	public GameObject enemy;

	public Transform respawnPoint;

	public GameObject Lives;

	private int health = 500000;

	private int lives = 5;

	private float buffDur = -100f;

	private float buffInfinityDur = -100f;

	private float damageMultiplier = 1f;

	private float passiveHealthTime;

	private float potionCD = -100f;

	private float respawnInvincibility = -100f;

	private float sendToRespawnPoint;

	private float getLotusAttackCD = -100f;

	private float damage;

	private bool didNotGuard;

	private bool isHit;

	private bool isBuffed;

	private bool isInfinityBuffed;

	private bool isGuarding;

	private bool applyBuff;

	private bool applyInfinityBuff;

	private bool applyBuffOnce;

	private bool applyInfinityBuffOnce;

	private bool isDead;

	private bool playDeadAudio;

	private bool showRespawn;

	private bool subtractLife;

	private bool lotusAttack;

	private TextMeshPro damageLine;

	private void Start()
	{
		damageLine = damageLinePrefab.transform.GetChild(0).GetComponent<TextMeshPro>();
		cdManager = GameObject.Find("CDDisplay");
		audioSource = GetComponent<AudioSource>();
		playerMovement = GameObject.Find("Player");
	}

	private void Update()
	{
		CheckHealth();
		if (isDead && lives > 0)
		{
			Respawn();
		}
		if (isHit)
		{
			isHit = false;
			if (GameObject.FindWithTag("GuardKeydown") != null)
			{
				didNotGuard = false;
			}
			else
			{
				didNotGuard = true;
			}
		}
		if (isBuffed)
		{
			isBuffed = false;
			applyBuff = true;
			applyBuffOnce = true;
			buffDur = Time.time;
		}
		if (isInfinityBuffed)
		{
			isInfinityBuffed = false;
			applyInfinityBuff = true;
			applyInfinityBuffOnce = true;
			buffInfinityDur = Time.time;
		}
		if (applyBuff && Time.time < buffDur + 60f)
		{
			if (applyBuffOnce)
			{
				applyBuffOnce = false;
				damageMultiplier += 0.15f;
			}
		}
		else if (applyBuff)
		{
			applyBuff = false;
			damageMultiplier -= 0.15f;
		}
		if (applyInfinityBuff && Time.time < buffInfinityDur + 30f)
		{
			if (applyInfinityBuffOnce)
			{
				applyInfinityBuffOnce = false;
				damageMultiplier += 0.15f;
			}
		}
		else if (applyInfinityBuff)
		{
			applyInfinityBuff = false;
			damageMultiplier -= 0.15f;
		}
		if (Time.time > passiveHealthTime + 10f)
		{
			passiveHealthTime = Time.time;
			if (health < 500000)
			{
				health = (int)((float)health * 1.04f);
				if (health > 500000)
				{
					health = 500000;
				}
			}
		}
		if (GameObject.FindWithTag("GuardKeydown") == null)
		{
			isGuarding = false;
		}
		if (Input.GetKeyDown(KeyCode.A) && Time.time > potionCD + 5f && !isDead)
		{
			potionCD = Time.time;
			cdManager.GetComponent<CDDisplayScript>().CreateCooldown("potion", 5f);
			Heal();
		}
		healthBar.GetComponent<HealthBarScript>().SetHealth(health);
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (!(LayerMask.LayerToName(collider.gameObject.layer) != "Collider"))
		{
			return;
		}
		if (collider.gameObject.CompareTag("EnemyAttack"))
		{
			isHit = true;
			CreateDamage(damage);
		}
		if (!(Time.time > respawnInvincibility + 2f))
		{
			return;
		}
		if (collider.gameObject.CompareTag("SmallDebris") && GameObject.FindWithTag("GuardKeydown") == null)
		{
			playerMovement.GetComponent<PlayerMovement>().GetStunned(0.5f);
		}
		if (collider.gameObject.CompareTag("MediumDebris") && GameObject.FindWithTag("GuardKeydown") == null)
		{
			playerMovement.GetComponent<PlayerMovement>().GetStunned(1f);
		}
		if (collider.gameObject.CompareTag("XMediumDebris") && GameObject.FindWithTag("GuardKeydown") == null)
		{
			playerMovement.GetComponent<PlayerMovement>().GetStunned(1f);
		}
		if (collider.gameObject.CompareTag("LargeDebris") && GameObject.FindWithTag("GuardKeydown") == null)
		{
			playerMovement.GetComponent<PlayerMovement>().GetStunned(1f);
		}
		if (collider.gameObject.CompareTag("EnemyAttack") && Time.time > getLotusAttackCD + 2f)
		{
			if (!lotusAttack)
			{
				lotusAttack = true;
				getLotusAttackCD = Time.time;
			}
			if (GameObject.FindWithTag("GuardKeydown") == null)
			{
				playerMovement.GetComponent<PlayerMovement>().GetStunned(1f);
			}
		}
		if (lotusAttack && Time.time > getLotusAttackCD + 2f)
		{
			lotusAttack = false;
		}
	}

	public void GetAttack(float percent)
	{
		damage = 500000f * percent;
		CreateDamage(damage);
	}

	public int GetHealth()
	{
		return health;
	}

	public void GetBuff()
	{
		isBuffed = true;
	}

	public void GetInfinityBuff()
	{
		isInfinityBuffed = true;
	}

	public float GetDamageMultiplier()
	{
		return damageMultiplier;
	}

	public bool DidGuard()
	{
		return didNotGuard;
	}

	public bool CheckDead()
	{
		return isDead;
	}

	private void CreateDamage(float damage)
	{
		float num = 0.6f;
		float num2 = -0.05f;
		didNotGuard = false;
		Vector3 position = new Vector3(base.transform.position.x + num2, base.transform.position.y + num, -3f);
		Quaternion identity = Quaternion.identity;
		if (!(Time.time > respawnInvincibility + 2f))
		{
			return;
		}
		if (GameObject.FindWithTag("GuardKeydown") != null)
		{
			if (!isGuarding)
			{
				isGuarding = true;
				damageLine.SetText("GUARD");
				Object.Instantiate(damageLinePrefab, position, identity);
			}
			return;
		}
		if (damage == 0f)
		{
			damageLine.SetText("MISS");
			Object.Instantiate(damageLinePrefab, position, identity);
			return;
		}
		damageLine.SetText(((int)damage).ToString());
		if (health > 0)
		{
			Object.Instantiate(damageLinePrefab, position, identity);
		}
		health -= (int)damage;
		if (health < 0)
		{
			health = 0;
		}
	}

	private void Heal()
	{
		health = 500000;
	}

	private void CheckHealth()
	{
		if (health != 0)
		{
			return;
		}
		if (!subtractLife)
		{
			subtractLife = true;
			lives--;
			Lives.transform.GetChild(0).GetComponent<TextMeshPro>().SetText(lives.ToString());
			if (lives == 0)
			{
				sendToRespawnPoint = Time.time;
			}
		}
		if (lives == 0 && Time.time > sendToRespawnPoint + 2f)
		{
			SceneManager.LoadScene("Respawn Point");
		}
		if (!playDeadAudio)
		{
			playDeadAudio = true;
			audioSource.PlayOneShot(die, 0.3f);
		}
		isDead = true;
	}

	private void Respawn()
	{
		if (!showRespawn)
		{
			playDeadAudio = false;
			showRespawn = true;
			Vector3 position = respawnPoint.position;
			position.z = -5f;
			Object.Instantiate(respawnPrefab, position, respawnPoint.rotation);
		}
		if (Input.GetKeyDown(KeyCode.Return))
		{
			isDead = false;
			showRespawn = false;
			subtractLife = false;
			health = 500000;
			if (GameObject.FindWithTag("Respawn") != null)
			{
				Object.Destroy(GameObject.FindWithTag("Respawn"));
			}
			respawnInvincibility = Time.time;
		}
	}
}
