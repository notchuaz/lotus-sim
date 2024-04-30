using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
	private const float ATTACK_CD = 4f;

	private float attackCD;

	private bool isAttackModeOn;

	private Animator animator;

	public bool laserBegin;

	public bool lasers;

	public Transform laserPoint;

	public GameObject laserSpawnBeginPrefab;

	public GameObject laserSpawnPrefab;

	public GameObject laserBeginPrefab;

	public GameObject laserPrefab;

	public GameObject player;

	public GameObject range;

	private bool facingRight = true;

	private bool isMoving;

	private int dir = -1;

	private Coroutine moveToPlayer;

	private float DECISION_CD = 2f;

	private float decisionCD = -100f;

	private bool isFollowing;

	private bool isAttacking;

	private void Start()
	{
		animator = GetComponent<Animator>();
	}

	private void Update()
	{
		if (SceneManager.GetActiveScene().name.Equals("Tutorial"))
		{
			if (GameObject.FindWithTag("BindEnd") == null)
			{
				if (animator.GetBool("IsBinded"))
				{
					attackCD = Time.time;
					animator.SetBool("IsBinded", value: false);
				}
				if (isAttackModeOn)
				{
					if (Time.time > attackCD + 4f)
					{
						attackCD = Time.time;
						animator.SetBool("IsAttacking", value: true);
					}
					AnimatorStateInfo currentAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
					if (currentAnimatorStateInfo.normalizedTime >= 1f && currentAnimatorStateInfo.IsName("Dummy_attack") && !animator.IsInTransition(0))
					{
						animator.SetBool("IsAttacking", value: false);
					}
				}
				else
				{
					animator.SetBool("IsAttacking", value: false);
				}
			}
			else
			{
				animator.SetBool("IsBinded", value: true);
			}
		}
		else if (SceneManager.GetActiveScene().name.Equals("P1"))
		{
			if (!laserBegin)
			{
				laserBegin = true;
				Vector3 position = laserPoint.position;
				position.z = -2f;
				Object.Instantiate(laserBeginPrefab, position, laserPoint.rotation);
				position.z = -3f;
				Object.Instantiate(laserSpawnBeginPrefab, position, laserPoint.rotation);
			}
			if (laserBegin && GameObject.FindWithTag("LaserBegin") == null && !lasers)
			{
				lasers = true;
				Vector3 position2 = laserPoint.position;
				position2.z = -2f;
				Object.Instantiate(laserPrefab, position2, laserPoint.rotation);
				position2.z = -3f;
				Object.Instantiate(laserSpawnPrefab, position2, laserPoint.rotation);
			}
		}
		else
		{
			if (!SceneManager.GetActiveScene().name.Equals("P2"))
			{
				return;
			}
			if (GameObject.FindWithTag("BindEnd") == null)
			{
				if (animator.GetBool("IsBinded"))
				{
					attackCD = Time.time;
					animator.SetBool("IsBinded", value: false);
				}
				float num = (0f - Mathf.Sin(Time.time * 5f)) * 0.001f;
				base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + num, base.transform.position.z);
				if (player.transform.position.x < base.transform.position.x && !facingRight && !isAttacking)
				{
					Flip();
				}
				if (player.transform.position.x > base.transform.position.x && facingRight && !isAttacking)
				{
					Flip();
				}
				if (Time.time > decisionCD + DECISION_CD)
				{
					decisionCD = Time.time;
					if (Random.Range(1, 100) < 98)
					{
						isFollowing = true;
					}
				}
				if (isFollowing)
				{
					if (!range.GetComponent<EnemyGetRange>().GetInRange() && !isMoving && !isAttacking)
					{
						isMoving = true;
						moveToPlayer = StartCoroutine(MoveToPlayer());
					}
					else if (range.GetComponent<EnemyGetRange>().GetInRange())
					{
						isFollowing = false;
						if (moveToPlayer != null)
						{
							isMoving = false;
							StopCoroutine(moveToPlayer);
						}
						if (Random.Range(1, 100) < 80)
						{
							isAttacking = true;
							animator.SetBool("IsAttacking", value: true);
						}
					}
				}
				AnimatorStateInfo currentAnimatorStateInfo2 = animator.GetCurrentAnimatorStateInfo(0);
				if (currentAnimatorStateInfo2.normalizedTime >= 1f && currentAnimatorStateInfo2.IsName("attack_anim") && !animator.IsInTransition(0))
				{
					isAttacking = false;
					animator.SetBool("IsAttacking", value: false);
				}
			}
			else
			{
				animator.SetBool("IsBinded", value: true);
			}
		}
	}

	public void ActivateAttackMode()
	{
		isAttackModeOn = !isAttackModeOn;
	}

	private IEnumerator MoveToPlayer()
	{
		while (true)
		{
			Vector3 position = base.transform.position;
			position.x += 0.004f * (float)dir;
			base.transform.position = position;
			yield return null;
		}
	}

	private void Flip()
	{
		facingRight = !facingRight;
		dir *= -1;
		base.transform.Rotate(0f, 180f, 0f);
	}

	public int Direction()
	{
		return dir;
	}
}
