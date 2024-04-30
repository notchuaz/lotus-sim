using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerWeapon : MonoBehaviour
{
	private const float ATTACK_DUR = 0.6f;

	private const float REIGN_DUR = 10f;

	private const float INFINITY_DUR = 30f;

	private const float GUARD_DUR = 3f;

	private const float BIND_DUR = 10f;

	private const float PLUMMET_CD = 1.5f;

	private const float RUSH_CD = 5f;

	private const float REIGN_CD = 30f;

	private const float RUIN_CD = 60f;

	private const float INFINITY_CD = 180f;

	private const float GUARD_CD = 25f;

	private const float WRATH_CD = 90f;

	private const float BIND_CD = 180f;

	private const float ATTACK_CD = 0.6f;

	public Transform attackPoint;

	public Transform plummetPoint;

	public Transform plummetHitPoint;

	public Transform rushPoint;

	public Transform rushMidPoint;

	public Transform rushEndPoint;

	public Transform reignPoint;

	public Transform reignEffectPoint;

	public Transform ruinPoint;

	public Transform guardPoint;

	public Transform guardShieldPoint;

	public Transform graveSummonPoint;

	public Transform graveHitPoint;

	public Transform graveEffectPoint;

	public Transform wrathPoint;

	public Transform bindInitPoint;

	public Transform bindSummonPoint;

	public Transform bindEndPoint;

	public GameObject mainAttackPrefabForm1;

	public GameObject mainAttackPrefabForm2;

	public GameObject mainAttackPrefabForm3;

	public GameObject plummetPrefab;

	public GameObject plummetHitPrefab;

	public GameObject rushPrefab;

	public GameObject rushMidPrefab;

	public GameObject rushEndPrefab;

	public GameObject reignStandPrefab;

	public GameObject reignSummonPrefab;

	public GameObject reignEndPrefab;

	public GameObject reignEffectPrefab;

	public GameObject ruinPrefab;

	public GameObject infinityStart0Prefab;

	public GameObject infinityStart1Prefab;

	public GameObject infinityDur0Prefab;

	public GameObject infinityDur1Prefab;

	public GameObject infinityEnd0Prefab;

	public GameObject infinityEnd1Prefab;

	public GameObject guardKeydownPrefab;

	public GameObject guardShieldPrefab;

	public GameObject graveSummonPrefab;

	public GameObject graveHitPrefab;

	public GameObject graveEffectPrefab;

	public GameObject wrathPrefab;

	public GameObject bindInitPrefab;

	public GameObject bindSummonPrefab;

	public GameObject bindEndPrefab;

	public GameObject cam;

	private GameObject player;

	private GameObject PlayerInteractionManager;

	private GameObject cdManager;

	private AudioSource audioSource;

	public AudioClip cleave1;

	public AudioClip cleave2;

	public AudioClip cleave3;

	public AudioClip plummet1;

	public AudioClip plummet2;

	public AudioClip ruin;

	public AudioClip rush1;

	public AudioClip rush2;

	public AudioClip wrath;

	public AudioClip reign1;

	public AudioClip reign2;

	public AudioClip grave;

	public AudioClip bind;

	private float attackCD = -100f;

	private float plummetCD = -100f;

	private float rushCD = -100f;

	private float reignCD = -100f;

	private float ruinCD = -100f;

	private float infinityCD = -1000f;

	private float guardCD = -100f;

	private float wrathCD = -100f;

	private float bindCD = -1000f;

	private float reignTime;

	private float infinityTime;

	private float guardTime;

	private float bindTime;

	private float startInfinityLoop;

	private float attackMainCD;

	private float attackStart;

	private string attackType = "";

	private int attackTypeForm;

	private int layerMask;

	private bool isAttackOver = true;

	private bool isAttackCD;

	private bool isPlummeting;

	private bool isPlummetHit;

	private bool isRushing;

	private bool isRushingMid;

	private bool isRushingEnd;

	private bool isReignStand;

	private bool isReignEnd;

	private bool isInfinity;

	private bool isInfinityStart;

	private bool isGuarding;

	private bool isBinding;

	private bool isBindingOnce;

	private bool isInRange;

	private bool hasStartedInfinityLoop;

	private bool hasStartedInfinityEnd;

	private bool hasFinishedInfinityEnd;

	private bool playInfinityBG;

	private bool isStunned;

	private bool canUseCleave = true;

	private bool canUseRush = true;

	private bool canUsePlummet = true;

	private bool canUseReign = true;

	private bool canUseRuin = true;

	private bool canUseInfinity = true;

	private bool canUseBind = true;

	private bool canUseIframe = true;

	private bool canUseBuff = true;

	private bool tutCleave;

	private bool tutRush;

	private bool tutPlummet;

	private bool tutReign;

	private bool tutRuin;

	private bool tutInfinity;

	private bool tutBind;

	private bool tutBuff;

	private Vector3 reignInitPos;

	private void Start()
	{
		player = GameObject.Find("Player");
		PlayerInteractionManager = GameObject.Find("PlayerHitbox");
		cam = GameObject.Find("Main Camera");
		audioSource = GetComponent<AudioSource>();
		cdManager = GameObject.Find("CDDisplay");
		layerMask = 64;
		if (SceneManager.GetActiveScene().name.Equals("Tutorial"))
		{
			canUseCleave = false;
			canUseRush = false;
			canUsePlummet = false;
			canUseReign = false;
			canUseRuin = false;
			canUseInfinity = false;
			canUseBind = false;
			canUseIframe = false;
			canUseBuff = false;
			tutCleave = false;
			tutRush = false;
			tutPlummet = false;
			tutReign = false;
			tutRuin = false;
			tutInfinity = false;
			tutBind = false;
			tutBuff = false;
		}
	}

	private void Update()
	{
		if (!PlayerInteractionManager.GetComponent<PlayerInteractionManager>().CheckDead())
		{
			if (!isPlummeting && !CheckDialogue() && !isStunned)
			{
				if (Input.GetKey(KeyCode.LeftControl) && isAttackOver && Time.time > attackMainCD + 0.6f && Time.time > attackCD + 0.6f && !isGuarding && canUseCleave)
				{
					attackStart = Time.time;
					attackMainCD = Time.time;
					attackType = "main";
					tutCleave = true;
					Attack();
				}
				if (Input.GetKey(KeyCode.Q) && isAttackOver && Time.time > attackCD + 0.6f && Time.time > guardCD + 25f && !isRushing && !isRushingMid && canUseIframe)
				{
					attackType = "guard";
					attackCD = Time.time;
					guardTime = Time.time;
					guardCD = Time.time;
					isGuarding = true;
					cdManager.GetComponent<CDDisplayScript>().CreateCooldown(attackType, 25f);
					Attack();
				}
			}
			if (!isGuarding && !CheckDialogue())
			{
				if (Input.GetKeyDown(KeyCode.LeftShift) && isAttackOver && Time.time > plummetCD + 1.5f && Time.time > attackCD + 0.6f && !player.GetComponent<PlayerMovement>().GetGrounded() && player.GetComponent<PlayerMovement>().GetY() > -3.4f && !isPlummeting && canUsePlummet && !isStunned)
				{
					attackType = "plummet";
					attackCD = Time.time;
					plummetCD = Time.time;
					isPlummeting = true;
					isPlummetHit = true;
					tutPlummet = true;
					cdManager.GetComponent<CDDisplayScript>().CreateCooldown(attackType, 1.5f);
					Attack();
				}
				else if (player.GetComponent<PlayerMovement>().GetGrounded())
				{
					if (isPlummetHit)
					{
						isPlummetHit = false;
						audioSource.PlayOneShot(plummet2);
						Vector3 position = plummetHitPoint.position;
						position.z = -2f;
						Object.Instantiate(plummetHitPrefab, position, plummetHitPoint.rotation);
					}
					isPlummeting = false;
				}
				if (Input.GetKeyDown(KeyCode.X) && isAttackOver && Time.time > attackCD + 0.6f && Time.time > rushCD + 5f && canUseRush && !isStunned)
				{
					attackType = "rush";
					attackCD = Time.time;
					rushCD = Time.time;
					isRushing = true;
					isRushingMid = true;
					tutRush = true;
					cdManager.GetComponent<CDDisplayScript>().CreateCooldown(attackType, 5f);
					Attack();
				}
				else if (player.GetComponent<PlayerMovement>().GetRushing())
				{
					if (isRushingMid)
					{
						isRushing = false;
						isRushingMid = false;
						isRushingEnd = true;
						audioSource.PlayOneShot(rush2);
						Vector3 position2 = rushMidPoint.position;
						position2.z = -2f;
						Object.Instantiate(rushMidPrefab, position2, rushMidPoint.rotation);
					}
				}
				else
				{
					if (GameObject.FindWithTag("RushMid") != null)
					{
						Object.Destroy(GameObject.FindWithTag("RushMid"));
					}
					if (isRushingEnd)
					{
						isRushingEnd = false;
						Vector3 position3 = rushEndPoint.position;
						position3.z = -2f;
						Object.Instantiate(rushEndPrefab, position3, rushEndPoint.rotation);
					}
				}
				if (Input.GetKeyDown(KeyCode.C) && isAttackOver && Time.time > attackCD + 0.6f && Time.time > reignCD + 30f && player.GetComponent<PlayerMovement>().GetGrounded() && canUseReign && !isStunned)
				{
					attackType = "reign";
					isAttackCD = true;
					attackCD = Time.time;
					reignCD = Time.time;
					isReignStand = true;
					isReignEnd = false;
					reignInitPos = reignPoint.position;
					tutReign = true;
					cdManager.GetComponent<CDDisplayScript>().CreateCooldown(attackType, 30f);
					Attack();
				}
				if (Input.GetKeyDown(KeyCode.Alpha1) && isAttackOver && Time.time > attackCD + 0.6f && Time.time > ruinCD + 60f && player.GetComponent<PlayerMovement>().GetGrounded() && canUseRuin && !isStunned)
				{
					attackType = "ruin";
					isAttackCD = true;
					ruinCD = Time.time;
					attackCD = Time.time;
					tutRuin = true;
					cdManager.GetComponent<CDDisplayScript>().CreateCooldown(attackType, 60f);
					Attack();
				}
				if (Input.GetKeyDown(KeyCode.Alpha2) && isAttackOver && Time.time > attackCD + 0.6f && Time.time > infinityCD + 180f && player.GetComponent<PlayerMovement>().GetGrounded() && canUseInfinity && !isStunned)
				{
					attackType = "infinity";
					attackCD = Time.time;
					infinityTime = Time.time;
					infinityCD = Time.time;
					isInfinity = true;
					isInfinityStart = true;
					isAttackCD = true;
					startInfinityLoop = Time.time;
					playInfinityBG = true;
					tutInfinity = true;
					PlayerInteractionManager.GetComponent<PlayerInteractionManager>().GetInfinityBuff();
					cdManager.GetComponent<CDDisplayScript>().CreateCooldown(attackType, 180f);
					Attack();
				}
				if (Input.GetKeyDown(KeyCode.F1) && isAttackOver && Time.time > attackCD + 0.6f && Time.time > wrathCD + 90f && player.GetComponent<PlayerMovement>().GetGrounded() && canUseBuff && !isStunned)
				{
					attackType = "buff";
					attackCD = Time.time;
					wrathCD = Time.time;
					isAttackCD = true;
					tutBuff = true;
					PlayerInteractionManager.GetComponent<PlayerInteractionManager>().GetBuff();
					cdManager.GetComponent<CDDisplayScript>().CreateCooldown(attackType, 90f);
					Attack();
				}
				if (Input.GetKeyDown(KeyCode.W) && isAttackOver && Time.time > attackCD + 0.6f && Time.time > bindCD + 180f && player.GetComponent<PlayerMovement>().GetGrounded() && canUseBind && !isStunned)
				{
					attackType = "bind";
					attackCD = Time.time;
					bindCD = Time.time;
					isBinding = true;
					isBindingOnce = true;
					isAttackCD = true;
					tutBind = true;
					Vector2 direction = -base.transform.right * 4f;
					RaycastHit2D[] array = Physics2D.RaycastAll(base.transform.position, direction, 4f, layerMask);
					for (int i = 0; i < array.Length; i++)
					{
						Debug.Log(array[i].collider.name);
						if (array[i].collider.name.Equals("Enemy") || array[i].collider.name.Equals("EnemyRange"))
						{
							isInRange = true;
						}
					}
					cdManager.GetComponent<CDDisplayScript>().CreateCooldown(attackType, 180f);
					Attack();
				}
			}
		}
		else
		{
			if (GameObject.FindWithTag("RushMid") != null)
			{
				Object.Destroy(GameObject.FindWithTag("RushMid"));
			}
			isPlummeting = false;
			isPlummetHit = false;
			isRushing = false;
			isRushingMid = false;
			isRushingEnd = false;
		}
		if (isReignStand && GameObject.FindWithTag("ReignStand") == null)
		{
			isReignStand = false;
			Vector3 position4 = reignInitPos;
			position4.z = -2f;
			Object.Instantiate(reignSummonPrefab, position4, reignPoint.rotation);
			reignTime = Time.time;
		}
		if (Time.time > reignTime + 10f && GameObject.FindWithTag("ReignSummon") != null)
		{
			Object.Destroy(GameObject.FindWithTag("ReignSummon"));
			if (!isReignEnd)
			{
				isReignEnd = true;
				Vector3 position5 = reignInitPos;
				position5.z = -2f;
				Object.Instantiate(reignEndPrefab, position5, reignPoint.rotation);
			}
		}
		if (Time.time > startInfinityLoop + 1f && isInfinity && !hasStartedInfinityLoop && isInfinityStart)
		{
			hasStartedInfinityLoop = true;
			isInfinityStart = false;
			Vector3 position6 = cam.transform.position;
			position6.z = 1f;
			Object.Instantiate(infinityDur0Prefab, position6, cam.transform.rotation);
			position6.z = -4f;
			Object.Instantiate(infinityDur1Prefab, position6, cam.transform.rotation);
			hasStartedInfinityEnd = true;
			Vector3 position7 = graveEffectPoint.position;
			position7.z = -2f;
			Object.Instantiate(graveEffectPrefab, position7, graveEffectPoint.rotation);
		}
		if (Time.time > infinityTime + 30f && hasStartedInfinityEnd)
		{
			if (GameObject.FindWithTag("InfinityDur") != null)
			{
				Object.Destroy(GameObject.FindWithTag("InfinityDur"));
			}
			if (GameObject.FindWithTag("InfinityDur1") != null)
			{
				Object.Destroy(GameObject.FindWithTag("InfinityDur1"));
			}
			if (GameObject.FindWithTag("GraveEffect") != null)
			{
				Object.Destroy(GameObject.FindWithTag("GraveEffect"));
			}
			hasStartedInfinityEnd = false;
			Vector3 position8 = cam.transform.position;
			position8.z = 1f;
			Object.Instantiate(infinityEnd0Prefab, position8, cam.transform.rotation);
			position8.z = -4f;
			Object.Instantiate(infinityEnd1Prefab, position8, cam.transform.rotation);
			isInfinity = false;
			isInfinityStart = false;
			hasStartedInfinityLoop = false;
			hasFinishedInfinityEnd = true;
		}
		if (hasFinishedInfinityEnd && GameObject.FindWithTag("InfinityEnd") == null)
		{
			playInfinityBG = false;
			hasFinishedInfinityEnd = false;
		}
		if (isGuarding && (Input.GetKeyUp(KeyCode.Q) || Time.time > guardTime + 3f))
		{
			isGuarding = false;
			if (GameObject.FindWithTag("GuardKeydown") != null)
			{
				Object.Destroy(GameObject.FindWithTag("GuardKeydown"));
			}
		}
		if (isBinding && GameObject.FindWithTag("BindSummon") == null && isInRange)
		{
			if (isBindingOnce)
			{
				isBindingOnce = false;
				Vector3 position9 = bindEndPoint.position;
				position9.z = -0.8f;
				Object.Instantiate(bindEndPrefab, position9, bindEndPoint.rotation);
				bindTime = Time.time;
			}
			if (Time.time > bindTime + 10f)
			{
				if (GameObject.FindWithTag("BindEnd") != null)
				{
					Object.Destroy(GameObject.FindWithTag("BindEnd"));
				}
				isBinding = false;
				isInRange = false;
			}
		}
		if (Time.time < attackStart + 0.6f)
		{
			isAttackOver = false;
			IsAttackOver();
		}
		else
		{
			isAttackOver = true;
			IsAttackOver();
		}
		if (Time.time > attackCD + 0.6f && isAttackCD)
		{
			isAttackCD = false;
		}
	}

	public (bool, string) AttackInfo()
	{
		return (isAttackOver, attackType);
	}

	public void GetStunned(bool isStunned)
	{
		this.isStunned = isStunned;
	}

	public int AttackFormInfo()
	{
		return attackTypeForm;
	}

	public bool GetPlummetingStatus()
	{
		return isPlummeting;
	}

	public bool GetRushingStatus()
	{
		return isRushing;
	}

	public bool GetGuardingStatus()
	{
		return isGuarding;
	}

	public bool GetInfinityStatus()
	{
		return playInfinityBG;
	}

	public bool GetAttackCD()
	{
		return isAttackCD;
	}

	public bool GetTutCleave()
	{
		return tutCleave;
	}

	public bool GetTutRush()
	{
		return tutRush;
	}

	public bool GetTutPlummet()
	{
		return tutPlummet;
	}

	public bool GetTutReign()
	{
		return tutReign;
	}

	public bool GetTutRuin()
	{
		return tutRuin;
	}

	public bool GetTutInfinity()
	{
		return tutInfinity;
	}

	public bool GetTutBind()
	{
		return tutBind;
	}

	public bool GetTutBuff()
	{
		return tutBuff;
	}

	public void CreateGuardShield()
	{
		Vector3 position = guardShieldPoint.position;
		position.z = -2f;
		Object.Instantiate(guardShieldPrefab, position, guardShieldPoint.rotation);
	}

	public void TutorialUnlockSkills(int step)
	{
		if (step == 5)
		{
			canUseCleave = true;
		}
		if (step == 7)
		{
			canUseRush = true;
		}
		if (step == 9)
		{
			canUsePlummet = true;
		}
		if (step == 11)
		{
			canUseReign = true;
		}
		if (step == 13)
		{
			canUseRuin = true;
		}
		if (step == 15)
		{
			canUseInfinity = true;
		}
		if (step == 17)
		{
			canUseBind = true;
		}
		if (step == 19)
		{
			canUseIframe = true;
		}
		if (step == 21)
		{
			canUseBuff = true;
		}
	}

	private void IsAttackOver()
	{
		player.GetComponent<PlayerMovement>().IsAttackOver();
	}

	private bool CheckDialogue()
	{
		if (!player.GetComponent<PlayerMovement>().CheckDialogue())
		{
			return false;
		}
		return true;
	}

	private void Attack()
	{
		if (attackType.Equals("main"))
		{
			Vector3 position = attackPoint.position;
			position.z = -2f;
			attackTypeForm = Random.Range(1, 4);
			if (attackTypeForm == 1)
			{
				audioSource.PlayOneShot(cleave1);
				Object.Instantiate(mainAttackPrefabForm1, position, attackPoint.rotation);
			}
			else if (attackTypeForm == 2)
			{
				audioSource.PlayOneShot(cleave2);
				Object.Instantiate(mainAttackPrefabForm2, position, attackPoint.rotation);
			}
			else if (attackTypeForm == 3)
			{
				audioSource.PlayOneShot(cleave3);
				Object.Instantiate(mainAttackPrefabForm3, position, attackPoint.rotation);
			}
			player.GetComponent<PlayerMovement>().IsAttackOver();
		}
		if (attackType.Equals("plummet"))
		{
			audioSource.PlayOneShot(plummet1);
			Vector3 position2 = plummetPoint.position;
			position2.z = -2f;
			Object.Instantiate(plummetPrefab, position2, plummetPoint.rotation);
		}
		if (attackType.Equals("rush"))
		{
			audioSource.PlayOneShot(rush1);
			Vector3 position3 = rushPoint.position;
			position3.z = -2f;
			Object.Instantiate(rushPrefab, position3, rushPoint.rotation);
		}
		if (attackType.Equals("reign"))
		{
			audioSource.PlayOneShot(reign1);
			Vector3 position4 = reignPoint.position;
			position4.z = -2f;
			Object.Instantiate(reignStandPrefab, position4, reignPoint.rotation);
			audioSource.PlayOneShot(reign2, 0.7f);
			Vector3 position5 = reignEffectPoint.position;
			position5.z = -2f;
			Object.Instantiate(reignEffectPrefab, position5, reignEffectPoint.rotation);
		}
		if (attackType.Equals("ruin"))
		{
			audioSource.PlayOneShot(ruin);
			Vector3 position6 = ruinPoint.position;
			position6.z = -2f;
			Object.Instantiate(ruinPrefab, position6, ruinPoint.rotation);
		}
		if (attackType.Equals("infinity"))
		{
			Vector3 position7 = cam.transform.position;
			position7.z = 1f;
			Object.Instantiate(infinityStart0Prefab, position7, cam.transform.rotation);
			position7.z = -4f;
			Object.Instantiate(infinityStart1Prefab, position7, cam.transform.rotation);
			audioSource.PlayOneShot(grave);
			Vector3 position8 = graveSummonPoint.position;
			position8.z = -2f;
			Object.Instantiate(graveSummonPrefab, position8, graveSummonPoint.rotation);
			Vector3 position9 = graveHitPoint.position;
			position9.z = -2f;
			Object.Instantiate(graveHitPrefab, position9, graveHitPoint.rotation);
		}
		if (attackType.Equals("guard"))
		{
			Vector3 position10 = guardPoint.position;
			position10.z = -2f;
			Object.Instantiate(guardKeydownPrefab, position10, guardPoint.rotation);
		}
		if (attackType.Equals("buff"))
		{
			audioSource.PlayOneShot(wrath);
			Vector3 position11 = wrathPoint.position;
			position11.z = -2f;
			Object.Instantiate(wrathPrefab, position11, wrathPoint.rotation);
		}
		if (attackType.Equals("bind"))
		{
			audioSource.PlayOneShot(bind);
			Vector3 position12 = bindInitPoint.position;
			position12.z = -2f;
			Object.Instantiate(bindInitPrefab, position12, bindInitPoint.rotation);
			if (isInRange)
			{
				Vector3 position13 = bindSummonPoint.position;
				position13.z = -0.8f;
				Object.Instantiate(bindSummonPrefab, position13, bindSummonPoint.rotation);
			}
		}
	}
}
