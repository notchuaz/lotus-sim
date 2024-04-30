using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
	private enum Direction
	{
		LEFT = -1,
		RIGHT = 1
	}

	private const float MAX_SPEED = 2.4f;

	private const float MIN_SPEED = 0f;

	private const float MAX_ARTIFICIAL_SPEED = 1f;

	private const float ARTIFICIAL_GAIN = 0.02f;

	private const float FRICTION = 0.96f;

	private const float SPEED_THRESHOLD = 0.5f;

	private const float AIR_DRAG_VALUE = 6f;

	private const float FLASH_JUMP_X_VEL = 8f;

	private const float FLASH_JUMP_Y_VEL_UP = 4f;

	private const float FLASH_JUMP_Y_VEL_DOWN = 0.4f;

	private const float FLASH_JUMP_UP_VEL = 10f;

	private const float FLOAT_DUR = 2f;

	private const float TP_X = 3.7f;

	private const float TP_Y = 0.5f;

	private const float TP_DIST = 3.92f;

	private const float TP_COLLISION_OFFSET = 0.4f;

	private const float TP_CD = 8f;

	private const float RUSH_FORCE = 6f;

	private const float RUSH_DIST = 3.7f;

	private float artificialSpeed;

	private float moveSpeed;

	private float jumpForce = 7.6f;

	private float moveInput;

	private float lastSpeed;

	private float stopMomentum = 2.4f;

	private float interval = 1f;

	private float floatTime;

	private float plummetInitTime;

	private float tpCD = -100f;

	private float rushDistance;

	private float stunDur;

	private float stunTime = -100f;

	private float leftCollider_x;

	private float rightCollider_x;

	private int lastDir;

	private int dir = -1;

	private int layerMask;

	private string attack = "";

	private bool isGrounded;

	private bool isSlowJumping;

	private bool canJumpInterrupt;

	private bool cancelJump;

	private bool isStopped;

	private bool hasFlashJumped;

	private bool hasFlashJumpedAttack;

	private bool isFloating;

	private bool isAnimFloat;

	private bool hasFloated;

	private bool canFlashFloat;

	private bool getFloatInitPos;

	private bool hasAttacked;

	private bool facingLeft = true;

	private bool isAttacking;

	private bool isAttackingAir;

	private bool isAttackingGround;

	private bool isAttackCD;

	private bool isAttackOver = true;

	private bool isPlummeting;

	private bool isPlummetingDown;

	private bool isPlummetFallInit;

	private bool getPlummetInitPos;

	private bool isTp;

	private bool isTpStopVelocity;

	private bool isTpFalling;

	private bool isRushing;

	private bool isMidRush;

	private bool isRushInit;

	private bool isGuarding;

	private bool isFlashJumping;

	private bool playJumpAudio;

	private bool summonTombstone;

	private bool respawn;

	private bool isStunned;

	private bool setStunTime;

	private Vector3 initFloatPos;

	private Vector3 initPlummetPos;

	private Vector3 initRushPos;

	private Rigidbody2D rb;

	private SpriteRenderer spriteRenderer;

	private GameObject playerWeapon;

	private GameObject PlayerInteractionManager;

	private AnimatorStateInfo isBindingAirStateInfo;

	private AudioSource audioSource;

	private SpriteRenderer sprite;

	public Animator animator;

	public Transform floatPoint;

	public Transform flashPoint;

	public Transform flashPointUp;

	public Transform plummetPoint;

	public Transform TpPoint;

	public Transform stunPoint;

	public GameObject floatPrefab;

	public GameObject flashPrefab;

	public GameObject plummetPrefab;

	public GameObject TpPrefab;

	public GameObject cdManager;

	public GameObject tombstonePrefab;

	public GameObject stunPrefab;

	public AudioClip jump;

	public AudioClip featherFloat;

	public AudioClip highRise;

	public AudioClip flash;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		cdManager = GameObject.Find("CDDisplay");
		isBindingAirStateInfo = animator.GetCurrentAnimatorStateInfo(0);
		playerWeapon = GameObject.Find("Player");
		PlayerInteractionManager = GameObject.Find("PlayerHitbox");
		audioSource = GetComponent<AudioSource>();
		sprite = GetComponent<SpriteRenderer>();
		layerMask = 128;
		if (SceneManager.GetActiveScene().name.Equals("Tutorial"))
		{
			leftCollider_x = -9.257f;
			rightCollider_x = 8.579f;
		}
		else
		{
			leftCollider_x = -7.4f;
			rightCollider_x = 5.81f;
		}
	}

	private void FixedUpdate()
	{
		if (isAttackingGround || isAttackCD || CheckDialogue() || PlayerInteractionManager.GetComponent<PlayerInteractionManager>().CheckDead() || isStunned)
		{
			return;
		}
		if (isGrounded && !isSlowJumping && !isAttackingGround && !isTp && !isRushing && !isMidRush && !isGuarding)
		{
			rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
		}
		if (isTpStopVelocity)
		{
			isTpStopVelocity = false;
			rb.velocity = new Vector2(0f, 0f);
		}
		if (!isAttackingAir && !isPlummeting)
		{
			if (moveInput > 0f && facingLeft)
			{
				Flip();
			}
			if (moveInput < 0f && !facingLeft)
			{
				Flip();
			}
		}
	}

	private void Update()
	{
		if (Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}
		if (CheckDialogue())
		{
			rb.velocity = new Vector2(0f, 0f);
		}
		if (isStunned)
		{
			if (isGrounded)
			{
				rb.velocity = new Vector2(0f, 0f);
				animator.SetFloat("Speed", 0f);
			}
			playerWeapon.GetComponent<PlayerWeapon>().GetStunned(isStunned);
			if (!setStunTime)
			{
				setStunTime = true;
				stunTime = Time.time;
				Vector3 position = stunPoint.position;
				position.z = -2f;
				UnityEngine.Object.Instantiate(stunPrefab, position, stunPoint.rotation);
			}
			if (Time.time > stunTime + stunDur)
			{
				isStunned = false;
				setStunTime = false;
				playerWeapon.GetComponent<PlayerWeapon>().GetStunned(isStunned);
				stunDur = 0f;
				if (GameObject.FindWithTag("Stun") != null)
				{
					UnityEngine.Object.Destroy(GameObject.FindWithTag("Stun"));
				}
			}
		}
		if (PlayerInteractionManager.GetComponent<PlayerInteractionManager>().CheckDead())
		{
			if (GameObject.FindWithTag("Stun") != null)
			{
				UnityEngine.Object.Destroy(GameObject.FindWithTag("Stun"));
				isStunned = false;
				setStunTime = false;
				playerWeapon.GetComponent<PlayerWeapon>().GetStunned(isStunned);
				stunDur = 0f;
			}
			if (!summonTombstone)
			{
				summonTombstone = true;
				respawn = true;
				Vector3 position2 = base.transform.position;
				position2.y = 4.5f;
				position2.z = -4f;
				UnityEngine.Object.Instantiate(tombstonePrefab, position2, Quaternion.identity);
				Color color = sprite.color;
				color.a = 0f;
				sprite.color = color;
			}
			rb.velocity = new Vector2(0f, 0f);
			Vector3 position3 = base.transform.position;
			position3.x = -0.69f;
			position3.y = -1.81f;
			base.transform.position = position3;
			if (isPlummetingDown || isPlummeting)
			{
				if (GameObject.FindWithTag("PlummetFall") != null)
				{
					UnityEngine.Object.Destroy(GameObject.FindWithTag("PlummetFall"));
				}
				rb.gravityScale = 2.4f;
				isPlummeting = false;
				isPlummetingDown = false;
				isPlummetFallInit = false;
				getPlummetInitPos = false;
				animator.SetBool("IsPlummeting", value: false);
			}
			if (isRushing || isMidRush)
			{
				isRushing = false;
				isMidRush = false;
				isRushInit = false;
				animator.SetBool("IsRushing", value: false);
			}
		}
		else if (respawn)
		{
			respawn = false;
			summonTombstone = false;
			Color color2 = sprite.color;
			color2.a = 1f;
			sprite.color = color2;
			if (GameObject.FindWithTag("Tombstone") != null)
			{
				UnityEngine.Object.Destroy(GameObject.FindWithTag("Tombstone"));
			}
			rb.gravityScale = 2.4f;
		}
		if (!isPlummeting && !isRushing && !isMidRush && !isGuarding && !isAttackCD && !CheckDialogue() && !PlayerInteractionManager.GetComponent<PlayerInteractionManager>().CheckDead() && !isStunned)
		{
			moveInput = Input.GetAxisRaw("Horizontal");
		}
		CheckPlummeting();
		CheckRushing();
		CheckGuarding();
		CheckAttackCD();
		SetArtificialSpeed();
		GetLastDirection();
		if (!isStunned)
		{
			animator.SetFloat("Speed", Mathf.Abs(moveInput));
		}
		if (isAttackCD)
		{
			rb.velocity = new Vector2(0f, 0f);
			animator.SetBool("IsAttackingCD", value: true);
		}
		else
		{
			animator.SetBool("IsAttackingCD", value: false);
		}
		if (isGuarding)
		{
			if (isGrounded)
			{
				rb.velocity = new Vector2(0f, 0f);
			}
			animator.SetBool("IsGuarding", value: true);
			animator.SetBool("IsJumping", value: false);
		}
		else
		{
			animator.SetBool("IsGuarding", value: false);
			if (!isGrounded)
			{
				animator.SetBool("IsJumping", value: true);
			}
		}
		if (Input.GetKeyDown(KeyCode.F) && !isPlummeting && !isRushing && !isMidRush && !isGuarding && Time.time > tpCD + 8f && !CheckDialogue())
		{
			audioSource.PlayOneShot(featherFloat, 1f);
			tpCD = Time.time;
			isTp = true;
			isTpStopVelocity = true;
			isGrounded = false;
			cdManager.GetComponent<CDDisplayScript>().CreateCooldown("tp", 8f);
			if (!isTpFalling)
			{
				isTpFalling = true;
			}
			if (isFloating)
			{
				isFloating = false;
				rb.gravityScale = 2.4f;
			}
			Vector3 position4 = TpPoint.position;
			position4.z = -0.9f;
			animator.SetBool("IsTp", value: true);
			UnityEngine.Object.Instantiate(TpPrefab, position4, TpPoint.rotation);
			Vector2 direction = new Vector2(-3.7f * (float)dir, 0.5f).normalized * 3.92f;
			RaycastHit2D raycastHit2D = Physics2D.Raycast(base.transform.position, direction, 3.92f, layerMask);
			if ((bool)raycastHit2D)
			{
				Vector2 point = raycastHit2D.point;
				rb.position = new Vector3(point.x + 0.4f * (float)dir, point.y, base.transform.position.z);
			}
			else
			{
				rb.position = new Vector3(base.transform.position.x - 3.7f * (float)dir, base.transform.position.y + 0.5f, base.transform.position.z);
			}
		}
		if (Input.GetButton("Jump") && isGrounded && !cancelJump && !isAttackingGround && !isAttackCD && !CheckDialogue() && !isGuarding)
		{
			if (!playJumpAudio)
			{
				audioSource.PlayOneShot(jump, 0.4f);
				playJumpAudio = true;
			}
			if (isStopped)
			{
				isStopped = false;
				lastSpeed = 0f;
			}
			if (Input.GetButton("Horizontal"))
			{
				isSlowJumping = false;
				rb.velocity = new Vector2(artificialSpeed * moveSpeed, jumpForce);
			}
			else
			{
				isSlowJumping = true;
				SlowJump();
			}
		}
		else if (!isGrounded)
		{
			playJumpAudio = false;
			if (Input.GetButton("Horizontal") && lastDir != dir)
			{
				Vector2 force = new Vector2(lastSpeed / 6f * (float)dir, 0f);
				rb.AddForce(force);
			}
		}
		else if (hasAttacked)
		{
			SlowMomentumAttack();
		}
		else if (!Input.GetButton("Horizontal"))
		{
			SlowMomentum();
		}
		else
		{
			isStopped = false;
			interval = 0f;
			moveSpeed = 2.4f;
		}
		if (!isGrounded)
		{
			if (isPlummeting && !isPlummetingDown && !isRushing && !isMidRush && !isGuarding)
			{
				if (isTp)
				{
					animator.SetBool("IsTp", value: false);
				}
				if (!GameObject.FindWithTag("PlummetInit"))
				{
					isPlummetingDown = true;
				}
				else
				{
					animator.SetBool("IsJumping", value: false);
					animator.SetBool("IsPlummeting", value: true);
				}
				if (!getPlummetInitPos)
				{
					plummetInitTime = Time.time;
					getPlummetInitPos = true;
					initPlummetPos = rb.transform.position;
				}
				rb.velocity = new Vector2(0f, 0f);
				rb.transform.position = initPlummetPos;
				rb.gravityScale = 0f;
			}
			else if (isPlummetingDown)
			{
				if (!isPlummetFallInit)
				{
					isPlummetFallInit = true;
					Vector3 position5 = plummetPoint.position;
					position5.z = -2f;
					UnityEngine.Object.Instantiate(plummetPrefab, position5, plummetPoint.rotation);
				}
				rb.gravityScale = 28f;
			}
			else
			{
				if (Input.GetKeyDown(KeyCode.D) && !isAttackingAir && !isRushing && !isMidRush && !isGuarding && !CheckDialogue())
				{
					if (!isFloating && !hasFloated)
					{
						audioSource.PlayOneShot(highRise, 1f);
						animator.SetBool("IsFloating", value: true);
						floatTime = Time.time;
						hasFloated = true;
						isFloating = true;
					}
					else
					{
						animator.SetBool("IsFloating", value: false);
						floatTime = 0f;
						isFloating = false;
						isAnimFloat = false;
						if (GameObject.FindGameObjectWithTag("Float") != null)
						{
							UnityEngine.Object.Destroy(GameObject.FindWithTag("Float"));
						}
						rb.gravityScale = 2.4f;
					}
				}
				if (isFloating && !isRushing && !isMidRush && !isGuarding)
				{
					if (hasFlashJumped && !canFlashFloat)
					{
						animator.SetBool("IsFloating", value: false);
						isFloating = false;
						isAnimFloat = false;
						if (GameObject.FindGameObjectWithTag("Float") != null)
						{
							UnityEngine.Object.Destroy(GameObject.FindWithTag("Float"));
						}
						rb.gravityScale = 2.4f;
					}
					else
					{
						Float();
						if (isTp)
						{
							animator.SetBool("IsTp", value: false);
						}
						if (!isAnimFloat)
						{
							isAnimFloat = true;
							Vector3 position6 = floatPoint.position;
							position6.z = -0.75f;
							UnityEngine.Object.Instantiate(floatPrefab, position6, floatPoint.rotation);
						}
						if (!isAttacking)
						{
							float num = (0f - Mathf.Sin(Time.time * 5f)) * 0.04f;
							base.transform.position = new Vector3(base.transform.position.x, initFloatPos.y + num, base.transform.position.z);
						}
					}
				}
				else
				{
					isFloating = false;
				}
				if ((Time.time > floatTime + 2f && isFloating) || isGuarding)
				{
					animator.SetBool("IsFloating", value: false);
					floatTime = 0f;
					isFloating = false;
					isAnimFloat = false;
					rb.gravityScale = 2.4f;
				}
				if (Input.GetButtonDown("Jump") && !hasFlashJumped && !hasFlashJumpedAttack)
				{
					if (!isFloating)
					{
						canFlashFloat = true;
					}
					isFlashJumping = true;
					FlashJump();
				}
			}
		}
		else
		{
			isFlashJumping = false;
			isFloating = false;
			isAnimFloat = false;
			getPlummetInitPos = false;
			hasFloated = false;
			canFlashFloat = false;
			getFloatInitPos = false;
			hasFlashJumped = false;
			animator.SetBool("IsFloating", value: false);
			if (GameObject.FindGameObjectWithTag("Float") != null)
			{
				UnityEngine.Object.Destroy(GameObject.FindWithTag("Float"));
			}
			if (isTp)
			{
				isTp = false;
				animator.SetBool("IsTp", value: false);
			}
			if (isPlummetingDown)
			{
				isPlummetFallInit = false;
				isPlummetingDown = false;
				rb.gravityScale = 2.4f;
				animator.SetBool("IsPlummeting", value: false);
				if (GameObject.FindGameObjectWithTag("PlummetFall") != null)
				{
					UnityEngine.Object.Destroy(GameObject.FindWithTag("PlummetFall"));
				}
			}
		}
		if (isRushing && !isMidRush)
		{
			animator.SetBool("IsRushing", value: true);
			animator.SetBool("IsJumping", value: false);
			animator.SetBool("IsTp", value: false);
			if (!GameObject.FindWithTag("RushInit"))
			{
				isMidRush = true;
			}
			rb.velocity = new Vector2(0f, 0f);
			rb.gravityScale = 0f;
		}
		else if (isMidRush)
		{
			if (!isRushInit)
			{
				isRushInit = true;
				initRushPos = base.transform.position;
			}
			if (initRushPos.x - 3.7f < leftCollider_x && dir == -1)
			{
				rushDistance = Math.Abs(leftCollider_x - initRushPos.x);
			}
			else if (initRushPos.x + 3.7f > rightCollider_x && dir == 1)
			{
				rushDistance = rightCollider_x - initRushPos.x;
			}
			else
			{
				rushDistance = 3.7f;
			}
			if ((double)rushDistance < 0.01)
			{
				rushDistance = 0f;
			}
			if (base.transform.position.x < initRushPos.x + rushDistance && dir == 1)
			{
				Vector2 force2 = new Vector2(6f * (float)dir, 0f);
				rb.AddForce(force2, ForceMode2D.Impulse);
			}
			else if (base.transform.position.x > initRushPos.x - rushDistance && dir == -1)
			{
				Vector2 force3 = new Vector2(6f * (float)dir, 0f);
				rb.AddForce(force3, ForceMode2D.Impulse);
			}
			else
			{
				animator.SetBool("IsRushing", value: false);
				rb.velocity = new Vector2(0f, 0f);
				rb.gravityScale = 2.4f;
				isRushing = false;
				isMidRush = false;
				isRushInit = false;
			}
		}
		if (!isAttackOver)
		{
			if (!isAttacking)
			{
				isAttacking = true;
				hasAttacked = true;
				animator.SetBool("IsTp", value: false);
				if (attack.Equals("main"))
				{
					animator.SetBool("IsAttacking", value: true);
					if (!isGrounded)
					{
						isAttackingAir = true;
						hasFlashJumpedAttack = true;
					}
					else
					{
						isAttackingGround = true;
					}
				}
			}
		}
		else
		{
			animator.SetBool("IsAttacking", value: false);
			isAttacking = false;
			hasAttacked = false;
			if (isAttackingGround)
			{
				isAttackingGround = false;
			}
		}
		if (!isAttacking && isAttackingAir && isFloating)
		{
			hasFlashJumpedAttack = false;
			isAttackingAir = false;
		}
		else if (!isAttacking && isAttackingAir && isGrounded)
		{
			hasFlashJumpedAttack = false;
			isAttackingAir = false;
		}
		if (isAttacking && isGrounded && isAttackingAir)
		{
			hasFlashJumpedAttack = false;
			isAttackingAir = false;
			isAttackingGround = true;
		}
		else if (isAttacking && isGrounded && attack.Equals("main"))
		{
			rb.velocity = new Vector2(0f, rb.velocity.y);
		}
		if (isAttacking && attack.Equals("reign"))
		{
			rb.velocity = new Vector2(0f, rb.velocity.y);
		}
		if (Input.GetButton("Jump") && !Input.GetButton("Horizontal"))
		{
			canJumpInterrupt = true;
		}
		if (canJumpInterrupt)
		{
			JumpInterrupt();
		}
		if (Input.GetButtonUp("Jump") && isSlowJumping)
		{
			isSlowJumping = false;
			Vector2 force4 = new Vector2(lastSpeed * (float)dir, rb.velocity.y);
			rb.AddForce(force4);
		}
		if (isGrounded && !Input.GetButton("Jump") && !isStopped)
		{
			lastSpeed = artificialSpeed * moveSpeed;
			lastDir = dir;
		}
	}

	public void IsAttackOver()
	{
		(isAttackOver, attack) = playerWeapon.GetComponent<PlayerWeapon>().AttackInfo();
	}

	public void GetStunned(float stunDur)
	{
		isStunned = true;
		this.stunDur += stunDur;
	}

	public bool GetGrounded()
	{
		return isGrounded;
	}

	public bool GetRushing()
	{
		return isMidRush;
	}

	public bool CheckDialogue()
	{
		if (GameObject.FindWithTag("DialogueBox") != null)
		{
			return true;
		}
		return false;
	}

	public bool CheckFlashJump()
	{
		return isFlashJumping;
	}

	public bool CheckFloat()
	{
		return isFloating;
	}

	public bool CheckTp()
	{
		return isTp;
	}

	public float GetY()
	{
		return base.transform.position.y;
	}

	public float GetX()
	{
		return base.transform.position.x;
	}

	private void CheckPlummeting()
	{
		isPlummeting = playerWeapon.GetComponent<PlayerWeapon>().GetPlummetingStatus();
	}

	private void CheckRushing()
	{
		isRushing = playerWeapon.GetComponent<PlayerWeapon>().GetRushingStatus();
	}

	private void CheckGuarding()
	{
		isGuarding = playerWeapon.GetComponent<PlayerWeapon>().GetGuardingStatus();
	}

	private void CheckAttackCD()
	{
		isAttackCD = playerWeapon.GetComponent<PlayerWeapon>().GetAttackCD();
	}

	private void SlowMomentum()
	{
		isStopped = true;
		interval += 0.02f;
		if (interval < 3.5f)
		{
			if (lastSpeed >= 4f)
			{
				stopMomentum = 4f * (float)Math.Pow(Math.E, -0.5f * interval);
			}
			else if (lastSpeed > 2f)
			{
				stopMomentum = 2.4f * (float)Math.Pow(Math.E, 0f - interval);
			}
			else
			{
				stopMomentum = 0f;
			}
			Vector2 force = new Vector2(stopMomentum * (float)lastDir, 0f);
			rb.AddForce(force);
		}
	}

	private void SlowMomentumAttack()
	{
		isStopped = true;
		interval += 0.001f;
		if (interval < 0.25f)
		{
			if (lastSpeed >= 4f)
			{
				stopMomentum = 4f * (float)Math.Pow(Math.E, -0.5f * interval);
			}
			else if (lastSpeed > 2f)
			{
				stopMomentum = 2.4f * (float)Math.Pow(Math.E, 0f - interval);
			}
			else
			{
				stopMomentum = 0f;
			}
			Vector2 force = new Vector2(stopMomentum * (float)lastDir, 0f);
			rb.AddForce(force);
		}
	}

	private void Flip()
	{
		facingLeft = !facingLeft;
		base.transform.Rotate(0f, 180f, 0f);
	}

	private void JumpInterrupt()
	{
		if (Input.GetButtonDown("Horizontal"))
		{
			cancelJump = true;
			isSlowJumping = false;
		}
		if (Input.GetButtonUp("Jump"))
		{
			cancelJump = false;
			canJumpInterrupt = false;
		}
	}

	private void SlowJump()
	{
		lastSpeed *= 0.96f;
		if (lastSpeed < 0f)
		{
			if (lastSpeed >= -0.5f)
			{
				lastSpeed = 0f;
			}
		}
		else if (lastSpeed > 0f && lastSpeed <= 0.5f)
		{
			lastSpeed = 0f;
		}
		rb.velocity = new Vector2(lastSpeed * (float)lastDir, jumpForce);
	}

	private void Float()
	{
		if (!getFloatInitPos)
		{
			getFloatInitPos = true;
			initFloatPos = rb.transform.position;
		}
		rb.velocity = new Vector2(0f, 0f);
		rb.transform.position = initFloatPos;
		rb.gravityScale = 0f;
	}

	private void FlashJump()
	{
		if (!(Math.Abs(rb.velocity.y) < 6f))
		{
			return;
		}
		audioSource.PlayOneShot(flash, 0.6f);
		hasFlashJumped = true;
		isStopped = false;
		interval = 0f;
		if (!Input.GetKey("up"))
		{
			Vector3 position = flashPoint.position;
			position.z = -0.75f;
			UnityEngine.Object.Instantiate(flashPrefab, position, flashPoint.rotation);
			if (Math.Abs(rb.velocity.y) < 5f)
			{
				if (lastDir != dir)
				{
					rb.velocity = new Vector2(5.28f * (float)dir, 4f);
					lastSpeed = 1.7424002f;
				}
				else
				{
					rb.velocity = new Vector2(8f * (float)dir, 4f);
					lastSpeed = 4f;
				}
			}
			else if (rb.velocity.y < 0f)
			{
				if (lastDir != dir)
				{
					rb.velocity = new Vector2(5.28f * (float)dir, 0f);
					lastSpeed = 1.7424002f;
				}
				else
				{
					rb.velocity = new Vector2(8f * (float)dir, 0f);
					lastSpeed = 4f;
				}
			}
		}
		else
		{
			Vector3 position2 = flashPointUp.position;
			position2.z = -0.75f;
			Quaternion rotation = flashPointUp.rotation;
			rotation.eulerAngles = new Vector3(rotation.eulerAngles.x, rotation.eulerAngles.y, -90f);
			UnityEngine.Object.Instantiate(flashPrefab, position2, rotation);
			if (rb.velocity.y > 0f)
			{
				rb.velocity = new Vector2(rb.velocity.x, 10f);
			}
			else if (rb.velocity.y < 0f)
			{
				rb.velocity = new Vector2(rb.velocity.x, 9f * (float)Math.Pow(Math.E, 0.1f * rb.velocity.y) + 1f);
			}
			lastSpeed = rb.velocity.x;
		}
	}

	private void SetArtificialSpeed()
	{
		if (!isGrounded)
		{
			return;
		}
		if (moveInput != 0f)
		{
			artificialSpeed += 0.02f;
			if (artificialSpeed > 1f)
			{
				artificialSpeed = 1f;
			}
		}
		else if (!Input.GetButton("Jump"))
		{
			artificialSpeed -= 0.02f;
			if (artificialSpeed < 0f)
			{
				artificialSpeed = 0f;
			}
		}
	}

	private void GetLastDirection()
	{
		if (moveInput < 0f)
		{
			dir = -1;
		}
		else if (moveInput > 0f)
		{
			dir = 1;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.CompareTag("Ground"))
		{
			isGrounded = true;
			animator.SetBool("IsJumping", value: false);
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.collider.CompareTag("Ground"))
		{
			isGrounded = false;
			animator.SetBool("IsJumping", value: true);
		}
	}
}
