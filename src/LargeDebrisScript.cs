using UnityEngine;

public class LargeDebrisScript : MonoBehaviour
{
	private const float SPAWN_TIME = 1.25f;

	private float spawnTimer;

	private Rigidbody2D rb;

	private Animator animator;

	private GameObject PlayerInteractionManager;

	private void Start()
	{
		spawnTimer = Time.time;
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		PlayerInteractionManager = GameObject.Find("PlayerHitbox");
	}

	private void Update()
	{
		if (Time.time < spawnTimer + 1.25f)
		{
			rb.gravityScale = 0f;
		}
		if (Time.time > spawnTimer + 1.25f)
		{
			rb.gravityScale = 1f + Random.Range(-0.2f, 0.2f);
			animator.SetBool("IsFalling", value: true);
		}
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.name.Equals("FloorCollider"))
		{
			rb.gravityScale = 0f;
			rb.velocity = new Vector2(0f, 0f);
			animator.SetBool("IsGrounded", value: true);
		}
		if (collider.gameObject.name.Equals("PlayerHitbox"))
		{
			rb.gravityScale = 0f;
			rb.velocity = new Vector2(0f, 0f);
			animator.SetBool("IsGrounded", value: true);
			PlayerInteractionManager.GetComponent<PlayerInteractionManager>().GetAttack(0.5f);
		}
	}
}
