using UnityEngine;

public class XLargeDebrisScript : MonoBehaviour
{
	private const float SPAWN_TIME = 3f;

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
		Vector3 position = base.transform.position;
		position.y = base.transform.position.y + 2.5f;
		base.transform.position = position;
	}

	private void Update()
	{
		if (Time.time < spawnTimer + 3f)
		{
			rb.gravityScale = 0f;
		}
		if (Time.time > spawnTimer + 3f)
		{
			rb.gravityScale = 0.5f;
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
			PlayerInteractionManager.GetComponent<PlayerInteractionManager>().GetAttack(1f);
		}
	}
}
